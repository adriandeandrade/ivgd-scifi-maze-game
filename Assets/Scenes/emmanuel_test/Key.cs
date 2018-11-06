using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Key : MonoBehaviour
{


   // public Key keyb;


    void OnTriggerEnter(Collider other)
    {

        PlayerController player = other.GetComponent<PlayerController>();

        if (player != null)
        {
            // a value that will hold wether or not we have a key(inside character controller)
            player.haskey = true;
            this.transform.parent = player.gameObject.transform;
            
            
            
        }        

    }


}
