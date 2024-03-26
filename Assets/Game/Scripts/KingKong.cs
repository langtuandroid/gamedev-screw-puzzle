using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts
{
    public class KingKong : MonoBehaviour
    {
  
        [FormerlySerializedAs("chainsRigid")] [SerializeField] private List<Rigidbody> _chainsRigid;
        [SerializeField] private GameObject _girl;
        [SerializeField] private GameObject _duplicateGirl;
        [SerializeField] private GameObject _hand;
        [Inject] private AudioManager _audioManager;
        [Inject] private UIManager _uiManager;
        [Inject] private GameManager _gameManager;
        private bool _chainBlast;
        void Update()
        {
            if (_chainBlast)
            {
                for (int i = 0; i < _chainsRigid.Count; i++)
                {
                    _chainsRigid[i].transform.SetParent(null);
                    _chainsRigid[i].isKinematic = false;
                    _chainsRigid[i]
                        .AddExplosionForce(5f, _chainsRigid[i].transform.position, 1f, 0.1f, ForceMode.Impulse);
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
                _chainBlast = true;
            });
        }

        public void GirlPosChange()
        {
            _audioManager.Play("Girl");
            _girl.transform.DOLocalMove(_duplicateGirl.transform.localPosition, 0.1f).SetEase(Ease.Linear);
            _girl.transform.DOLocalRotateQuaternion(_duplicateGirl.transform.localRotation,0.1f).SetEase(Ease.Linear);
            _girl.transform.DOScale(_duplicateGirl.transform.localScale,0.1f).SetEase(Ease.Linear);
            _girl.SetActive(false);
            _duplicateGirl.SetActive(true);
            _duplicateGirl.transform.SetParent(_hand.transform);
          
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
