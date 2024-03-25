using System;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Serialization;

namespace Game.Scripts
{
    public class Wednesday : MonoBehaviour
    {
        public static Wednesday instance;
        [SerializeField] private GameObject _hand;
        [SerializeField] private GameObject _wednesdayGirl;
        [SerializeField] private GameObject _thief;
        [SerializeField] private Transform _thiefFinalPos;
        private bool _dead;
        private AudioManager _audioManager;
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _audioManager = AudioManager.instance;
        }

        public void WednesdayDone()
        {
            var seq = DOTween.Sequence();
            seq.Append(_hand.transform.parent.DORotate(new Vector3(0, 30f, 0f), 0.4f, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear));
            seq.AppendCallback(() =>
            {
                _hand.GetComponent<Animator>().SetTrigger("Run");
                seq.Append(_hand.transform.parent.DOMove(new Vector3(0, -1.2f, 0f), 1f).SetEase(Ease.Linear).OnComplete(
                    () =>
                    {
                        _hand.GetComponent<Animator>().SetTrigger("Punch");
                        DOVirtual.DelayedCall(1.5f, () =>
                        {
                            if (_audioManager)
                            {
                                _audioManager.Play("Punch");
                            }
                        });
                        seq.Append(_hand.transform.parent
                            .DORotate(new Vector3(0, -30f, 0f), 0.1f, RotateMode.WorldAxisAdd)
                            .SetEase(Ease.Linear).OnComplete(() =>
                            {
                                seq.Append(_hand.transform.parent
                                    .DORotate(new Vector3(45f, -10f, 10f), 0.1f, RotateMode.WorldAxisAdd)
                                    .SetEase(Ease.Linear));
                            }));
                    }));
            });
            seq.AppendInterval(2.4f);
            seq.AppendCallback(() =>
            {
                _hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = true;
                _thief.GetComponent<Animator>().SetTrigger("Backpunch");
                DOVirtual.DelayedCall(0.4f, () =>
                {
                    _hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = false;
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        _hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = true;
                        if (_audioManager)
                        {
                            _audioManager.Play("Punch");
                        }

                        DOVirtual.DelayedCall(0.4f, () =>
                        {
                            _hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = false;
                            DOVirtual.DelayedCall(1f, () =>
                            {
                                _hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = true;
                                if (_audioManager)
                                {
                                    _audioManager.Play("Punch");
                                }
                            });
                        });
                    });
                });

            });
            seq.AppendInterval(5f);
            seq.AppendCallback(() =>
            {
                _wednesdayGirl.GetComponent<SplineFollower>().enabled = true;
                _wednesdayGirl.GetComponent<SplineFollower>().spline = GameManager.instance.playerspline;
                _wednesdayGirl.GetComponent<Animator>().SetTrigger("Run");
                _hand.transform.parent.DORotate(new Vector3(82, 120, 240), 0.5f);
                _hand.transform.parent
                    .DOScale(
                        new Vector3(_hand.transform.parent.localScale.x - 30f, _hand.transform.parent.localScale.y - 30f,
                            _hand.transform.parent.localScale.z - 30f), 0.2f).SetEase(Ease.Linear);
                _hand.transform.parent.DOJump(_thiefFinalPos.position, 4f, 1, 0.3f).OnComplete(() =>
                {
                    if (_audioManager)
                    {
                        _audioManager.Play("Punch");
                    }
                });
                DOVirtual.DelayedCall(0.4f, () =>
                {
                    _thief.GetComponent<Animator>().SetTrigger("Cpunch");
                    _hand.GetComponent<Animator>().SetTrigger("Finalpunch");
                });
                DOVirtual.DelayedCall(2.4f, () =>
                {
                    _hand.transform.parent.DOJump(_thief.transform.position, 4f, 1, 1f).SetEase(Ease.Linear);
                    _hand.GetComponent<Animator>().SetTrigger("Run");
                    _thief.GetComponent<Animator>().SetTrigger("TRY");

                });
                DOVirtual.DelayedCall(2.2f, () =>
                {
                    _hand.transform.parent.DORotate(new Vector3(11, -105, 100 - 110), 0.3f).SetEase(Ease.Linear)
                        .OnComplete(
                            () =>
                            {
                                DOVirtual.DelayedCall(0.5f, () =>
                                {
                                    _hand.transform.parent.DOMove(_wednesdayGirl.transform.position, 3f).SetEase(Ease.Linear)
                                        .OnComplete(
                                            () =>
                                            {
                                                if (!UIManager.instance.win)
                                                {
                                                    GameManager.instance.winning();
                                                }
                                            });
                                    _thief.GetComponent<Animation>().enabled = false;

                                });
                            });
                });

            });


        }

   
        public void ThiefHit()
        {
            if (!_dead)
            {
                _thief.GetComponent<Animator>().SetTrigger("Cpunch");
                if (_audioManager)
                {
                    _audioManager.Play("Punch");
                }
            }
            else
            {
                _thief.GetComponent<Animator>().SetTrigger("TRY");
            }
        
        }

        public void Dead()
        {
            _thief.GetComponent<Animator>().SetTrigger("TRY");
        }
    }
}
