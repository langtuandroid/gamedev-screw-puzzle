using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Scripts
{
    public class NewBolt : MonoBehaviour
    {
        [SerializeField] private List<GameObject> connectedBodylist;
        [Inject] private GameManager _gameManger;
        
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
