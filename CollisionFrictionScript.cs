using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Attached to the mass, whenever it has a collision it detects the collider and finds the coefficient of friction for it
// A public getter then allows other scripts to access it.

public class CollisionFrictionScript : MonoBehaviour {

    private float frictionCoefficient;
    public Text coefficientText;
    public Text accelerationText;
    private float mass;
    private Rigidbody rb;       //rigidbody of the mass
    
    private Collider col;

    private CalculateForcesScript forcesScript;
	// Use this for initialization
	void Start () {
        rb = transform.GetComponent<Rigidbody>();
        forcesScript = transform.GetComponentInParent<CalculateForcesScript>(); //allows access to the forces and acceleration
        mass = rb.mass;
    }

    //private void OnCollisionEnter(Collision collision)
    void Update()
    {
        //col = collision.collider;
        //if the mass is stationary then static friction coefficient used, else dynamic.
        if (col != null)
        {
            if (rb.velocity.z == 0)
            {
                frictionCoefficient = col.material.staticFriction;
            }
            else
            {
                frictionCoefficient = col.material.dynamicFriction;
            }
            OutputToTextBox(frictionCoefficient);   //output to UI
            UpdateAcceleration();
        }
    }

    //called from calculate forces script in order to get the friction force
    public float GetFrictionCoefficient()
    {
        return frictionCoefficient;
    }

    //acceleration of the rigidbody not calculated in Unity - calculate using components of forces along slope
    private void UpdateAcceleration()
    {
        float resultant = forcesScript.GetParallelForce() - forcesScript.GetFriction();     //resultant along slope
        if(resultant < 0 || rb.velocity.z == 0)     
        {
            accelerationText.text = "0";
        }
        else
        {
            float acceleration = resultant / mass;
            accelerationText.text = acceleration.ToString("F1");

        }
    }

    private void OutputToTextBox(float frictionCoefficient)
    {
        coefficientText.text = frictionCoefficient.ToString();
    }

    //when mass collides with a new object it resets the collider
    private void OnCollisionEnter(Collision collision)
    {
        col = collision.collider;
    }
}
