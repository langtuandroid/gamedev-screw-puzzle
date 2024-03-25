using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class TouchBolt : MonoBehaviour
{
    public static TouchBolt instance;

    private RaycastHit _hit;
    [Header("Scripts")]
    public GameManager gameManager;
    
    private Vector3 offset;
    public Image BoltImage;

    public GameObject selectedBolt;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        gameManager=GameManager.instance;
    }

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out _hit))
            {
                if (_hit.collider.gameObject.CompareTag("BOLT"))
                {
                    var selectObject = _hit.collider.gameObject;
                    //boltfunction(selectObject);-----------------------Select and move object-----------
                    boltuifunction(selectObject);
                    
                }
            }
        }
    }

    public void boltuifunction(GameObject selectobj)
    {
        selectobj.GetComponent<Collider>().enabled = false;
        GameObject image = gameManager.uiimage;
        Vector3 movePos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(image.GetComponent<RectTransform>(), image.GetComponent<RectTransform>().position, Camera.main, out movePos);
        movePos.z = selectobj.transform.GetChild(0).transform.localPosition.z;
        
        selectobj.transform.GetChild(0).transform.DOMove(movePos, 2f).OnComplete(() =>
        {
            selectobj.transform.GetChild(0).gameObject.SetActive(false);
            selectobj.GetComponent<Bolt>().boltstate = Bolt.states.done;
           
        });
        //selectobj.transform.position = movePos;
       
        for (int i = 0; i < selectobj.GetComponent<Bolt>().connectedJoints.Count; i++)
        {
            //selectobj.GetComponent<Bolt>().connectedJoints[i].GetComponent<Collider>().isTrigger = true;
            Destroy(selectobj.GetComponent<Bolt>().connectedJoints[i]);
        }
    }
    
    public void boltfunction(GameObject selectedbolt)
    {
        if (gameManager.selected.Count == 0)
        {
            if (selectedbolt.GetComponent<Bolt>().boltstate != Bolt.states.select)
            {
                selectedbolt.transform.DOLocalMoveZ(selectedbolt.transform.localPosition.z - 0.013f, 0.3f);
                gameManager.selected.Add(selectedbolt);
                selectedbolt.GetComponent<Bolt>().boltstate = Bolt.states.select;
            }
        }
        if (gameManager.selected.Count == 1)
        {
            if ((selectedbolt.GetComponent<Bolt>().boltstate != Bolt.states.select))
            {
                gameManager.selected[0].transform
                    .DOLocalMove(gameManager.selected[0].GetComponent<Bolt>().pos, 0.3f).SetEase(Ease.Linear);
                selectedbolt.transform.DOLocalMoveZ(selectedbolt.transform.localPosition.z - 0.013f, 0.3f);
                gameManager.selected.RemoveAt(0);
                gameManager.selected.Add(selectedbolt);
                selectedbolt.GetComponent<Bolt>().boltstate = Bolt.states.select;
            }
        }
    }
}