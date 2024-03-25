using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.U2D.IK;

public class KingKong : MonoBehaviour
{
    public static KingKong instance;
    
    public List<GameObject> chains;
    public List<Rigidbody> chainsRigid;

    public bool chainblast;
    public GameObject girl;
    public GameObject dupgirl;
    public GameObject hand;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (chainblast)
        {
            for (int i = 0; i < chainsRigid.Count; i++)
            {
                chains[i].transform.SetParent(null);
                chainsRigid[i].isKinematic = false;
                //chainsRigid[i].AddForce(Vector3.forward*UnityEngine.Random.Range(1,2),ForceMode.Force);
                chainsRigid[i]
                    .AddExplosionForce(5f, chainsRigid[i].transform.position, 1f, 0.1f, ForceMode.Impulse);
            }
        }
    }

    public void kingkongfun()
    {
        var seq = DOTween.Sequence();
        seq.AppendCallback(() =>
        {
            GetComponent<Animator>().SetTrigger("Roar");
            if (AudioManager.instance)
            {
                AudioManager.instance.Play("Chain");
            }
        });
        seq.AppendInterval(2.3f);
        seq.AppendCallback(() =>
        {
            chainblast = true;
        });
    }

    public void girlposchange()
    {
        if (AudioManager.instance)
        {
            AudioManager.instance.Play("Girl");
        }
        girl.transform.DOLocalMove(dupgirl.transform.localPosition, 0.1f).SetEase(Ease.Linear);
        girl.transform.DOLocalRotateQuaternion(dupgirl.transform.localRotation,0.1f).SetEase(Ease.Linear);
        girl.transform.DOScale(dupgirl.transform.localScale,0.1f).SetEase(Ease.Linear);
        girl.SetActive(false);
        dupgirl.SetActive(true);
        dupgirl.transform.SetParent(hand.transform);
        /*DOVirtual.DelayedCall(0.1f,() =>
        {
            girl.transform.SetParent(hand.transform);
        });*/
        DOVirtual.DelayedCall(2.5f, () =>
        {
            transform.GetComponent<DOTweenAnimation>().DOPlay();
            if (!UIManager.INSTANCE.win)
            {
                DOVirtual.DelayedCall(3f, () =>
                {
                    GameManager.instance.winning();
                });
            }
        });

    }
}
