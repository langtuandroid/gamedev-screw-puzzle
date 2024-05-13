using UnityEngine;
using UnityEngine.Serialization;
using WS.Script.GameManagers;
using WS.Script.Weapon;
using Zenject;

namespace WS.Script.Target
{
    public class TomatoCollectable : MonoBehaviour
    {
        [Inject] private ObjectPool _objectPool;
        [Inject] private SoundManager _soundManager;
        [Inject] private GameController _gameManager;
        
        [FormerlySerializedAs("FX")] [SerializeField] private GameObject _particle;
        [FormerlySerializedAs("HitFX")] [SerializeField] private GameObject _hitParticle;
        [FormerlySerializedAs("sound")] [SerializeField] private AudioClip _hitSound;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<Weapon.Weapon>())
            {
                _soundManager.PlaySfx(_hitSound);
                _objectPool.GetFreeElement (_particle, true, transform.position);
                _objectPool.GetFreeElement(_hitParticle, true, transform.position);
                ValueStorage.CoinsData++;
                _gameManager.GameScore++;
                Destroy(gameObject);
            }
        }
    }
}
