using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Attached to the mass, whenever it has a collision it detects the collider and finds the coefficient of friction for it
// A public getter then allows other scripts to access it.

public class CollisionFrictionScript : MonoBehaviour {

    private float frictionCoefficient;
    public Text coefficientText;
    
    
    private Collider col;
	// Use this for initialization
	void Start () {
        
	}

    private void OnCollisionEnter(Collision collision)
    {
        col = collision.collider;
        //Only static friction used for now, will implement a change in friction later
        frictionCoefficient = col.material.staticFriction;
        Debug.Log("Coefficient = " + col.material.staticFriction);
        OutputToTextBox(frictionCoefficient);
    }

    public float GetFrictionCoefficient()
    {
        return frictionCoefficient;
    }

    private void OutputToTextBox(float frictionCoefficient)
    {
        coefficientText.text = frictionCoefficient.ToString();
    }

    
}
