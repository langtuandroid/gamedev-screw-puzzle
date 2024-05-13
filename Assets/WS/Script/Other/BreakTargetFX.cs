using UnityEngine;
using UnityEngine.Serialization;

namespace WS.Script.Other
{
    public class BreakTargetFX : MonoBehaviour
    {
        [FormerlySerializedAs("targetImage")] [SerializeField] private SpriteRenderer[] _targetSpriteRenderer;
        [FormerlySerializedAs("pieceRig")] [SerializeField] private Rigidbody2D[] _rigidbody;
        private readonly float _breakForce = 150;
        private readonly float _breakTrgle = 100;
        public void Construct(Sprite targetImage)
        {
            if (targetImage != null)
            {
                foreach (var image in this._targetSpriteRenderer)
                {
                    image.sprite = targetImage;
                }
            }

            foreach (var rig in _rigidbody)
            {
                rig.AddForce(new Vector2(Random.Range(-1f,1f), Random.Range(-.2f, 1f)) * _breakForce);
                rig.AddTorque(Random.Range(-_breakTrgle, _breakTrgle));
            }

            Destroy(gameObject, 2);
        }
    }
}
