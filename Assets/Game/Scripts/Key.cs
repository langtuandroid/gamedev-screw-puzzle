using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class Key : MonoBehaviour
    {
        [SerializeField] private GameObject locking;
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
                gameObject.transform.DOMove(locking.gameObject.transform.position, 0.5f).OnComplete(() =>
                {
                    transform.GetComponentInChildren<MeshRenderer>().enabled = false;
                    if (_audioManager)
                    {
                        _audioManager.Play("Lock");
                        GameManager.Vibrate();
                    }
                    locking.GetComponentInChildren<DOTweenAnimation>().DOPlay();
                    if (!locking.transform.GetComponentInChildren<ParticleSystem>().isPlaying)
                    {
                        locking.transform.GetComponentInChildren<ParticleSystem>().Play();
                    }
            
                });
            }
        }

        public void Locked()
        {
            locking.transform.parent.GetChild(1).tag = "BOLT";
            transform.gameObject.SetActive(false);
            locking.transform.SetParent(null);
            locking.GetComponent<Rigidbody>().isKinematic = false;
        }
    
    }
}
