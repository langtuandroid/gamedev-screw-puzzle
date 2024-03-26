using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;

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
        
        public static GameManager instance;
        [SerializeField] private Modes gamemodes;
      
        [Header("Bolt Shifting")] 
        public GameObject dupcolider;

        public int totalcount;
        private int _doneCount;
        public GameObject fillPartical;

        [Header("ANIMAL")] 
        public GameObject Animal;
        public GameObject thief;
        public GameObject player;
        public SplineComputer playerspline;

        [Header("Birds or cats")]
        public bool Birds;
        public bool cats;
        public bool Dogs,Rabbit,Fox,Zebra,Dear,Flamingo;
        public List<GameObject> birds;
        public List<DOTweenAnimation> birdsanim;
        public List<GameObject> birds2;
        public List<DOTweenAnimation> birdsanim2;
        
        public GameObject cagedoor;
        public GameObject policeMan;
        public GameObject sleepeffect;
        private bool _once;
        private bool _fail;
        public Modes GameMode => gamemodes;
        public State GameState { get; set; }
        public GameObject DupPlug { get; set; }
     
        private void Awake()
        {
            instance = this;
        }

        void Start()
        {
            GameState = State.Done;
            if (Board.instance)
            {
                transform.position = Board.instance.transform.position;
            }
            dupcolider.GetComponent<CircleCollider2D>().enabled = false;
            Vibration.Init();
        }
        
        void Update()
        {
            if (totalcount == _doneCount /*|| test*/)
            {
                if (!UIManager.instance.Win && gamemodes==Modes.Null)
                {
                    Win();
                }
            
                if (gamemodes == Modes.BirdsCats || gamemodes==Modes.Dogs)
                {
                    if (totalcount == _doneCount && !_once)
                    {
                        CameraMove.Instance.SecondChange();
                        _once = true;
                    }
                }
            
                if ((gamemodes == Modes.Tiger || gamemodes == Modes.Elephant || gamemodes==Modes.Pig) && !_once)
                {
                    CameraMove.Instance.SecondChange();
                    _once = true;
                    DOVirtual.DelayedCall(4.2f, () =>
                    {
                        StartCoroutine(PoliceChaseWait());
                    });

                }

                if (gamemodes == Modes.Wednesday && !_once)
                {
                    CameraMove.Instance.SecondChange();
                    _once = true;
                }

                if (gamemodes == Modes.Kingkong && !_once)
                {
                    _once = true;
                    CameraMove.Instance.SecondChange();
                }
                if (gamemodes == Modes.KingKong1 && !_once)
                {
                    _once = true;
                    CameraMove.Instance.SecondChange();
                    DOVirtual.DelayedCall(4.2f, () =>
                    {
                        if (AudioManager.instance)
                        {
                            AudioManager.instance.Play("KingKong");
                        }
                        StartCoroutine(PoliceChaseWait());
                    });
                }
            }
        
            if (_fail && !_once)
            {
                CameraMove.Instance.Fail();
                _once = true;
            }

        }
        public void Win()
        {
            Finish.instance.PlayBlastParticle();
            AudioManager.instance.Play("Win");
            UIManager.instance.Win = true;
            UIManager.instance.WinPanel();
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
            if (!UIManager.instance.Win)
            {
                DOVirtual.DelayedCall(5f, () =>
                {
                    Win();
                });
            }
        }
    
        public void DoorOpen()
        {
            if (gamemodes == Modes.Kingkong)
            {
                cagedoor.GetComponent<Rigidbody>().isKinematic = false;
                KingKong.instance.kingkongfun();
            }

            else
            {
                if (AudioManager.instance)
                {
                    AudioManager.instance.Play("Door");
                }
                cagedoor.transform.DOLocalRotate(new Vector3(0, -120f, 0), 1f,RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(
                    () =>
                    {
                        if (gamemodes == Modes.Tiger)
                        {
                            AudioManager.instance.Play("Cheetah");
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
                Wednesday.instance.WednesdayDone();
                print("Wednesday");
            }

            if (gamemodes==Modes.Elephant)
            {
                Animal.GetComponent<Animator>().SetTrigger("Run");
            
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    if (AudioManager.instance)
                    {
                        AudioManager.instance.Play("Elephant");
                    }
                    Animal.GetComponent<DOTweenAnimation>().DOPlay();
                });
            }
            if (gamemodes == Modes.Pig)
            {
                Animal.GetComponent<Animator>().SetTrigger("Run");
                Animal.transform.DOMoveY(Animal.transform.position.y-1.5f,0.02f);
                DOVirtual.DelayedCall(0.3f, () =>
                {
                    if (AudioManager.instance)
                    {
                        AudioManager.instance.Play("Pig");
                    }
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
            if (AudioManager.instance)
            {
                if (Birds)
                {
                    AudioManager.instance.Play("Birds");
                }

                if (cats)
                {
                    AudioManager.instance.Play("Cats");
                }

                if (Dogs)
                {
                    AudioManager.instance.Play("Dogs");
                }

                if (Flamingo)
                {
                    AudioManager.instance.Play("Flamingo");
                }

                if (Rabbit)
                {
                    AudioManager.instance.Play("Rabbit");
                }

                if (Zebra)
                {
                    AudioManager.instance.Play("Zebra");
                }

                   
            }
            foreach (var bird in birds)
            {
                bird.GetComponent<Animator>().SetTrigger("Fly");
                    
                if (Birds)
                {
                    bird.transform
                        .DOMove(
                            new Vector3((float)UnityEngine.Random.Range(-1.14f, 1.14f),
                                (float)UnityEngine.Random.Range(-4f, 0f), UnityEngine.Random.Range(-1f, -0.3f)),
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

            if (AudioManager.instance)
            {
                if (Birds)
                {
                    AudioManager.instance.Play("Birds");
                }
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
            if (AudioManager.instance)
            {
                AudioManager.instance.Play("Police");
            }
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
        public void Vibrate()
        {
            Vibration.Vibrate(40);
            print("vibrate");
        }
    }
}
