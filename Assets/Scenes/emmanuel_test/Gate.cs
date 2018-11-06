using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

    //get the child of the game object 
    
    GameObject door;
    float doorCurrentPositionY, doorNewPositionY;

    public bool isOpen = false;

   

	// Use this for initialization
	void Start () {

        // get the door from the Gate child object
        door = this.gameObject.transform.GetChild(2).gameObject;
	
		//this.gameObject.transform.

        //a float to keep track of the door last position 
        doorCurrentPositionY = door.transform.position.y;

        print(doorCurrentPositionY);

        doorNewPositionY = door.transform.position.y + 1f;
    

	}
	
	// Update is called once per frame
	void Update () {
        if (isOpen)
        {
            openGate();
        }

        
		
	}

    //play animation
    public void openGate() {

        print("OPEN THE GATE!!!");


        if (doorCurrentPositionY < doorNewPositionY) {

           door.transform.Translate(new Vector3(0,0.1f,0) * Time.deltaTime * 20);


        }
        print("currentPosition" + doorCurrentPositionY);


        
    }

    private void moveDoor() {



    }
}
