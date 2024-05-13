using UnityEngine;
using UnityEngine.Serialization;

namespace WS.Script.Target
{
    public enum OrderType
    {
        Sequence
    }

    public class TargetRotation : MonoBehaviour
    {
        [FormerlySerializedAs("orderType")] [SerializeField] private OrderType _orderType;
        [FormerlySerializedAs("delayOnStart")] [SerializeField] private float _startDelay;
        [FormerlySerializedAs("waves")] [SerializeField] private Wave[] _waves;

        public OrderType OrderType => _orderType;
        public float DelayOnStart => _startDelay;
        public Wave[] Waves => _waves;
    }
    
    [System.Serializable]
    public class Wave
    {
        [FormerlySerializedAs("targetSpeeds")] public SpeedInfo[] _speedInfo;

        public SpeedInfo[] SpeedInfo => _speedInfo;
    }

    [System.Serializable]
    public class SpeedInfo
    {
        [FormerlySerializedAs("targetAngle")] public float _angle = 10;
        [FormerlySerializedAs("accSpeed")] public float _accSpedd = 10;
        [FormerlySerializedAs("timeMin")] public float _minTime = 3;
        [FormerlySerializedAs("timeMax")] public float _maxTime = 6;
    }
}