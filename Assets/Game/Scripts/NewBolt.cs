using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts
{
    public class NewBolt : MonoBehaviour
    {
        [FormerlySerializedAs("connectedBodylist")] [SerializeField] private List<GameObject> _connectedBodyList;
        [Inject] private GameManager _gameManger;
        
        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("CUBE"))
            {
                if (_gameManger.GameState == GameManager.State.Idle)
                {
                    if (!_connectedBodyList.Contains(other.gameObject))
                    {
                        _connectedBodyList.Add(other.gameObject);
                    }
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag("CUBE"))
            {
                if (_connectedBodyList.Contains(other.gameObject))
                {
                    _connectedBodyList.Remove(other.gameObject);
                }
            }
        }
    }
}
