using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomScriptMomentum : MonoBehaviour
{

    public Camera cam;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (cam.orthographic == true)       //if cam is orthographic
            {
                float currentSize = cam.orthographicSize;
                float newSize = currentSize + 0.5f;
                if (newSize < 50)   //maximum effective field of view
                {
                    cam.orthographicSize = newSize;
                }
            }
            //else it is perspective
            else
            {
                float current_fov = cam.fieldOfView;
                float new_fov = current_fov += 1f;
                if (new_fov < 70)
                {
                    cam.fieldOfView = new_fov;
                }
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (cam.orthographic == true)
            {
                float currentSize = cam.orthographicSize;
                float newSize = currentSize - 0.5f;
                if (newSize > 5)        //minimum effective field of view
                {
                    cam.orthographicSize = newSize;
                }
            }
            else
            {
                float current_fov = cam.fieldOfView;
                float new_fov = current_fov -= 1f;
                if (new_fov > 20)
                {
                    cam.fieldOfView = new_fov;
                }
            }
        }
    }
}
