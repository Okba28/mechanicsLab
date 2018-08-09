using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectScript : MonoBehaviour {

    public Transform follow;        //the object that should be followed

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = follow.position;
	}
}
