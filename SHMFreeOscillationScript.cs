using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script for generating simple harmonic motion of a free oscillator
//With simply a spring joint and no damping (either drag on rigidbody or spring damping) the mass is still damped.
//Therefore a periodic impulse is applied to the spring in order to maintain its amplitude: this is still not perfect.
//This script needs to be added to the DrivenOscillationsScript as well in order to allow for mass and spring constant changes.

public class SHMFreeOscillationScript : MonoBehaviour {

    public Transform oscillator;    //the mass that is oscillating
    private float oscillator_mass;  //the mass of the oscillator
    private float k;                // the spring constant
    private float damping_constant;

    private Rigidbody rb;           // the rigidbody component of the oscillator
    private SpringJoint spring;     //the spring joint component
    private float period;           //the period of the simple harmonic motion

    private float nextTime;         // the time in seconds when the next force should be applied   

    public float forceMag;          // the magnitude of the force to add to the spring for driven oscillations

    //UI text boxes
    public InputField massText;
    public InputField springText;

	// Use this for initialization
	void Start () {
        rb = oscillator.GetComponent<Rigidbody>();      //get the rigidbody component
        spring = oscillator.GetComponent<SpringJoint>();    //and the spring joint

        oscillator_mass = rb.mass;
        k = spring.spring;

        //the period of the motion is 2*pi*(m/k)^0.5
        period = 2 * Mathf.PI * Mathf.Sqrt(oscillator_mass / k); 

        //start the clock
        nextTime = Time.time + period;      //this is the next time that the force should be applied

        OutputToUI(oscillator_mass, k);

        //HAVE LISTENERS IN CASE THE MASS OR SPRING CONSTANT ARE CHANGED
        massText.onEndEdit.AddListener(delegate { UpdateMassSpringPeriod(); });
        springText.onEndEdit.AddListener(delegate { UpdateMassSpringPeriod(); });
    }
	
	// Update is called once per frame
	void Update () {
        if(Time.time >= nextTime)
        {
            rb.AddForce(new Vector3(0, forceMag, 0), ForceMode.Impulse);
            nextTime += period;
        }
        

        
	}

    //A listener should call this if the mass or spring constant are changed
    private void UpdateMassSpringPeriod()
    {
        //Update mass: input field is new mass, so first assign this to the oscillator mass and then to the rigidbody
        oscillator_mass = float.Parse(massText.text);
        rb.mass = oscillator_mass;

        //Update the spring constant
        k = float.Parse(springText.text);
        spring.spring = k;

        //Update the period
        period = 2 * Mathf.PI * Mathf.Sqrt(oscillator_mass / k);

        
    }

    //outputs the mass and spring constant to the UI in order for calculations to be done on period etc.
    private void OutputToUI(float mass, float k)
    {
        massText.text = mass.ToString("F1");
        springText.text = k.ToString("F1");
    }
}
