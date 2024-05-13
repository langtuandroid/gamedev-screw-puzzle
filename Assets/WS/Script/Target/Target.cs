using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using WS.Script.GameManagers;
using WS.Script.Other;
using WS.Script.Weapon;
using Zenject;

namespace WS.Script.Target
{
    public enum STAGE_TYPE
    {
        Normal,
        BossFight
    }

    public class Target : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private SoundManager _soundManager;
        [Inject] private GameController _gameManager;
        [FormerlySerializedAs("stageType")] public STAGE_TYPE _stage;
        [FormerlySerializedAs("targetBehaviorID")] public TargetRotation _ID;
        [FormerlySerializedAs("targetBehaviorIDList")] [SerializeField] private TargetRotation[] _targetRotations;
        [FormerlySerializedAs("lives")] [SerializeField] private int _lives = 10;
        [FormerlySerializedAs("soundHit")] [SerializeField] private AudioClip _hitSound;
        [FormerlySerializedAs("soundBreak")] [SerializeField] private AudioClip _breakSound;
        [FormerlySerializedAs("ownerImage")] [SerializeField] private SpriteRenderer _ownerImage;
        [FormerlySerializedAs("brokenFX")] [SerializeField] private BreakTargetFX _brokenParticle;

        
        [Header("COIN and OBSTACLE")]
        [FormerlySerializedAs("obstacleNumberMax")][Range(0, 6)] [SerializeField] private int _maxObstacles;
        [FormerlySerializedAs("coinNumberMax")] [Range(0, 6)] [SerializeField] private int _coinNumMax;
        [FormerlySerializedAs("coinObj")] [SerializeField] private GameObject _coinPrefab;
        [FormerlySerializedAs("obstacleObj")] [SerializeField] private GameObject _onstaclePrefab;
        [FormerlySerializedAs("randomCoin")] [SerializeField] private bool isRandCoin = true;
        [FormerlySerializedAs("randomObstacle")] [SerializeField] private bool isRandObstacle = true;
        [FormerlySerializedAs("coinHolderPos")] [SerializeField] private Transform[] _coinsPositions;
        [FormerlySerializedAs("ObstaclesHolderPos")] [SerializeField] private Transform[] _obstacleHolders;
        
        private int _hitPoint = 1;
        private float _speed;
        private float _angle;
        private float _rotationAngle;
        private Animator _animator;
        private bool _isDead;
        private List<GameObject> _knifeList;
        private List<GameObject> _tomatoList;

        public int Lives => _lives;

        private void Start()
        {
            if (_targetRotations.Length > 0)
            {
                _ID = _targetRotations[Random.Range(0, _targetRotations.Length)];
            }
            else
                Debug.LogError("NEED SET TARGET BEHAVIOR TO THIS TARGET CONTROLLER " + gameObject.name);

            StartCoroutine(NextStageRoutine());
            _animator = GetComponent<Animator>();
            _knifeList = new List<GameObject>();
            _tomatoList = new List<GameObject>();

            SpawnAdditional();
        }

        private void SpawnAdditional()
        {
            if (_coinNumMax > 0)
            {
                int spawnAmount = isRandCoin ? Random.Range(0, _coinNumMax+1) : _coinNumMax;
                List<Transform> listPos = new List<Transform>(_coinsPositions);
                for(int i = 0; i < spawnAmount; i++)
                {
                    var rand = Random.Range(0, listPos.Count);
                    _tomatoList.Add( _diContainer.InstantiatePrefab(_coinPrefab, listPos[rand].position, listPos[rand].rotation, transform));
                    listPos.RemoveAt(rand);
                }
            }

            if (_maxObstacles > 0)
            {
                int spawnAmount = isRandObstacle ? Random.Range(0, _maxObstacles+1) : _maxObstacles;
                List<Transform> listPos = new List<Transform>(_obstacleHolders);
                for (int i = 0; i < spawnAmount; i++)
                {
                    var rand = Random.Range(0, listPos.Count);
                    var spawnedKnife = _diContainer.InstantiatePrefab(_onstaclePrefab, listPos[rand].position, listPos[rand].rotation, transform);
                    spawnedKnife.GetComponent<Weapon.Weapon>().SpawnInTarget();
                    _knifeList.Add(spawnedKnife);
                    listPos.RemoveAt(rand);
                }
            }
        }

        private IEnumerator NextStageRoutine()
        {
            yield return new WaitForSeconds(_ID.DelayOnStart);

            int currentWave = 0;
            while (true)
            {
                Wave pickedWave;
                if (_ID.OrderType == OrderType.Sequence)
                {
                    pickedWave = _ID.Waves[currentWave];
                    currentWave++;
                    if (currentWave >= _ID.Waves.Length)
                        currentWave = 0;
                }
                else
                {
                    var rand = Random.Range(0, _ID.Waves.Length);
                    pickedWave = _ID.Waves[rand];
                }

                for (int i = 0; i < pickedWave.SpeedInfo.Length; i++)
                {
                    _speed = pickedWave.SpeedInfo[i]._accSpedd;
                    _angle = pickedWave.SpeedInfo[i]._angle;


                    yield return new WaitForSeconds(Random.Range(pickedWave.SpeedInfo[i]._minTime, pickedWave.SpeedInfo[i]._maxTime));

                }
            }
        }

        private void FixedUpdate()
        {
            if (_gameManager.gameState != GameController.GameState.Playing)
                return;

            if (_isDead)
                return;

            if (Time.timeScale == 0)
                return;
        
            _rotationAngle = Mathf.MoveTowards(_rotationAngle, _angle, _speed * Time.deltaTime);
            transform.Rotate(Vector3.forward, _rotationAngle);
        }
        public void KnifeHit(GameObject weapon)
        {
            _gameManager.GameScore += _hitPoint;
            _lives--;
            _knifeList.Add(weapon);
            if (Lives <= 0)
            {
                _soundManager.PlaySfx(_breakSound);
                DestroyObject();
            }
            else
            {
                _soundManager.PlaySfx(_hitSound);
                _animator.SetTrigger("hit");
            }
        }

        private void DestroyObject()
        {
            Instantiate(_brokenParticle, transform.position, transform.rotation).Construct(_ownerImage.sprite);
            _isDead = true;
            _ownerImage.enabled = false;

            foreach(var knife in _knifeList)
            {
                var rig = knife.GetComponent<Rigidbody2D>();
                knife.GetComponent<Collider2D>().enabled = false;
                rig.isKinematic = false;
                rig.AddForce(new Vector2(Random.Range(-.5f, .5f), Random.Range(0.3f, 1f)) * 200);
                rig.AddTorque(Random.Range(-150, 150));
            }

            foreach (var tom in _tomatoList)
            {
                if (tom != null)
                {
                    var rig = tom.GetComponent<Rigidbody2D>();
                    tom.GetComponent<Collider2D>().enabled = false;
                    rig.isKinematic = false;
                    rig.AddForce(new Vector2(Random.Range(-.5f, .5f), Random.Range(0.3f, 1f)) * 200);
                    rig.AddTorque(Random.Range(-150, 150));
                }
            }

            _gameManager.OpenNewStage();
        }
    }
}