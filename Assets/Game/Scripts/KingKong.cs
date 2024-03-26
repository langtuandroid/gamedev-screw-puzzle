using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class KingKong : MonoBehaviour
    {
        
        [SerializeField] private List<GameObject> chains;
        [SerializeField] private List<Rigidbody> chainsRigid;
        [SerializeField] private bool chainblast;
        [SerializeField] private GameObject girl;
        [SerializeField] private GameObject dupgirl;
        [SerializeField] private GameObject hand;
        [Inject] private AudioManager _audioManager;
        [Inject] private UIManager _uiManager;
        [Inject] private GameManager _gameManager;
        
        void Update()
        {
            if (chainblast)
            {
                for (int i = 0; i < chainsRigid.Count; i++)
                {
                    chains[i].transform.SetParent(null);
                    chainsRigid[i].isKinematic = false;
                    chainsRigid[i]
                        .AddExplosionForce(5f, chainsRigid[i].transform.position, 1f, 0.1f, ForceMode.Impulse);
                }
            }
        }

        public void KingKongFun()
        {
            var seq = DOTween.Sequence();
            seq.AppendCallback(() =>
            {
                GetComponent<Animator>().SetTrigger("Roar");
                _audioManager.Play("Chain");
            });
            seq.AppendInterval(2.3f);
            seq.AppendCallback(() =>
            {
                chainblast = true;
            });
        }

        public void GirlPosChange()
        {
            _audioManager.Play("Girl");
            girl.transform.DOLocalMove(dupgirl.transform.localPosition, 0.1f).SetEase(Ease.Linear);
            girl.transform.DOLocalRotateQuaternion(dupgirl.transform.localRotation,0.1f).SetEase(Ease.Linear);
            girl.transform.DOScale(dupgirl.transform.localScale,0.1f).SetEase(Ease.Linear);
            girl.SetActive(false);
            dupgirl.SetActive(true);
            dupgirl.transform.SetParent(hand.transform);
          
            DOVirtual.DelayedCall(2.5f, () =>
            {
                transform.GetComponent<DOTweenAnimation>().DOPlay();
                if (!_uiManager.Win)
                {
                    DOVirtual.DelayedCall(3f, () =>
                    {
                        _gameManager.Win();
                    });
                }
            });

        }
    }
}
