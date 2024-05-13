using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts
{
    public class CameraMove : MonoBehaviour
    {
        [FormerlySerializedAs("originalpos")] [SerializeField] private Transform _originalPos;
        [FormerlySerializedAs("changePos")] [SerializeField] private Transform _changePos;
        [FormerlySerializedAs("FailCam")] [SerializeField] private Transform _failCam;
        [FormerlySerializedAs("wedcam")] [SerializeField] private Transform _wedCam;
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
                _transform.DOMove(_wedCam.position, 2f).SetEase(Ease.Linear).OnComplete(() =>
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
            _transform.DOMove(_changePos.position, 2.5f).OnComplete(() =>
            {
                _touchDrop.SetStart();
            });
            _transform.rotation = _changePos.transform.rotation;
        }
        
        public void SecondChange()
        {
            _transform.DOMove(_originalPos.position, 2.5f).OnComplete(() =>
            { 
                _gameManager.DoorOpen();
            });
            _transform.rotation = _originalPos.rotation;
        }

        public void Fail()
        {
            if (_gameManager.GameMode == GameManager.Modes.Pig || _gameManager.GameMode == GameManager.Modes.Kingkong )
            {
                _transform.DOMove(_failCam.position, 1.5f).OnComplete(() =>
                {
                    _gameManager.AnimalFail();
                });
                _transform.rotation = _failCam.rotation;
            
            }
        }
    }
}
