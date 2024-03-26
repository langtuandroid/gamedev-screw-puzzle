using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private Transform originalpos;
        [SerializeField] private Transform changePos;
        [SerializeField] private Transform FailCam;
        [SerializeField] private Transform wedcam;
        [Inject] private GameManager _gameManager;
        [Inject] private TouchDrop _touchDrop;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            if (_gameManager.GameMode == GameManager.Modes.Wednesday)
            {
                _transform.DOMove(wedcam.position, 2f).SetEase(Ease.Linear).OnComplete(() =>
                {
                    DOVirtual.DelayedCall(1f, () =>
                    {
                        FirstChange();
                    });
                });
            }
            else
            {
                DOVirtual.DelayedCall(3f, () =>
                {
                    FirstChange();
                });
            }
        }

        private void FirstChange()
        {
            _transform.DOMove(changePos.position, 2.5f).OnComplete(() =>
            {
                _touchDrop.SetStart();
            });
            _transform.rotation = changePos.transform.rotation;
        }
        
        public void SecondChange()
        {
            _transform.DOMove(originalpos.position, 2.5f).OnComplete(() =>
            { 
                _gameManager.DoorOpen();
            });
            _transform.rotation = originalpos.rotation;
        }

        public void Fail()
        {
            if (_gameManager.GameMode == GameManager.Modes.Pig || _gameManager.GameMode == GameManager.Modes.Kingkong )
            {
                _transform.DOMove(FailCam.position, 1.5f).OnComplete(() =>
                {
                    _gameManager.AnimalFail();
                });
                _transform.rotation = FailCam.rotation;
            
            }
        }
    }
}
