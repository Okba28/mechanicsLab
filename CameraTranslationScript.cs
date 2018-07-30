using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script should allow the camera to be moved in the x-z plane (not y, that is done by zoom script by changing the size of the 
// orthographic camera).
// This version has been adapted for the mechanics lab momentum scene
// WASD keys are used to move the camera around
public class CameraTranslationScript : MonoBehaviour {

    public Camera cam;                  // The main scene camera

    public float camShiftDelta = 0.3f;         //the amount to shift the camera by on each update when key pressed
    float cam_y;
	// Use this for initialization
	void Start () {
        cam_y = cam.transform.position.y;       //the height/y coord of the camera, which shouldn't be changed
	}
	
	// Update is called once per frame
	void Update () {
        //If any of the WASD directional keys are held then move the camera in the corresponding direction
        //Limit the movement
        if (Input.GetAxis("Vertical") > 0)
        {
            float current_z = cam.transform.position.z; //get current z position
            float new_z = current_z + camShiftDelta;               // get new position
            cam.transform.position = new Vector3(cam.transform.position.x, cam_y, new_z);   //update camera position
        }

        if (Input.GetAxis("Vertical") < 0)
        {
            float current_z = cam.transform.position.z; //get current z position
            float new_z = current_z - camShiftDelta;               // get new position
            cam.transform.position = new Vector3(cam.transform.position.x, cam_y, new_z);   //update camera position
        }

        if (Input.GetAxis("Horizontal") < 0)
        {
            float current_x = cam.transform.position.x; //get current z position
            float new_x = current_x - camShiftDelta;               // get new position
            cam.transform.position = new Vector3(new_x, cam_y, cam.transform.position.z);   //update camera position
        }

        if (Input.GetAxis("Horizontal") > 0)
        {
            float current_x = cam.transform.position.x; //get current z position
            float new_x = current_x + camShiftDelta;               // get new position
            cam.transform.position = new Vector3(new_x, cam_y, cam.transform.position.z);   //update camera position
        }
	}


    
}
