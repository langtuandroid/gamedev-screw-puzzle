using UnityEngine;
using UnityEngine.Serialization;

namespace WS.Script.Other
{
    public class RotationObject : MonoBehaviour
    {
        [FormerlySerializedAs("direction")] [SerializeField] private Vector3 _rotateDirection = new Vector3(0, 0, 1f);
        [FormerlySerializedAs("speed")] [SerializeField] private float _rotationSpeed = 1f;

        private void Update()
        {
            transform.Rotate(_rotateDirection * (_rotationSpeed * 2));
        }
    }
}
