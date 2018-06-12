using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script attached to the masses in the moments scene allowing the position of the masses to be changed
// Although scene is 3D, only allow movement of the masses in the y-z plane. Fixing their x position to be the same as the 
// plank on which they are being placed.
public class PositionScriptMoments : MonoBehaviour
{

    public GameObject plank;          //the plank in the scene to which the x coord must be fixed
    bool handleClicked;
    public float offset_y;
    public float offset_z;

    private BoxCollider col;           //the collider attached to mass gameobject
    // Use this for initialization
    void Start()
    {
        handleClicked = false;
        plank = GameObject.FindGameObjectWithTag("plank") as GameObject;
        col = transform.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && handleClicked == true)
        {
            //disable the collider so that things don't get knocked around whilst moving the mass
            col.enabled = false;
            float new_y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;
            float new_z = Camera.main.ScreenToWorldPoint(Input.mousePosition).z;

            Vector3 newPosition = new Vector3(plank.transform.position.x, new_y - offset_y, new_z - offset_z);
            transform.position = newPosition;
        }
        else
        {
            // renable the collider if the button is not clicked
            col.enabled = true;
        }

    }

    private void OnMouseDown()      //when the Tranform to which this script is attached is clicked
    {
        handleClicked = true;
    }

    private void OnMouseUp()
    {
        handleClicked = false;
    }
}
