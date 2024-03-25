using DG.Tweening;
using UnityEngine;

namespace Game.Scripts
{
    public class Key : MonoBehaviour
    {
        public static Key instance;

        public GameObject locking;

        public GameManager gamemanager;
        private void Awake()
        {
            instance = this;
        }
        void Start()
        {
            gamemanager = GameManager.instance;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("CUBE"))
            {
                gameObject.GetComponent<Collider>().enabled = false;
                gameObject.transform.GetComponentInChildren<DOTweenAnimation>().DOComplete();
                if (AudioManager.instance)
                {
                    gamemanager.Vibration();
                    AudioManager.instance.Play("Key");
                }
                gameObject.transform.DOMove(locking.gameObject.transform.position, 0.5f).OnComplete(() =>
                {
                    transform.GetComponentInChildren<MeshRenderer>().enabled = false;
                    if (AudioManager.instance)
                    {
                        AudioManager.instance.Play("Lock");
                        gamemanager.Vibration();
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
