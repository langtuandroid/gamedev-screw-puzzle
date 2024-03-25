using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Dreamteck.Splines;
using Unity.VisualScripting;
using UnityEngine;

public class Wednesday : MonoBehaviour
{
    public static Wednesday instance;
    
    public GameObject Hand;
    public Transform HandPos;
    public GameObject Player;
    public GameObject thief;
    public Transform thiefpos;
    public Transform ThiefFinalPos;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void weddone()
    {
        var seq = DOTween.Sequence();
        seq.Append(Hand.transform.parent.DORotate(new Vector3(0, 30f, 0f), 0.4f, RotateMode.WorldAxisAdd).SetEase(Ease.Linear));
        seq.AppendCallback(() =>
        {
            Hand.GetComponent<Animator>().SetTrigger("Run");
            seq.Append(Hand.transform.parent.DOMove(new Vector3(0, -1.2f, 0f), 1f).SetEase(Ease.Linear).OnComplete(
                () =>
                {
                    Hand.GetComponent<Animator>().SetTrigger("Punch");
                    DOVirtual.DelayedCall(1.5f, () =>
                    {
                        if (AudioManager.instance)
                        {
                            AudioManager.instance.Play("Punch");
                        }
                    });
                    seq.Append(Hand.transform.parent.DORotate(new Vector3(0, -30f, 0f), 0.1f, RotateMode.WorldAxisAdd)
                        .SetEase(Ease.Linear).OnComplete(() =>
                        {
                            seq.Append(Hand.transform.parent.DORotate(new Vector3(45f, -10f, 10f), 0.1f, RotateMode.WorldAxisAdd)
                                .SetEase(Ease.Linear));
                        }));
                }));
        });
        seq.AppendInterval(2.4f);
        seq.AppendCallback(() =>
        {
            //Hand.transform.parent.GetComponent<DOTweenAnimation>().DOPlay();
            Hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = true;
            thief.GetComponent<Animator>().SetTrigger("Backpunch");
            DOVirtual.DelayedCall(0.4f, () =>
            {
                Hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = false;
                DOVirtual.DelayedCall(2.5f,()=>
                {
                    Hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = true;
                    if (AudioManager.instance)
                    {
                        AudioManager.instance.Play("Punch");
                    }
                    DOVirtual.DelayedCall(0.4f, () =>
                    {
                        Hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = false;
                        DOVirtual.DelayedCall(1f, () =>
                        {
                            Hand.transform.parent.GetComponent<DOTweenVisualManager>().enabled = true;
                            if (AudioManager.instance)
                            {
                                AudioManager.instance.Play("Punch");
                            }
                        });
                    });
                });
            });
            
        });
        seq.AppendInterval(5f);
        seq.AppendCallback(() =>
        {
            Player.GetComponent<SplineFollower>().enabled = true;
            Player.GetComponent<SplineFollower>().spline = GameManager.instance.playerspline;
            Player.GetComponent<Animator>().SetTrigger("Run");
            //Player.GetComponent<SplineFollower>().follow = true;
            Hand.transform.parent.DORotate(new Vector3(82, 120, 240), 0.5f);
            Hand.transform.parent
                .DOScale(
                    new Vector3(Hand.transform.parent.localScale.x - 30f, Hand.transform.parent.localScale.y - 30f,
                        Hand.transform.parent.localScale.z - 30f), 0.2f).SetEase(Ease.Linear);
            Hand.transform.parent.DOJump(ThiefFinalPos.position, 4f, 1, 0.3f).OnComplete(() =>
            {
                if (AudioManager.instance)
                {
                    AudioManager.instance.Play("Punch");
                }
            });
            DOVirtual.DelayedCall(0.4f, () =>
            {
                thief.GetComponent<Animator>().SetTrigger("Cpunch");
                Hand.GetComponent<Animator>().SetTrigger("Finalpunch");
            });
            DOVirtual.DelayedCall(2.4f, () =>
            {
                Hand.transform.parent.DOJump(thief.transform.position, 4f, 1, 1f).SetEase(Ease.Linear);
                Hand.GetComponent<Animator>().SetTrigger("Run");
                thief.GetComponent<Animator>().SetTrigger("TRY");
                
            });
            DOVirtual.DelayedCall(2.2f,() =>
            {
                Hand.transform.parent.DORotate(new Vector3(11, -105, 100-110), 0.3f).SetEase(Ease.Linear).OnComplete(
                    () =>
                    {
                        DOVirtual.DelayedCall(0.5f, () =>
                        {
                            Hand.transform.parent.DOMove(Player.transform.position, 3f).SetEase(Ease.Linear).OnComplete(
                                () =>
                                {
                                    if (!UIManager.INSTANCE.win)
                                    {
                                        GameManager.instance.winning();
                                    }
                                });
                            thief.GetComponent<Animation>().enabled = false;
                           
                        });

                    });
                
                /*DOVirtual.DelayedCall(0.2f, (() =>
                {
                    Hand.transform.parent.DOMove(Player.transform.position, 3f).SetEase(Ease.Linear);
                }));*/
            });
            
        });

       
    }
    public bool _dead;
    public void thiefJerk()
    {
        if (!_dead)
        {
            thief.GetComponent<Animator>().SetTrigger("Cpunch");
            if (AudioManager.instance)
            {
                AudioManager.instance.Play("Punch");
            }
        }
        else
        {
            thief.GetComponent<Animator>().SetTrigger("TRY");
        }
        
    }

    public void dead()
    {
        thief.GetComponent<Animator>().SetTrigger("TRY");
    }
    /*public void weddone()
    {
        /*var seq = DOTween.Sequence();
        seq.Append(Hand.transform.DOMove(new Vector3(0, -2.5f, 0), 2f).SetEase(Ease.Linear));
        seq.AppendCallback(() =>
        {
            Hand.GetComponent<Animator>().SetTrigger("Run");

        });
        //seq.AppendInterval(0.1f);
        seq.AppendCallback(() =>
        {
            Hand.GetComponent<DOTweenAnimation>().DOPlay();
        });#1#
        
        Hand.GetComponent<Animator>().SetTrigger("Run");
        Hand.transform.DOMove(new Vector3(0, -2.5f, 0), 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Hand.GetComponent<DOTweenAnimation>().DOPlay();
        });
        /*Hand.transform.DOMove(HandPos.position, 2f).SetEase(Ease.Linear).OnComplete(() =>
        {
            Hand.GetComponent<DOTweenAnimation>().DOPlay();
        });#1#
    }

    public void thiefdown()
    {
        Hand.GetComponent<Animator>().SetTrigger("Punch");
        //Hand.transform.DOMove(new Vector3(thiefpos.position.x,thiefpos.position.y+3f,thiefpos.position.z), 0.3f).SetEase(Ease.Linear);
        thief.GetComponent<Animator>().SetTrigger("Backpunch");
        DOVirtual.DelayedCall(2f, () =>
        {
            Hand.GetComponent<Animator>().SetTrigger("Punch2");
            DOVirtual.DelayedCall(1f, () =>
            {
                Hand.GetComponent<Animator>().SetTrigger("Punch2");
                Player.GetComponent<SplineFollower>().spline = GameManager.instance.playerspline;
                Player.GetComponent<Animator>().SetTrigger("Run");
                Player.GetComponent<SplineFollower>().enabled = true;
                Player.GetComponent<SplineFollower>().follow = true;
                DOVirtual.DelayedCall(2f,() =>
                {
                    /*Hand.transform
                        .DOMove(
                            new Vector3(thief.transform.localPosition.x, thief.transform.localPosition.y + 1f,
                                thief.transform.localPosition.z), 1f).SetEase(Ease.Linear);
                                #1#
                    Hand.transform
                        .DOScale(
                            new Vector3(Hand.transform.localScale.x - 25f, Hand.transform.localScale.y - 25f,
                                Hand.transform.localScale.z - 25f), 0.5f).SetEase(Ease.Linear);
                    Hand.transform.DORotate(new Vector3(75, -95, 60), 0.5f).SetEase(Ease.Linear);

                    Hand.transform.DOJump(new Vector3(ThiefFinalPos.transform.localPosition.x,
                        ThiefFinalPos.transform.localPosition.y ,
                        ThiefFinalPos.transform.localPosition.z), 5f, 1, .25f).SetEase(Ease.Linear).OnComplete(() =>
                    {
                        thief.GetComponent<Animator>().SetTrigger("Cpunch");
                        Hand.GetComponent<Animator>().SetTrigger("Finalpunch");
                        DOVirtual.DelayedCall(2.2f, () =>
                        {
                            _dead = true;
                            Hand.transform.DORotate(new Vector3(-5f, -95f, 10f), 0.5f).SetEase(Ease.Linear);

                            Hand.GetComponent<Animator>().SetTrigger("LastPunch");
                            Hand.transform.DOJump(thief.transform.position, 3, 1, .5f).SetEase(Ease.Linear).OnComplete(
                                () =>
                                {
                                    Hand.transform.DOMove(Player.transform.position, 5f).SetEase(Ease.Linear);
                                });
                            Hand.GetComponent<Animator>().SetTrigger("Run");
                        });
                    });
                   
                });
            });
            
        });
        
    }*/

    
}
