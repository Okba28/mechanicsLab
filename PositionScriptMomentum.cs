using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// Script attached to the position handles on movable objects
public class PositionScriptMomentum : MonoBehaviour
{

    public Transform parent;
    bool handleClicked;
    public float offset_x;
    public float offset_z;

    public Toggle twod;         //if selected then allow movement of pucks in 2d

    // Use this for initialization
    void Start()
    {
        handleClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && handleClicked == true && twod.isOn)
        {
            float new_x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float new_z = Camera.main.ScreenToWorldPoint(Input.mousePosition).z;

            Vector3 newPosition = new Vector3(new_x - offset_x, parent.position.y, new_z - offset_z);
            parent.position = newPosition;
        }
           //if twod is not selected then only move puck in 1 dimension
        else if(Input.GetMouseButton(0) && handleClicked == true && !twod.isOn)
        {
            float new_x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            Vector3 newPosition = new Vector3(new_x - offset_x, parent.position.y, parent.position.z);
            parent.position = newPosition;
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
