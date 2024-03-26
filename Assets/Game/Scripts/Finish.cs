using UnityEngine;

namespace Game.Scripts
{
    public class Finish : MonoBehaviour
    {
        public static Finish instance;
        [SerializeField] private ParticleSystem _blastParticle;
        private GameManager _gameManager;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            _gameManager = GameManager.instance;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("CUBE"))
            {
                _gameManager.IncreaseLevelsDone();
                Rigidbody rigidbody = other.gameObject.GetComponentInChildren<Rigidbody>();
                if (rigidbody.isKinematic)
                {
                    rigidbody.isKinematic = false;
                }
            
                Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                Destroy(other.gameObject.GetComponent<PolygonCollider2D>());
           
            }
        }

        public void PlayBlastParticle()
        {
            _blastParticle.Play();
        }
    }
}
