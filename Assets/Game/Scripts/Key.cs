using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts
{
    public class Key : MonoBehaviour
    {
        [FormerlySerializedAs("locking")] [SerializeField] private GameObject _locking;
        [Inject] private AudioManager _audioManager;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("CUBE"))
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.transform.GetComponentInChildren<DOTweenAnimation>().DOComplete();
                if (_audioManager)
                {
                    GameManager.Vibrate();
                    _audioManager.Play("Key");
                }
                gameObject.transform.DOMove(_locking.gameObject.transform.position, 0.5f).OnComplete(() =>
                {
                    transform.GetComponentInChildren<MeshRenderer>().enabled = false;
                    if (_audioManager)
                    {
                        _audioManager.Play("Lock");
                        GameManager.Vibrate();
                    }
                    _locking.GetComponentInChildren<DOTweenAnimation>().DOPlay();
                    if (!_locking.transform.GetComponentInChildren<ParticleSystem>().isPlaying)
                    {
                        _locking.transform.GetComponentInChildren<ParticleSystem>().Play();
                    }
            
                });
            }
        }

        public void Locked()
        {
            _locking.transform.parent.GetChild(1).tag = "BOLT";
            transform.gameObject.SetActive(false);
            _locking.transform.SetParent(null);
            _locking.GetComponent<Rigidbody>().isKinematic = false;
        }
    
    }
}
