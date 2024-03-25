using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Bolt : MonoBehaviour
{
    public static Bolt instance;

    public enum states
    {
        Null,
        idle,
        select,
        done
    }
    public states boltstate;
    public Transform originalTransform;
    public Vector3 pos;
    
    public List<CharacterJoint> connectedJoints;
    public CharacterJoint otherObject;
    public Vector3 offset;
    
    public bool touch;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        originalTransform = gameObject.transform;
        pos = transform.localPosition;
        //print(pos);
        //boltstate = states.idle;

    }

    
    void Update()
    {
       
    }
   
    private void OnMouseDrag()
    {
        if (boltstate == states.select)
        {
            transform.position = (Mousepos() + offset);
        }
    }

    private void OnMouseDown()
    {
        if (boltstate == states.select)
        {
            offset = transform.position - Mousepos();
        }
        
    }

    Vector3 Mousepos()
    {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
       
    }
}
