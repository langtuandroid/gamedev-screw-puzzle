using UnityEngine;

namespace Game.Scripts
{
    public class Finish : MonoBehaviour
    {
        public static Finish instance;
    
        public ParticleSystem blast;

        private void Awake()
        {
            instance = this;
        }
    
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("CUBE"))
            {
                GameManager.instance.donecount++;
                if (other.gameObject.GetComponentInChildren<Rigidbody>().isKinematic == true)
                {
                    other.gameObject.GetComponentInChildren<Rigidbody>().isKinematic = false;
                }
            
                Destroy(other.gameObject.GetComponent<Rigidbody2D>());
                Destroy(other.gameObject.GetComponent<PolygonCollider2D>());
           
            }
        }
    }
}
