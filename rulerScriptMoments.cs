using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//This script allows users to click two points and output the distance between those two points to a textfield in the 
// user interface.

//MOMENTS plane is y-z: just to be difficult! Next time remember to maintain a consistent plane in which all scenes work.
public class rulerScriptMoments : MonoBehaviour
{

    private int numPoints;
    private Vector3 startPoint;     //Vector3 associated with first mouse click/start point
    private Vector3 endPoint;

    public Material lineMaterial;       //line material to make the ruler measurement from
    private GameObject line;            //the line to draw and measure the length of.

    private bool lineDrawn;             //checking whether the line has been drawn or not

    public float offset_z;          //the offset for plotting the line after mouse clicks
    public float offset_y;

    public Text display;               //display the calculated distance

    public Transform crosshair_prefab;             //the animation to play on the points clicked for ruler
    private Transform crossStart;
    private Transform crossEnd;

    private float rayWidth = 0.05f;

    // Use this for initialization
    void Start()
    {
        numPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && numPoints == 0)  //assign the start point of the line
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint.x = 10f;      //ensure that line is above background and level in the z plane
            startPoint.z += offset_z;  //add offset and get middle of ray to correct point
            startPoint.y += offset_y;
            numPoints++;
            //Instantiate the crosshair prefab and set its position
            crossStart = GameObject.Instantiate(crosshair_prefab) as Transform;
            crossStart.position = startPoint;
            crossStart.Rotate(Vector3.forward, 90);      //ADDED TO MOMENTS SCRIPT IN ORDER TO SEE THE ANIMATION
        }
        else if (Input.GetMouseButtonDown(1) && numPoints == 1)    //if 1 click has already happened then assign the end point of the line
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint.x = 10f;
            endPoint.z += offset_z;
            endPoint.y += offset_y;
            numPoints++;
            //Instantiate the cross hair prefab for the end point and set its position
            crossEnd = GameObject.Instantiate(crosshair_prefab) as Transform;
            crossEnd.position = endPoint;
        }
        else if (Input.GetMouseButtonDown(1))    //if the user clicks again then get rid of the line
        {
            numPoints = 0;
            Destroy(line);
            lineDrawn = false;
            Destroy(crossStart.gameObject);
            Destroy(crossEnd.gameObject);
        }

        if (numPoints == 2 && lineDrawn == false)    //if button has been clicked twice then draw the line
        {
            DrawRay(startPoint, endPoint);
            lineDrawn = true;
        }
    }


    private void DrawRay(Vector3 startPosition, Vector3 endPosition)
    {
        line = new GameObject();
        line.AddComponent<LineRenderer>();
        LineRenderer finalRay = line.GetComponent<LineRenderer>();
        finalRay.positionCount = 2;
        finalRay.SetPosition(0, startPosition);
        finalRay.SetPosition(1, endPosition);
        finalRay.material = new Material(lineMaterial);
        //finalRay.startColor = Color.white;
        //finalRay.endColor = Color.white;
        finalRay.startWidth = rayWidth;
        finalRay.endWidth = rayWidth;

        //calculate the length of the ray and display on UI
        displayLength();
    }

    // calculates the length of the line vector and displays the magnitude on the UI
    private void displayLength()
    {
        float mag = Vector3.Magnitude(endPoint - startPoint);
        display.text = mag.ToString("F1");          //1 decimal place
    }
}
