using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Cameramove : MonoBehaviour
{
    public static Cameramove Instance;
    
    public Transform originalpos;
    public Transform changePos;
    public Transform FailCam;

    public DOTweenAnimation Animalanim;
    public Transform wedcam;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        //originalpos = transform;
        if ((GameManager.instance.gamemodes == GameManager.Modes.Wednesday))
        {
            transform.DOMove(wedcam.position, 2f).SetEase(Ease.Linear).OnComplete(() =>
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    FirstChange();
                });
            });
        }
        else
        {
            DOVirtual.DelayedCall(3f, () =>
            {
                FirstChange();
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstChange()
    {
        transform.DOMove(changePos.position, 2.5f).OnComplete(() =>
        {
            TouchDrop.instance.start = true;
        });
        transform.rotation = changePos.transform.rotation;
        GameManager.instance.test = true;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    public void SecondChange()
    {
        transform.DOMove(originalpos.position, 2.5f).OnComplete(() =>
        { 
            GameManager.instance.DoorOpen();
        });
        transform.rotation = originalpos.rotation;
    }

    public void fail()
    {
        /*GameManager.instance.Animal.GetComponent<Animator>().SetTrigger("Fail");
        GameManager.instance.Animal.transform.DORotate(new Vector3(0, -90, 0), 0.01f,RotateMode.LocalAxisAdd).SetEase(Ease.Linear).OnComplete(
            () =>
            {
                transform.DOMove(FailCam.position, 1.5f);
                transform.rotation = FailCam.rotation;
            });*/
        if (GameManager.instance.gamemodes == GameManager.Modes.Pig || GameManager.instance.gamemodes == GameManager.Modes.Kingkong )
        {
            transform.DOMove(FailCam.position, 1.5f).OnComplete(() =>
        {
            //Animalanim.DOPlay();
            GameManager.instance.Animal.GetComponent<Animator>().SetTrigger("Fail");
        });
        transform.rotation = FailCam.rotation;
            
        }
        /*transform.DOMove(FailCam.position, 1.5f).OnComplete(() =>
        {
            //Animalanim.DOPlay();
            GameManager.instance.Animal.transform.DORotate(new Vector3(0, -90, 0), 0.01f,RotateMode.LocalAxisAdd).SetEase(Ease.Linear);
            GameManager.instance.Animal.GetComponent<Animator>().SetTrigger("Fail");
        });
        transform.rotation = FailCam.rotation;*/
    }
}
