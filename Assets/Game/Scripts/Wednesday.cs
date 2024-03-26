using System;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts
{
    public class Wednesday : MonoBehaviour
    {
        [SerializeField] private GameObject _hand;
        [SerializeField] private GameObject _wednesdayGirl;
        [SerializeField] private GameObject _thief;
        [SerializeField] private Transform _thiefFinalPos;
        [Inject] private GameManager _gameManager;
        [Inject] private AudioManager _audioManager;
        [Inject] private UIManager _uiManager;
        private bool _dead;
        private Animator _handAnimator;
        private Animator _thiefAnimator;
        private DOTweenVisualManager _handVisualManager;

        private void Start()
        {
            _handAnimator = _hand.GetComponent<Animator>();
            _handVisualManager = _hand.transform.parent.GetComponent<DOTweenVisualManager>();
            _thiefAnimator = _thief.GetComponent<Animator>();
        }

        public void WednesdayDone()
        {
            var seq = DOTween.Sequence();
            seq.Append(_hand.transform.parent.DORotate(new Vector3(0, 30f, 0f), 0.4f, RotateMode.WorldAxisAdd)
                .SetEase(Ease.Linear));
            seq.AppendCallback(() =>
            {
                _handAnimator.SetTrigger("Run");
                seq.Append(_hand.transform.parent.DOMove(new Vector3(0, -1.2f, 0f), 1f).SetEase(Ease.Linear).OnComplete(
                    () =>
                    {
                        _handAnimator.SetTrigger("Punch");
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
                _handVisualManager.enabled = true;
                _thief.GetComponent<Animator>().SetTrigger("Backpunch");
                DOVirtual.DelayedCall(0.4f, () =>
                {
                    _handVisualManager.enabled = false;
                    DOVirtual.DelayedCall(2.5f, () =>
                    {
                        _handVisualManager.enabled = true;
                        if (_audioManager)
                        {
                            _audioManager.Play("Punch");
                        }

                        DOVirtual.DelayedCall(0.4f, () =>
                        {
                            _handVisualManager.enabled = false;
                            DOVirtual.DelayedCall(1f, () =>
                            {
                                _handVisualManager.enabled = true;
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
                _wednesdayGirl.GetComponent<SplineFollower>().spline = _gameManager.PlayerSpline;
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
                    _handAnimator.SetTrigger("Finalpunch");
                });
                DOVirtual.DelayedCall(2.4f, () =>
                {
                    _hand.transform.parent.DOJump(_thief.transform.position, 4f, 1, 1f).SetEase(Ease.Linear);
                    _handAnimator.SetTrigger("Run");
                    _thiefAnimator.SetTrigger("TRY");

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
                                                if (!_uiManager.Win)
                                                {
                                                    _gameManager.Win();
                                                }
                                            });
                                    _thiefAnimator.enabled = false;

                                });
                            });
                });

            });


        }

   
        public void ThiefHit()
        {
            if (!_dead)
            {
                _thiefAnimator.SetTrigger("Cpunch");
                if (_audioManager)
                {
                    _audioManager.Play("Punch");
                }
            }
            else
            {
                _thiefAnimator.SetTrigger("TRY");
            }
        
        }

        public void Dead()
        {
            _thiefAnimator.SetTrigger("TRY");
        }
    }
}
