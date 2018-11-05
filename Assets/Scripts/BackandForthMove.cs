using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackandForthMove : MonoBehaviour {

    public float moveSpeed;
    public float moveDistance;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(Mathf.PingPong(Time.time * moveSpeed, moveDistance), transform.position.y, transform.position.z);
	}
}
