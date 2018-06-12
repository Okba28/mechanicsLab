using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Should be attached to a vector sprite.
//Will update the position of the text and its value.
public class VectorTextBoxScript : MonoBehaviour {

    public Transform vector;        //the vector to which this textbox is attached
    public Text text;              //the textbox in which the vector value will be displayed.
    public GameObject massContainer;    //contains the script which calculates forces
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 newPosition = UpdateVectorValuePos(vector.tag);
        text.transform.position = Camera.main.WorldToScreenPoint(newPosition);  //places the text at the vector position.
        
	}

    // updates the text value and sets the new position
    private Vector3 UpdateVectorValuePos(string vectorTag)
    {
        if (vectorTag == "VectorReaction")
        {
            text.text = massContainer.GetComponent<CalculateForcesScript>().GetReaction().ToString("F1");
            return new Vector3(vector.position.x - 2.5f, vector.position.y, vector.position.z);
        }
        else if (vectorTag == "VectorWeight")
        {
            text.text = massContainer.GetComponent<CalculateForcesScript>().GetWeight().ToString("F1");
            return new Vector3(vector.position.x, vector.position.y, vector.position.z);
        }
        else if (vectorTag == "VectorFriction")
        {
            text.text = massContainer.GetComponent<CalculateForcesScript>().GetFriction().ToString("F1");
            return new Vector3(vector.position.x + 0.5f, vector.position.y + 0.5f, vector.position.z);
        }
        return new Vector3(0, 0, 0);
    }
}
