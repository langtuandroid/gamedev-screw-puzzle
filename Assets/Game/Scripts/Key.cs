using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Key : MonoBehaviour
{
    public static Key instance;

    public GameObject locking;

    public GameManager gamemanager;
    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gamemanager=GameManager.instance;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("CUBE"))
        {
            gameObject.GetComponent<Collider>().enabled = false;
            gameObject.transform.GetComponentInChildren<DOTweenAnimation>().DOComplete();
            if (AudioManager.instance)
            {
                gamemanager.vibration();
                AudioManager.instance.Play("Key");
                gamemanager.keycollect = true;
            }
            gameObject.transform.DOMove(locking.gameObject.transform.position, 0.5f).OnComplete(() =>
            {
                transform.GetComponentInChildren<MeshRenderer>().enabled = false;
                if (AudioManager.instance)
                {
                    AudioManager.instance.Play("Lock");
                    gamemanager.vibration();
                }
                locking.GetComponentInChildren<DOTweenAnimation>().DOPlay();
                if (!locking.transform.GetComponentInChildren<ParticleSystem>().isPlaying)
                {
                    locking.transform.GetComponentInChildren<ParticleSystem>().Play();
                }
            
            });
        }
       
    }

    public void Locked()
    {
        locking.transform.parent.GetChild(1).tag = "BOLT";
        transform.gameObject.SetActive(false);
        locking.transform.SetParent(null);
        locking.GetComponent<Rigidbody>().isKinematic = false;
    }
    
}
