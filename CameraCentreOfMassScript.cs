using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script moves the camera to remain above the centre of mass of a two particle system.
public class CameraCentreOfMassScript : MonoBehaviour {

    public Camera CoMCam;           //the camera that will remain above the centre of mass
    public Transform planet;
    public Transform star;

    private Rigidbody rb_star;
    private Rigidbody rb_planet;

	// Use this for initialization
	void Start () {
        rb_star = star.GetComponent<Rigidbody>();
        rb_planet = planet.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        float star_mass = rb_star.mass;
        float planet_mass = rb_planet.mass;
        Vector3 com = (planet_mass * planet.position + star_mass*star.position)/(star_mass+planet_mass);      //CoM in 3D
        CoMCam.transform.position = new Vector3(com.x, CoMCam.transform.position.y, com.z);
	}
}
