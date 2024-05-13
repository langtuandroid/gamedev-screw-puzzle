using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using WS.Script.GameManagers;
using WS.Script.Target;
using Zenject;

namespace WS.Script.Weapon
{
    public class Weapon : MonoBehaviour
    {
        [Inject] private TargetHandler _targetManager;
        [Inject] private SoundManager _soundManager;
        [Inject] private GameController _gameManager;
        private bool _isMoving;
        private Vector2 _targetPos;
        private float _speed;
        private RaycastHit2D _hitRay;
        private Rigidbody2D _rigidbody2D;
        private Collider2D _collider;
        private bool _isOnTarget;
        private bool _isAllowContact = true;
        private bool _isSpawnOnTarget;
        
        [FormerlySerializedAs("weaponRenderer")] [SerializeField] private SpriteRenderer _weaponRenderer;
        [FormerlySerializedAs("ID")][SerializeField] private int _id = 1;
        [FormerlySerializedAs("price")] [SerializeField] private int _price;
        [SerializeField] private string _name;

        public string Name => _name;
        public SpriteRenderer weaponRenderer => _weaponRenderer;
        public int Price => _price;
        public bool IsUnlocked
        {
            get
            {
                if (Price == 0)
                    return true;
                else
                    return PlayerPrefs.GetInt("Weapon" + _id, 0) == 1;
            }
            set => PlayerPrefs.SetInt("Weapon" + _id, value ? 1 : 0);
        }

        
        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();
            _collider.enabled = false;

            if (_isSpawnOnTarget)
                Throw(100);
        }

        public void SpawnInTarget()
        {
            _isSpawnOnTarget = true;
            _isAllowContact = false;
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                transform.position = Vector2.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
                if ((Vector2)transform.position == _targetPos)
                {
                    transform.parent = _hitRay.collider.transform;
                    _isMoving = false;
                    Invoke(nameof(TargetDamage), Time.fixedDeltaTime);
                }
            }
        }

        private void TargetDamage()
        {
            if (!_isSpawnOnTarget)
            {
                _targetManager.Hit(gameObject);
            }
            _isAllowContact = false;
            _isOnTarget = true;
        }

        public void Throw(float moveSpeed, LayerMask targetLayer)
        {
            _hitRay = Physics2D.Raycast(transform.position, transform.up, 100, targetLayer);
            _targetPos = _hitRay.point;
            _speed = moveSpeed;
            _isMoving = true;
            _collider.enabled = true;
        }

        private void Throw(float moveSpeed)
        {
            _hitRay = Physics2D.Raycast(transform.position, transform.up, 100);
            _targetPos = _hitRay.point;
            _speed = moveSpeed;
            _isMoving = true;
            _collider.enabled = true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!_isAllowContact || collision.gameObject.GetComponent<Target.Target>())
                return;

            if (collision.gameObject.layer == LayerMask.NameToLayer("Knife"))
            {
                _isMoving = false;
                _rigidbody2D.isKinematic = false;
                _rigidbody2D.AddTorque(Random.Range(500, 800));
                transform.parent = null;
                _gameManager.Fail();
                _soundManager.PlaySfx(_soundManager.ImpactSound);
                Destroy(this);
            }
        }

        public void Destroy()
        {
            StartCoroutine(DestroyRoutine());
        }

        private IEnumerator DestroyRoutine()
        {
            while (!_isOnTarget)
            {
                yield return null;
            }
            Destroy(this);
        }
    }
}
