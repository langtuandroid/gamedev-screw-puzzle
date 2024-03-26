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
        
        [SerializeField] private Modes gamemodes;
        
        [Header("Bolt Shifting")] 
        [SerializeField] private CircleCollider2D _dupColider;
        [SerializeField] private int totalcount;
        private int _doneCount;
        [SerializeField] private GameObject _fillParticle;

        [Header("ANIMAL")] 
        [SerializeField] private GameObject Animal;
        [SerializeField] private GameObject thief;
        [SerializeField] private GameObject player;
        [SerializeField] private SplineComputer playerspline;

        [Header("Birds or cats")]
        [SerializeField] private bool Birds;
        [SerializeField] private bool cats;
        [SerializeField] private bool Dogs,Rabbit,Fox,Zebra,Dear,Flamingo;
        [SerializeField] private List<GameObject> birds;
        [SerializeField] private List<DOTweenAnimation> birdsanim;
        [SerializeField] private List<GameObject> birds2;
        [SerializeField] private List<DOTweenAnimation> birdsanim2;
        
        [SerializeField] private GameObject cagedoor;
        [SerializeField] private GameObject policeMan;
        [SerializeField] private GameObject sleepeffect;
        private bool _once;
        private bool _fail;
        [Inject] private Finish _finish;
        [Inject] private AudioManager _audioManager;
        [Inject] private UIManager _uiManager;
        [Inject] private CameraMove _cameraMove;
        [Inject] private Board _board;
        [Inject] private Wednesday _wednesday;
        [Inject] private KingKong _kingKong;
        public SplineComputer PlayerSpline => playerspline;
        public Modes GameMode => gamemodes;
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
            if (totalcount == _doneCount /*|| test*/)
            {
                if (!_uiManager.Win && gamemodes == Modes.Null)
                {
                    Win();
                }
            
                switch (gamemodes)
                {
                    case Modes.BirdsCats or Modes.Dogs:
                    {
                        if (totalcount == _doneCount && !_once)
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

                switch (gamemodes)
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
            Animal.gameObject.GetComponent<Animator>().SetTrigger("Walk");
            Animal.transform.DOMove(new Vector3(0, Animal.transform.position.y, 0.5f), 1.5f).SetEase(Ease.Linear);
            DOVirtual.DelayedCall(1.25f, () =>
            {
                Animal.transform.DOLocalRotate(new Vector3(0, 0, -65), 0.1f, RotateMode.LocalAxisAdd)
                    .SetEase(Ease.Linear);
            });
            DOVirtual.DelayedCall(1.5f,() =>
            {
            
                Animal.transform.DOMove(new Vector3(thief.transform.localPosition.x-0.5f, Animal.transform.position.y, thief.transform.localPosition.z+1.5f), 1f).SetEase(Ease.Linear).OnUpdate(
                    () =>
                    {
                        Animal.gameObject.GetComponent<Animator>().SetTrigger("Attack");
                        player.GetComponent<Animator>().SetTrigger("Run");
                        player.GetComponent<SplineFollower>().enabled = true;
                        player.GetComponent<SplineFollower>().spline = playerspline;
                        DOVirtual.DelayedCall(0.4f, () =>
                        {
                            thief.GetComponent<Animator>().SetTrigger("Fall");
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
            if (gamemodes == Modes.Kingkong)
            {
                cagedoor.GetComponent<Rigidbody>().isKinematic = false;
                _kingKong.KingKongFun();
            }

            else
            {
                _audioManager.Play("Door");
                cagedoor.transform.DOLocalRotate(new Vector3(0, -120f, 0), 1f,RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(
                    () =>
                    {
                        if (gamemodes == Modes.Tiger)
                        {
                            _audioManager.Play("Cheetah");
                        }
                        AnimalAnimation();
                
                    });
            }
        }
        public void AnimalAnimation()
        {
            if (gamemodes == Modes.Tiger)
            {
                TigerAttack();
            }
            if (gamemodes==Modes.BirdsCats || gamemodes==Modes.Dogs)
            {
                BirdsCats();
            }
        
            if (gamemodes == Modes.Wednesday)
            {
                _wednesday.WednesdayDone();
                print("Wednesday");
            }

            if (gamemodes==Modes.Elephant)
            {
                Animal.GetComponent<Animator>().SetTrigger("Run");
            
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    _audioManager.Play("Elephant");
                    Animal.GetComponent<DOTweenAnimation>().DOPlay();
                });
            }
            if (gamemodes == Modes.Pig)
            {
                Animal.GetComponent<Animator>().SetTrigger("Run");
                Animal.transform.DOMoveY(Animal.transform.position.y-1.5f,0.02f);
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    _audioManager.Play("Pig");
                    Animal.GetComponent<DOTweenAnimation>().DOPlay();
                });
            }
            if (gamemodes == Modes.KingKong1)
            {
                Animal.GetComponent<Animator>().SetTrigger("Run");
                Animal.transform.DOMoveY(Animal.transform.position.y-0.5f,0.02f);
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    Animal.GetComponent<DOTweenAnimation>().DOPlay();
                });
            }

        }

        private void BirdsCats()
        {
            if (Birds)
            {
                _audioManager.Play("Birds");
            }

            if (cats)
            {
                _audioManager.Play("Cats");
            }

            if (Dogs)
            {
                _audioManager.Play("Dogs");
            }

            if (Flamingo)
            {
                _audioManager.Play("Flamingo");
            }

            if (Rabbit)
            {
                _audioManager.Play("Rabbit");
            }

            if (Zebra)
            {
                _audioManager.Play("Zebra");
            }
            
            foreach (var bird in birds)
            {
                bird.GetComponent<Animator>().SetTrigger("Fly");
                    
                if (Birds)
                {
                    bird.transform
                        .DOMove(
                            new Vector3(Random.Range(-1.14f, 1.14f),
                                Random.Range(-4f, 0f), Random.Range(-1f, -0.3f)),
                            Random.Range(0.3f, 0.7f)).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            foreach (var t in birdsanim)
                            {
                                t.DOPlay();
                            }
                        });
                    if (birds2.Count == 0)
                    {
                        StartCoroutine(PoliceChaseWait());
                    }
                }
                if (cats || Dogs || Rabbit || Fox || Zebra || Dear || Flamingo)
                {
                    bird.transform
                        .DOMove(
                            new Vector3((float)UnityEngine.Random.Range(-1.14f, 1.14f),
                                (float)UnityEngine.Random.Range(-4f, -3f), UnityEngine.Random.Range(-1f, -0.3f)),
                            UnityEngine.Random.Range(0.3f, 0.7f)).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            foreach (var t in birdsanim)
                            {
                                t.DOPlay();
                            }
                        });
                    if (birds2.Count == 0)
                    {
                        StartCoroutine(PoliceChaseWait());
                    }
                }
            }

            if (Birds)
            {
                _audioManager.Play("Birds");
            }
            foreach (var bird in birds2)
            {
                bird.GetComponent<Animator>().SetTrigger("Fly");
                    
                if (Birds)
                {
                    bird.transform
                        .DOMove(
                            new Vector3((float)UnityEngine.Random.Range(-1.14f, 1.14f),
                                (float)UnityEngine.Random.Range(-4f, 0f), UnityEngine.Random.Range(-1f, -0.3f)),
                            UnityEngine.Random.Range(0.5f, 1f)).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            foreach (var t in birdsanim2)
                            {
                                t.DOPlay();
                            }
                        });
                    StartCoroutine(PoliceChaseWait());
                }
                if (cats || Dogs || Rabbit || Fox || Zebra || Dear || Flamingo)
                {
                    bird.transform
                        .DOMove(
                            new Vector3((float)UnityEngine.Random.Range(-1.14f, 1.14f),
                                (float)UnityEngine.Random.Range(-4f, -3f), UnityEngine.Random.Range(-1f, -0.3f)),
                            UnityEngine.Random.Range(0.5f, 1f)).SetEase(Ease.Linear).OnComplete(() =>
                        {
                            foreach (var t in birdsanim2)
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
            policeMan.GetComponent<Animator>().SetTrigger("Chase");
        }

        private void BirdsPoliceMove()
        {
            _audioManager.Play("Police");
            policeMan.GetComponent<DOTweenAnimation>().DOPlay();
            DOVirtual.DelayedCall(3f, () =>
            {
                Win();
            });
        }

        private IEnumerator PoliceChaseWait()
        {
            sleepeffect.SetActive(false);
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
            Animal.GetComponent<Animator>().SetTrigger("Fail");
        }
        public static void Vibrate()
        {
            Vibration.Vibrate(40);
            print("vibrate");
        }
    }
}
