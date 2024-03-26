using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    public class CameraMove : MonoBehaviour
    {
        public static CameraMove Instance;
    
        public Transform originalpos;
        public Transform changePos;
        public Transform FailCam;
        
        public Transform wedcam;

        private GameManager _gameManager;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _gameManager = GameManager.instance;
            
            if (_gameManager.GameMode == GameManager.Modes.Wednesday)
            {
                transform.DOMove(wedcam.position, 2f).SetEase(Ease.Linear).OnComplete(() =>
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
            transform.DOMove(changePos.position, 2.5f).OnComplete(() =>
            {
                TouchDrop.instance.start = true;
            });
            transform.rotation = changePos.transform.rotation;
        }
        
        public void SecondChange()
        {
            transform.DOMove(originalpos.position, 2.5f).OnComplete(() =>
            { 
                _gameManager.DoorOpen();
            });
            transform.rotation = originalpos.rotation;
        }

        public void Fail()
        {
            if (_gameManager.GameMode == GameManager.Modes.Pig || _gameManager.GameMode == GameManager.Modes.Kingkong )
            {
                transform.DOMove(FailCam.position, 1.5f).OnComplete(() =>
                {
                    _gameManager.Animal.GetComponent<Animator>().SetTrigger("Fail");
                });
                transform.rotation = FailCam.rotation;
            
            }
        }
    }
}
