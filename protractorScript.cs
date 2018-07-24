using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Similar to the rulerScript but for angle measurements
// 3 positions (ABC) are clicked and the angle ABC is calculated and output to a textfield on the UI.
// A UI button interchanges between ruler and protractor measurements.

public class protractorScript : MonoBehaviour {

    int numPoints;
    Vector3 startPoint;
    Vector3 midPoint;
    Vector3 endPoint;

    private GameObject line1;   //lines to draw that mark angle
    private GameObject line2;

    public Material lineMaterial;

    public Text display;

    public float offset_x;          //the offset for plotting the line after mouse clicks
    public float offset_z;

    private bool lineDrawn;

    private GameObject[] lines;         //list of the lines drawn for destroying them later

    public Transform crosshair_prefab;             //the animation to play on the points clicked for ruler
    private Transform crossStart;
    private Transform crossMid;
    private Transform crossEnd;

    private float height = 10f;

    // Use this for initialization
    void Start () {
        numPoints = 0;
        lineDrawn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(1) && numPoints == 0)
        {
            startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            startPoint.y = height;
            startPoint.x += offset_x;
            startPoint.z += offset_z;
            numPoints++;
            //Instantiate the crosshair prefab and set its position
            crossStart = GameObject.Instantiate(crosshair_prefab) as Transform;
            crossStart.position = startPoint;
        } else if(Input.GetMouseButtonDown(1) && numPoints == 1)
        {
            midPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            midPoint.y = height;
            midPoint.x += offset_x;
            midPoint.z += offset_z;
            numPoints++;
            //Instantiate the crosshair prefab and set its position
            crossMid = GameObject.Instantiate(crosshair_prefab) as Transform;
            crossMid.position = midPoint;
        } else if(Input.GetMouseButtonDown(1) && numPoints == 2)
        {
            endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            endPoint.y = height;
            endPoint.x += offset_x;
            endPoint.z += offset_z;
            numPoints++;
            //Instantiate the crosshair prefab and set its position
            crossEnd = GameObject.Instantiate(crosshair_prefab) as Transform;
            crossEnd.position = endPoint;
        } else if (Input.GetMouseButtonDown(1)) //click again to destroy the objects
        {
            numPoints = 0;
            //Destroy the drawn lines
            Destroy(lines[0]);
            Destroy(lines[1]);
            lineDrawn = false;
            //Destroy the crosshairs
            Destroy(crossStart.gameObject);
            Destroy(crossMid.gameObject);
            Destroy(crossEnd.gameObject);
        }

        if(numPoints == 3 && lineDrawn == false)
        {
            lines = new GameObject[2];
            DrawRay(startPoint, midPoint, line1, 0);
            DrawRay(midPoint, endPoint, line2, 1);
            lineDrawn = true;
        }
	}

    private void DrawRay(Vector3 startPosition, Vector3 endPosition, GameObject line, int position)
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
        finalRay.startWidth = 0.1f;
        finalRay.endWidth = 0.1f;
        // add line to list of lines for destruction later: first ray goes at index 0 second at 1 - there are only 2 lines at a time
        lines[position] = line;
        //calculate the length of the ray and display on UI
        displayAngle();
    }

    // calculates the length of the line vector and displays the magnitude on the UI
    private void displayAngle()
    {
        //calculate angle here
        Vector3 from = startPoint - midPoint;   //ensure vectors are acting from a common point in order to calculate angle between
        Vector3 to = endPoint - midPoint;

        float angle = Vector3.Angle(to, from);
        display.text = angle.ToString("F1");          //1 decimal place
    }
}
