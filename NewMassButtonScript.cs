using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//When the "New Mass" button is clicked in the UI, a mass box drops from the sky and lands in the space in front of the 
//plank. 
public class NewMassButtonScript : MonoBehaviour {

    public Canvas canvas;                   //The canvas to which the UI label should be set.
    public Transform plank;                  //The plank object to allow relative positioning of the new mass.
    public GameObject mass_prefab;          //The prefab mass which will be created.
    public GameObject massinput_prefab;     //The inputfield prefab

    // Use this for initialization
    void Start () {
		
	}

    
    public void CreateNewMass()
    {
        //Create the mass and position it above the field of view so that it falls into place
        GameObject newMass = Instantiate(mass_prefab) as GameObject;
        newMass.transform.position = new Vector3(plank.position.x + 2, plank.position.y + 10, plank.position.z - 2);

        //Instantiate the inputfield prefab and position it where the mass is.
        //It is not updated here.
        Vector3 input_position = newMass.transform.position;
        GameObject massInput = Instantiate(massinput_prefab) as GameObject;
        massInput.transform.SetParent(canvas.transform);
        input_position = new Vector3(input_position.x, input_position.y, input_position.z + 1);
        massInput.transform.position = Camera.main.WorldToScreenPoint(input_position);

        //Attach the input field to the new mass
        newMass.GetComponent<MomentsMassUIScript>().inputField = massInput.GetComponent<InputField>();

    }
}
