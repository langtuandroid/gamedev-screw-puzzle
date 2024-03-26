using System.Collections.Generic;
using UnityEngine;
namespace Game.Scripts
{
    public class NewBolt : MonoBehaviour
    {
        public List<GameObject> connectedBodylist;
        private GameManager _gameManger;
        void Start()
        {
            _gameManger = GameManager.instance;
        }
    
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("CUBE"))
            {
                if (_gameManger.GameState == GameManager.State.Idle)
                {
                    if (!connectedBodylist.Contains(other.gameObject))
                    {
                        connectedBodylist.Add(other.gameObject);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("CUBE"))
            {
                if (connectedBodylist.Contains(other.gameObject))
                {
                    connectedBodylist.Remove(other.gameObject);
                }
            }
        }
    }
}
