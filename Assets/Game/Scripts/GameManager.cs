using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Game.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public enum Modes
        {
            Null,
            Tiger,
            BirdsCats,
            Elephant,
            Wednesday,
            Pig,
            Kingkong,
            KingKong1,
            Dogs,
        }
        public enum State
        {
            Null,
            Idle,
            Select,
            Done,
        }
        
        [FormerlySerializedAs("gamemodes")] [SerializeField] private Modes _gamemodes;
        [Header("Bolt Shifting")] 
        [SerializeField] private CircleCollider2D _dupColider;
        [FormerlySerializedAs("totalcount")] [SerializeField] private int _totalCount;
        [SerializeField] private GameObject _fillParticle;
        
        [Header("ANIMAL")] 
        [FormerlySerializedAs("Animal")] [SerializeField] private GameObject _animal;
        [FormerlySerializedAs("thief")] [SerializeField] private GameObject _thief;
        [FormerlySerializedAs("player")] [SerializeField] private GameObject _player;
        [FormerlySerializedAs("playerspline")] [SerializeField] private SplineComputer _playerSpline;

        [Header("Birds or cats")]
        [SerializeField] private bool _birds;
        [SerializeField] private bool _cats;
        [SerializeField] private bool _dogs;
        [SerializeField] private bool _rabbit;
        [SerializeField] private bool _fox;
        [SerializeField] private bool _zebra;
        [SerializeField] private bool _dear;
        [SerializeField] private bool _flamingo;
        [FormerlySerializedAs("birds")] [SerializeField] private List<GameObject> _birdsObjects;
        [FormerlySerializedAs("birdsanim")] [SerializeField] private List<DOTweenAnimation> _birdsAnim;
        [FormerlySerializedAs("birds2")] [SerializeField] private List<GameObject> _birds2;
        [FormerlySerializedAs("birdsanim2")] [SerializeField] private List<DOTweenAnimation> _birdsAnim2;
        [FormerlySerializedAs("cagedoor")] [SerializeField] private GameObject _cageDoor;
        [FormerlySerializedAs("policeMan")] [SerializeField] private GameObject _policeMan;
        [FormerlySerializedAs("sleepeffect")] [SerializeField] private GameObject _sleepEffect;
        [Inject] private Finish _finish;
        [Inject] private AudioManager _audioManager;
        [Inject] private UIManager _uiManager;
        [Inject] private CameraMove _cameraMove;
        [Inject] private Board _board;
        [Inject] private Wednesday _wednesday;
        [Inject] private KingKong _kingKong;
        private bool _once;
        private bool _fail;
        private int _doneCount;
        
        public SplineComputer PlayerSpline => _playerSpline;
        public Modes GameMode => _gamemodes;
        public State GameState { get; set; }
        public GameObject DupPlug { get; set; }
        
        void Start()
        {
            GameState = State.Done;
            if (_board)
            {
                transform.position = _board.transform.position;
            }
            _dupColider.enabled = false;
            Vibration.Init();
        }
        
        void Update()
        {
            if (_totalCount == _doneCount /*|| test*/)
            {
                if (!_uiManager.Win && _gamemodes == Modes.Null)
                {
                    Win();
                }
            
                switch (_gamemodes)
                {
                    case Modes.BirdsCats or Modes.Dogs:
                    {
                        if (_totalCount == _doneCount && !_once)
                        {
                            _cameraMove.SecondChange();
                            _once = true;
                        }

                        break;
                    }
                    case Modes.Tiger or Modes.Elephant or Modes.Pig when !_once:
                        _cameraMove.SecondChange();
                        _once = true;
                        DOVirtual.DelayedCall(4.2f, () =>
                        {
                            StartCoroutine(PoliceChaseWait());
                        });
                        break;
                }

                switch (_gamemodes)
                {
                    case Modes.Wednesday when !_once:
                        _cameraMove.SecondChange();
                        _once = true;
                        break;
                    case Modes.Kingkong when !_once:
                        _once = true;
                        _cameraMove.SecondChange();
                        break;
                    case Modes.KingKong1 when !_once:
                        _once = true;
                        _cameraMove.SecondChange();
                        DOVirtual.DelayedCall(4.2f, () =>
                        {
                            _audioManager.Play("KingKong");
                            StartCoroutine(PoliceChaseWait());
                        });
                        break;
                }
            }

            if (!_fail || _once) return;
            _cameraMove.Fail();
            _once = true;

        }
        public void Win()
        {
            _finish.PlayBlastParticle();
            _audioManager.Play("Win");
            _uiManager.Win = true;
            _uiManager.WinPanel();
        }

        private void TigerAttack()
        {
            _animal.gameObject.GetComponent<Animator>().SetTrigger("Walk");
            _animal.transform.DOMove(new Vector3(0, _animal.transform.position.y, 0.5f), 1.5f).SetEase(Ease.Linear);
            DOVirtual.DelayedCall(1.25f, () =>
            {
                _animal.transform.DOLocalRotate(new Vector3(0, 0, -65), 0.1f, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.Linear);
            });
            DOVirtual.DelayedCall(1.5f,() =>
            {
            
                _animal.transform.DOMove(new Vector3(_thief.transform.localPosition.x-0.5f, _animal.transform.position.y, _thief.transform.localPosition.z+1.5f), 1f).SetEase(Ease.Linear).OnUpdate(
                    () =>
                    {
                        _animal.gameObject.GetComponent<Animator>().SetTrigger("Attack");
                        _player.GetComponent<Animator>().SetTrigger("Run");
                        _player.GetComponent<SplineFollower>().enabled = true;
                        _player.GetComponent<SplineFollower>().spline = _playerSpline;
                        DOVirtual.DelayedCall(0.4f, () =>
                        {
                            _thief.GetComponent<Animator>().SetTrigger("Fall");
                        });

                    });
            });
            if (!_uiManager.Win)
            {
                DOVirtual.DelayedCall(5f, Win);
            }
        }
    
        public void DoorOpen()
        {
            if (_gamemodes == Modes.Kingkong)
            {
                _cageDoor.GetComponent<Rigidbody>().isKinematic = false;
                _kingKong.KingKongFun();
            }

            else
            {
                _audioManager.Play("Door");
                _cageDoor.transform.DOLocalRotate(new Vector3(0, -120f, 0), 1f,RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(
                    () =>
                    {
                        if (_gamemodes == Modes.Tiger)
                        {
                            _audioManager.Play("Cheetah");
                        }
                        AnimalAnimation();
                
                    });
            }
        }
        public void AnimalAnimation()
        {
            if (_gamemodes == Modes.Tiger)
            {
                TigerAttack();
            }
            if (_gamemodes==Modes.BirdsCats || _gamemodes==Modes.Dogs)
            {
                BirdsCats();
            }
        
            if (_gamemodes == Modes.Wednesday)
            {
                _wednesday.WednesdayDone();
                print("Wednesday");
            }

            if (_gamemodes==Modes.Elephant)
            {
                _animal.GetComponent<Animator>().SetTrigger("Run");
            
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    _audioManager.Play("Elephant");
                    _animal.GetComponent<DOTweenAnimation>().DOPlay();
                });
            }
            if (_gamemodes == Modes.Pig)
            {
                _animal.GetComponent<Animator>().SetTrigger("Run");
                _animal.transform.DOMoveY(_animal.transform.position.y-1.5f,0.02f);
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    _audioManager.Play("Pig");
                    _animal.GetComponent<DOTweenAnimation>().DOPlay();
                });
            }
            if (_gamemodes == Modes.KingKong1)
            {
                _animal.GetComponent<Animator>().SetTrigger("Run");
                _animal.transform.DOMoveY(_animal.transform.position.y-0.5f,0.02f);
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    _animal.GetComponent<DOTweenAnimation>().DOPlay();
                });
            }

        }

        private void BirdsCats()
        {
            if (_birds)
            {
                _audioManager.Play("Birds");
            }

            if (_cats)
            {
                _audioManager.Play("Cats");
            }

            if (_dogs)
            {
                _audioManager.Play("Dogs");
            }

            if (_flamingo)
            {
                _audioManager.Play("Flamingo");
            }

            if (_rabbit)
            {
                _audioManager.Play("Rabbit");
            }

            if (_zebra)
            {
                _audioManager.Play("Zebra");
            }
            
            foreach (var bird in _birdsObjects)
            {
                bird.GetComponent<Animator>().SetTrigger("Fly");
                    
                if (_birds)
                {
                    bird.transform
                        .DOMove(
                            new Vector3(Random.Range(-1.14f, 1.14f),
                                Random.Range(-4f, 0f), Random.Range(-1f, -0.3f)),
                            Random.Range(0.3f, 0.7f)).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            foreach (var t in _birdsAnim)
                            {
                                t.DOPlay();
                            }
                        });
                    if (_birds2.Count == 0)
                    {
                        StartCoroutine(PoliceChaseWait());
                    }
                }
                if (_cats || _dogs || _rabbit || _fox || _zebra || _dear || _flamingo)
                {
                    bird.transform
                        .DOMove(
                            new Vector3((float)UnityEngine.Random.Range(-1.14f, 1.14f),
                                (float)UnityEngine.Random.Range(-4f, -3f), UnityEngine.Random.Range(-1f, -0.3f)),
                            UnityEngine.Random.Range(0.3f, 0.7f)).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            foreach (var t in _birdsAnim)
                            {
                                t.DOPlay();
                            }
                        });
                    if (_birds2.Count == 0)
                    {
                        StartCoroutine(PoliceChaseWait());
                    }
                }
            }

            if (_birds)
            {
                _audioManager.Play("Birds");
            }
            foreach (var bird in _birds2)
            {
                bird.GetComponent<Animator>().SetTrigger("Fly");
                    
                if (_birds)
                {
                    bird.transform
                        .DOMove(
                            new Vector3((float)UnityEngine.Random.Range(-1.14f, 1.14f),
                                (float)UnityEngine.Random.Range(-4f, 0f), UnityEngine.Random.Range(-1f, -0.3f)),
                            UnityEngine.Random.Range(0.5f, 1f)).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            foreach (var t in _birdsAnim2)
                            {
                                t.DOPlay();
                            }
                        });
                    StartCoroutine(PoliceChaseWait());
                }
                if (_cats || _dogs || _rabbit || _fox || _zebra || _dear || _flamingo)
                {
                    bird.transform
                        .DOMove(
                            new Vector3((float)UnityEngine.Random.Range(-1.14f, 1.14f),
                                (float)UnityEngine.Random.Range(-4f, -3f), UnityEngine.Random.Range(-1f, -0.3f)),
                            UnityEngine.Random.Range(0.5f, 1f)).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            foreach (var t in _birdsAnim2)
                            {
                                t.DOPlay();
                            }
                        });
                    StartCoroutine(PoliceChaseWait());
                }
            }
        }


        private void BirdsPoliceChase()
        {
            _policeMan.GetComponent<Animator>().SetTrigger("Chase");
        }

        private void BirdsPoliceMove()
        {
            _audioManager.Play("Police");
            _policeMan.GetComponent<DOTweenAnimation>().DOPlay();
            DOVirtual.DelayedCall(3f, () =>
            {
                Win();
            });
        }

        private IEnumerator PoliceChaseWait()
        {
            _sleepEffect.SetActive(false);
            BirdsPoliceChase();
            yield return new WaitForSeconds(1.8f);
            BirdsPoliceMove();
        }

        public void IncreaseLevelsDone()
        {
            _doneCount++;
        }

        public void EnableDupCollider(Vector3 pos)
        {
            _dupColider.transform.localPosition = pos;
            _dupColider.enabled = true;
        }

        public void DisableDupCollider()
        {
            _dupColider.enabled = false;
        }

        public void SpawnParticle(Vector3 pos)
        {
            Instantiate(_fillParticle, pos, Quaternion.identity);
        }

        public void AnimalFail()
        {
            _animal.GetComponent<Animator>().SetTrigger("Fail");
        }
        public static void Vibrate()
        {
            Vibration.Vibrate(40);
            print("vibrate");
        }
    }
}
