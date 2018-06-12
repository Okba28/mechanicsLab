using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script attached to the horizontal and vertical axes in the SHM scenes.
public class PositionScriptAxes : MonoBehaviour
{

    public Transform parent;
    bool handleClicked;
    public float offset_x;
    public float offset_y;
    // Use this for initialization
    void Start()
    {
        handleClicked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && handleClicked == true)
        {
            float new_x = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
            float new_y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

            Vector3 newPosition = new Vector3(new_x - offset_x, new_y - offset_y, parent.position.z);
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
