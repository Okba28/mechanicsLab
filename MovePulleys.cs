using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//If the handle object is clicked and dragged then move the mass and pulleys the appropriate amount
public class MovePulleys : MonoBehaviour {

    public Transform mass;
    public List<GameObject> moveable_pulleys;

    private Vector3 newPosition;
    private float mass_offset = -5f;

	// Use this for initialization
	void Start () {
       
	}
	

    private void OnMouseDrag()
    {
        //get the mechanical advantage of the pulley system
        float mechanical_advantage = PulleyController.GetMechanicalAdvantage();
        
      

        //Add all moveablepulleys to list
        GameObject[] pulleys = GameObject.FindGameObjectsWithTag("Pulley");
        foreach (GameObject pulley in pulleys)
        {
            if (!pulley.GetComponent<Pulley>().fixedPulley)
            {
                moveable_pulleys.Add(pulley);
            }
        }
        //if mouse moves up then shift handle up and pulleys down
        if(Input.GetAxis("Mouse Y") > 0)
        {
            newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);
            foreach(GameObject pulley in moveable_pulleys)
            {
                Vector3 new_pulley_pos = new Vector3(pulley.transform.position.x, -newPosition.y/mechanical_advantage, pulley.transform.position.z);
                pulley.transform.position = new_pulley_pos;
            }
        }
        //shift handle down and pulleys up
        else if(Input.GetAxis("Mouse Y") < 0)
        {
            newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);
            foreach (GameObject pulley in moveable_pulleys)
            {
                Vector3 new_pulley_pos = new Vector3(pulley.transform.position.x, -newPosition.y/mechanical_advantage, pulley.transform.position.z);
                pulley.transform.position = new_pulley_pos;
            }
        }
        //returns +1 if no pulleys in the pulley list
        float mass_x_pos = PulleyController.AverageMoveablePulleyPositionX();
        //move the mass to the correct position
        if (mass_x_pos <= 0)
        {
            mass.position = new Vector3(mass_x_pos, -newPosition.y / mechanical_advantage + mass_offset, mass.position.z);
        }

       

    }
}
