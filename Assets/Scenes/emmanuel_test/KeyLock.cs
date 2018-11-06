using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyLock : MonoBehaviour {

    //NoteToSelf: need to add a object in the inspector 
    public Gate gate;
    public Key key; 
    public Key keyb;
    
    
    


    void Start()
        
    {
        ///***gate = GetComponent<Gate>();
        
    }


    /* 
     * private void OnCollisionEnter(Collision other)
     {

         print("hey");
         Player player = other.gameObject.GetComponent<Player>();

         //key = other.GetComponent<Key>() ;



         if (player.haskey && other.gameObject.name == "Key") {


             Destroy(other.gameObject);


             player.haskey = false;




             gate.open();

         }

     }*/



    private void OnTriggerEnter(Collider other)
    {
        
    

        PlayerController player = other.gameObject.GetComponent<PlayerController>();

        //need to test condition for two keys
        
        if (player.gameObject.transform.childCount == 5) {

            Destroy(key.gameObject);
            Destroy(keyb.gameObject);
              
            //player.haskey = false;

            gate.isOpen = true;

            
              

        }

//
//        if ()
//        {
//            Destroy(key.gameObject);
//            Destroy(keyb.gameObject);
//
//            gate.isOpen = true;
//
//
//        }








    }





}
