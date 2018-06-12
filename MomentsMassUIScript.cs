using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   

//A script for attaching the mass label to the masses and applying the input mass to the object.
// This script is attached to the mass.
public class MomentsMassUIScript : MonoBehaviour {

    public InputField inputField;
    public float offset_x;
    public float offset_y;
    public float offset_z;

    // Use this for initialization
    void Start () {

        //Add a listener for changes in the mass value
        inputField.onEndEdit.AddListener(delegate { UpdateMass(); });
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 newPosition = transform.position;
        newPosition.z += offset_z;
        newPosition.y += offset_y;
        newPosition.x += offset_x;
        inputField.transform.position = Camera.main.WorldToScreenPoint(newPosition);
	}

    //When mass is updated the mass needs to be moved up to stop the collision in order to recalculate the weight
    private void UpdateMass()
    {
        float newValue;
        if (float.TryParse(inputField.text, out newValue))
        {
            transform.GetComponent<Rigidbody>().mass = newValue;
            //increase the y position fractionally in order to stop the collision and then it will recalculate
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.01f, transform.position.z);
        }
    }
}
