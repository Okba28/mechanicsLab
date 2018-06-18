using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script gets the camera to follow a single transform - such as the star or planet in the orbits scene
//Camera should be set to a specific height in the y direction and rotate in the x to face the x-z plane
public class CameraFollowTransformScript : MonoBehaviour {

    public Transform toFollow;
    public Camera cam;
	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        cam.transform.position = new Vector3(toFollow.position.x, cam.transform.position.y, toFollow.position.z);
	}
}
