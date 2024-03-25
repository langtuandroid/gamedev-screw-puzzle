using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class maskcheck : MonoBehaviour
{
    //public CircleCollider2D boltcoll;
    // Start is called before the first frame update
    void Start()
    {
        //boltcoll = GetComponent<CircleCollider2D>();
    }

    
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("MASk"))
        {
            gameObject.tag = "Fill";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MASk"))
        {
            gameObject.tag = "Untagged";
        }
    }
}
