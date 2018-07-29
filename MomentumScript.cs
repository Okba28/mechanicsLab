using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Takes in settings from UI and the initial velocity set by vectors attached to each puck. Then adds the appropriate momentum 
// to each puck and traces the motion. Output properties momentum, velocity etc of each puck.

public class MomentumScript : MonoBehaviour {

    public InputField mass1;    //the masses of each puck as set by the user. Defaults are already set to 1 unit
    public InputField mass2;

    public Transform puck1;     //the puck objects themselves
    public Transform puck2;

    private Rigidbody rb_1;     //the rigidbodies attached to the pucks
    private Rigidbody rb_2;

    public Transform vectorHead1;
    public Transform vectorHead2;

    public Toggle twod;         //if 2d is selected then allow motion in 2d           

	// Use this for initialization
	void Start () {
        rb_1 = puck1.GetComponentInChildren<Rigidbody>();
        //rb_1 = puck1.GetComponent<Rigidbody>();
        rb_2 = puck2.GetComponentInChildren<Rigidbody>();

        //set the mass values to the default rigidbody start masses
        mass1.text = rb_1.mass.ToString();
        mass2.text = rb_2.mass.ToString();

        //add listeners for mass changes
        mass1.onEndEdit.AddListener(delegate { UpdateMass(); });
        mass2.onEndEdit.AddListener(delegate { UpdateMass(); });

        //load the trail particles
        
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}

    public void AddMomentum()
    {
        Vector3 vel = GetVelocity(puck1, vectorHead1);
        Vector3 vel2 = GetVelocity(puck2, vectorHead2);
        rb_1.AddForce(vel, ForceMode.VelocityChange);
        rb_2.AddForce(vel2, ForceMode.VelocityChange);
        RemoveVectors();            //remove the initialisation vectors and their labels

    }

    //All vectors and labels are given the tag "vectorArrow" allowing them to be easily removed from the scene when pucks
    // are launched
    private void RemoveVectors()
    {
        GameObject[] vectors = GameObject.FindGameObjectsWithTag("vectorArrow");
        foreach (GameObject vector in vectors)
        {
            Destroy(vector);
        }
    }

    //returns the velocity defined by the speed and direction of the vector associated with a puck
    private Vector3 GetVelocity(Transform puck, Transform vectorHead)
    {
        float vel_x = vectorHead.position.x - puck.position.x;
        float vel_z = vectorHead.position.z - puck.position.z;
        Vector3 vel;
        if (twod.isOn)
        {
            vel = new Vector3(vel_x, 0, vel_z);
        }
        else
        {
            vel = new Vector3(vel_x, 0, 0);
        }
        
        
        return vel;
    }

    //when the mass Input fields are edited then run this update function
    private void UpdateMass()
    {
        float mass1_value;
        float mass2_value;
        if(float.TryParse(mass1.text, out mass1_value))
        {
            rb_1.mass = mass1_value;
        }

        if (float.TryParse(mass2.text, out mass2_value))
        {
            rb_2.mass = mass2_value;
        }

    }
}
