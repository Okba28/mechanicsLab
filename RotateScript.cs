using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Attached to a rotate handle on an object in order to rotate it around the x axis.
//rampAngle stores the current angle in degrees for easy output to the UI

public class RotateScript : MonoBehaviour {

    public Transform ramp;    //the attached ramp that will be rotated by this handle
    public Transform rampContainer; //the empty game object containing the ramp
    bool handleClicked;         //whether the mouse is clicking the rotate handle or not
    public float angleOffset = 0;
    private Quaternion rampRotation;
    public float deltaAngle;

    public Text angleText;          //the textbox showing the ramp angle

    
	// Use this for initialization
	void Start () {
        rampRotation = ramp.rotation;
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0) && handleClicked == true)
        {
            if(Input.GetAxis("Mouse Y") > 0)    //mouse moving up
            {
                deltaAngle += 0.5f;
            }
                else if(Input.GetAxis("Mouse Y") < 0 && ramp.position.y > 0)   //mouse moving down, limited to 0 degrees
            {
                deltaAngle -= 0.5f;
            }

            Quaternion deltaQuat = Quaternion.AngleAxis(deltaAngle, Vector3.right);
            rampContainer.rotation = rampRotation * deltaQuat;   //multiplying quaternions adds the rotations
            
            
            
            
        }
        Debug.Log("Angle of ramp = " + GetRampAngle());
        OutputRampAngle();  //output the ramp angle in degrees to the screen
         
	}

    private void OnMouseDown()
    {
        handleClicked = true;
    }

    private void OnMouseUp()
    {
        handleClicked = false;
    }

    //The ramp angle in world space is the sum of the ramp angle and the container that it is inside.
    public float GetRampAngle()
    {
        //float rampContainerAngle = Quaternion.Angle(rampContainer.rotation, Quaternion.identity);
        float rampAngle = Quaternion.Angle(ramp.rotation, Quaternion.identity);

        return rampAngle;
     
    }

    private void OutputRampAngle()
    {
        angleText.text = GetRampAngle().ToString("F1");
        angleText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(0,0,-1));
    }
}
