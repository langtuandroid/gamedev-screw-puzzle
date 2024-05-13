using UnityEngine;
using UnityEngine.Serialization;

namespace WS.Script.Other
{
    public class DeactivateSelf : MonoBehaviour
    {
        [FormerlySerializedAs("timeToLive")] [SerializeField] private float _liveTime = 0.5f;

        private void OnEnable()
        {
            Invoke(nameof(Deactivate), _liveTime);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void OnDisable()
        {
            CancelInvoke();
        }
    }
}
