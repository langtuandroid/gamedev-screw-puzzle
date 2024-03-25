using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

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
    public Modes gamemodes;
    
    public enum State
    {
        Null,
        Idle,
        Select,
        Done,
    }
    public State gamestate;
    
    [Header("Bolt Ui Move")] public List<GameObject> selected;
    public List<GameObject> doneObjects;

    public GameObject uiimage;

    [Header("Bolt Shifting")] public List<GameObject> selectedBolt;
    public GameObject dupPlug;
    public GameObject dupcolider;

    public int totalcount;
    public int donecount;

    /*public GameObject keyparticle;
    public GameObject lockparticle;*/

    public GameObject fillPartical;

    [Header("ANIMAL")] public bool animal;
    public GameObject Animal;
    public GameObject AnimalMovePos;
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
    public bool keycollect;
    public bool test;
    public GameObject policeMan;
    public GameObject sleepeffect;

    public bool Fail;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gamestate = State.Done;
        if (Board.instance)
        {
            transform.position = Board.instance.transform.position;
        }
        dupcolider.GetComponent<CircleCollider2D>().enabled = false;
        Vibration.Init();
    }
    public bool once;
    void Update()
    {
        if (totalcount == donecount /*|| test*/)
        {
            if (!UIManager.INSTANCE.win && gamemodes==Modes.Null)
            {
                winning();
            }
            
            if (gamemodes == Modes.BirdsCats || gamemodes==Modes.Dogs)
            {
                /*if (test && !once)
                {
                    once = true;
                    Cameramove.Instance.SecondChange();
                }*/

                if (totalcount == donecount && !once)
                {
                    Cameramove.Instance.SecondChange();
                    once = true;
                    //keycollect = false;
                    //AnimalAnimation();
                }
            }
            
            if ((gamemodes == Modes.Tiger || gamemodes == Modes.Elephant || gamemodes==Modes.Pig) && !once)
            {
                Cameramove.Instance.SecondChange();
                //TigerAttack();
                once = true;
                DOVirtual.DelayedCall(4.2f, () =>
                {
                    StartCoroutine(policechasewait());
                });

            }

            if (gamemodes == Modes.Wednesday && !once)
            {
                Cameramove.Instance.SecondChange();
                once = true;
            }

            if (gamemodes == Modes.Kingkong && !once)
            {
                once = true;
                Cameramove.Instance.SecondChange();
            }
            if (gamemodes == Modes.KingKong1 && !once)
            {
                once = true;
                Cameramove.Instance.SecondChange();
                DOVirtual.DelayedCall(4.2f, () =>
                {
                    if (AudioManager.instance)
                    {
                        AudioManager.instance.Play("KingKong");
                    }
                    StartCoroutine(policechasewait());
                });
            }
            
            /*if ((gamemodes != Modes.Null && !UIManager.INSTANCE.win) && !once)
            {
                once = true;
                Cameramove.Instance.SecondChange();
                
                DOVirtual.DelayedCall(4.5f, () =>
                {
                    StartCoroutine(policechasewait());
                });
            }*/
            
        }
        
        if (Fail && !once)
        {
            Cameramove.Instance.fail();
            once = true;
        }

    }
    public void winning()
    {
        Finish.instance.blast.Play();
        AudioManager.instance.Play("Win");
        UIManager.INSTANCE.win = true;
        UIManager.INSTANCE.Winpanel();
    }
    
    public void TigerAttack()
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
        if (!UIManager.INSTANCE.win)
        {
            DOVirtual.DelayedCall(5f, () =>
            {
                winning();
            });
        }
    }
    
    public void DoorOpen()
    {
        //cagedoor.GetComponent<DOTweenAnimation>().DOPlay();
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
            birdscats();
        }
        
        if (gamemodes == Modes.Wednesday)
        {
            Wednesday.instance.weddone();
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
            //seq.AppendInterval(0.3f);
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

    public void birdscats()
    {
        if (birds.Count != null)
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
                for (int i = 0; i < birds.Count; i++)
                {
                    birds[i].GetComponent<Animator>().SetTrigger("Fly");
                    
                    if (Birds)
                    {
                        birds[i].transform
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
                            StartCoroutine(policechasewait());
                        }
                    }
                    if (cats || Dogs || Rabbit || Fox || Zebra || Dear || Flamingo)
                    {
                        birds[i].transform
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
                            StartCoroutine(policechasewait());
                        }
                    }
                }
            }
            if (birds2.Count != null)
            {
                if (AudioManager.instance)
                {
                    if (Birds)
                    {
                        AudioManager.instance.Play("Birds");
                    }

                    /*if (cats)
                    {
                        AudioManager.instance.Play("Cats");
                    }*/
                }
                for (int i = 0; i < birds2.Count; i++)
                {
                    birds2[i].GetComponent<Animator>().SetTrigger("Fly");
                    
                    if (Birds)
                    {
                        birds2[i].transform
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
                        StartCoroutine(policechasewait());
                    }
                    if (cats || Dogs || Rabbit || Fox || Zebra || Dear || Flamingo)
                    {
                        birds2[i].transform
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
                        StartCoroutine(policechasewait());
                    }
                }
            }
            keycollect = false;
    }
    

    public void BirdsPoliceChase()
    {
        
        policeMan.GetComponent<Animator>().SetTrigger("Chase");
    }

    public void BirdsPolicemove()
    {
        if (AudioManager.instance)
        {
            AudioManager.instance.Play("Police");
        }
        policeMan.GetComponent<DOTweenAnimation>().DOPlay();
        DOVirtual.DelayedCall(3f, () =>
        {
            winning();
        });
    }
    IEnumerator policechasewait()
    {
        sleepeffect.SetActive(false);
        BirdsPoliceChase();
        yield return new WaitForSeconds(1.8f);
        BirdsPolicemove();
    }
    public void vibration()
    {
        Vibration.Vibrate(40);
        print("vibrate");
    }
}
