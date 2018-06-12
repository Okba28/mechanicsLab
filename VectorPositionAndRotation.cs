using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//This class is attached to each vector_prefab which is created on right clicking.
public class VectorPositionAndRotation : MonoBehaviour {

    public GameObject vector_sprite;            //the sprite that is used as the vector head
    public Material lineMaterial;               //The material that will be rendered as the vector
    //private Color vectorColour = Color.black;
    public float vectorWidth = 1f;
    public float vector_offset_x;
    public float vector_offset_z;

    private GameObject vector;      //the line that will be drawn as a vector
    Vector3 startPosition;          // the start and end positions of that vector
    Vector3 endPosition;

    public GameObject canvas;       //The UI canvas onto which textboxes are instantiated
    public Text lengthText_prefab;        //The prefab UI textboxes
    public Text angleText_prefab;
    private Text lengthText;     //the UI text boxes for displaying vector information
    private Text angleText;

    private Color vector_colour;        //the colour assigned to this vector, depending on the vector_count at the time of instantiation
    // each vector label needs to be offset a little in order not to overlap with others when drawn close to one another
    private Vector3 offset;

    //Initialise the possible vector colours
    private Color[] colours = new Color[] { Color.red, Color.black, Color.blue, Color.green, Color.cyan, Color.yellow, Color.magenta, Color.yellow, Color.grey };
    
    // Use this for initialization
    void Start () {
        float startPosition_x = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x + vector_offset_x;
        float startPosition_y = 1f;
        float startPosition_z = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).z + vector_offset_z;
        startPosition = new Vector3(startPosition_x, startPosition_y, startPosition_z);

        //get the canvas
        canvas = GameObject.FindGameObjectWithTag("canvas");

        //Instantiate the UI textboxes for displaying vector length and angle
        lengthText = Instantiate(lengthText_prefab) as Text;
        lengthText.transform.SetParent(canvas.transform);
        angleText = Instantiate(angleText_prefab) as Text;
        angleText.transform.SetParent(canvas.GetComponent<Canvas>().transform);

        int vector_count = Camera.main.GetComponent<ClickToAddVector>().GetVectorCount();
        vector_colour = colours[vector_count]; //colour assigned to vector
       

    }

    private void OnMouseDrag()
    {
        float endPosition_x = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).x + vector_offset_x;
        float endPosition_y = 1f;
        float endPosition_z = (Camera.main.ScreenToWorldPoint(Input.mousePosition)).z + vector_offset_z;
        endPosition = new Vector3(endPosition_x, endPosition_y, endPosition_z);
        Destroy(vector);
        DrawRay(startPosition, endPosition, vector_colour);    //the vector count is iterated everytime the right mouse is clicked globally
        vector_sprite.transform.position = endPosition;
        UpdateLengthAndAngle();
    }

    //method for drawing rays
    private void DrawRay(Vector3 startPosition, Vector3 endPosition, Color colour)
    {
        vector = new GameObject();
        vector.AddComponent<LineRenderer>();
        LineRenderer finalRay = vector.GetComponent<LineRenderer>();
        finalRay.positionCount = 2;
        finalRay.SetPosition(0, startPosition);
        finalRay.SetPosition(1, endPosition);
        //finalRay.material = new Material(Shader.Find("Particles/Additive (Soft)"));
        finalRay.material = new Material(lineMaterial);
        //finalRay.startColor = colour;
        finalRay.material.color = colour;
        //finalRay.endColor = colour;
        finalRay.startWidth = vectorWidth;
        finalRay.endWidth = vectorWidth;

        //Destroy(line, 0.02f);
    }

    //This function will calculate the magnitude of the drawn vector and its angle TO THE HORIZONTAL 
    // and output these to the UI text boxes which should stay with the vector head
    private void UpdateLengthAndAngle()
    {
        Vector3 drawnVector = (endPosition - startPosition);
        //get the length of the drawn vector
        float length = drawnVector.magnitude;
        
        //get its angle to the horizontal
        float angle = Vector3.Angle(drawnVector, Vector3.right);
        

        //Add calculated values to the UI
        //Positions textboxes
        
        lengthText.text = "Length = " + length.ToString("F1");
        lengthText.color = vector_colour;
        lengthText.transform.position = Camera.main.WorldToScreenPoint(startPosition + (endPosition - startPosition) / 2 + new Vector3(2,0,0));
        angleText.text = "Angle = " + angle.ToString("F1");
        angleText.color = vector_colour;
        angleText.transform.position = Camera.main.WorldToScreenPoint(startPosition + (endPosition - startPosition) / 2 + new Vector3(2,0,-1));
    }
}
