using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//The Physics engine does not guarantee exact physics.

public class CalculateForcesScript : MonoBehaviour {

    public Transform mass;      //the sliding mass for accessing the friction coefficient and mass
    public Transform ramp;      // the ramp accessing the angle and the friction coefficient

    private float weight;       //The three forces that are calculated in the script
    private float reaction;
    private float friction;
	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        weight = CalculateWeight(mass.GetComponent<Rigidbody>().mass);
        reaction = CalculateReaction(weight);
        friction = CalculateFriction(reaction);
        

    }

    private float CalculateWeight(float mass)
    {
        return mass * Physics.gravity.magnitude;    //changing the strength of gravity will automatically change weight
    }

    private float CalculateReaction(float weight)
    {
        Quaternion boxRot = mass.rotation;
        float angle = Quaternion.Angle(boxRot, Quaternion.identity);    //the box rotation in radians - the box can only rotate around the x axis (fixed in others) so no other rotations possible
        float reaction = weight * Mathf.Cos(angle*Mathf.Deg2Rad);
        return reaction;
    }

    //Currently, only a single friction value is used, the static friction one.
    //IMPLEMENT a changing friction value later.
    private float CalculateFriction(float reaction)
    {
        //Instead of only getting the ramp friction, have adapted this in order to get the friction of whatever the mass
        // is colliding with.
        //float staticFriction = ramp.GetComponent<BoxCollider>().material.staticFriction;
        //float dynamicFriction = ramp.GetComponent<BoxCollider>().material.dynamicFriction;

        float staticFriction = mass.GetComponent<CollisionFrictionScript>().GetFrictionCoefficient();
        return staticFriction * reaction;
    }


    //Getters for the three force variables.
    public float GetWeight()
    {
        return weight;
    }

    public float GetReaction()
    {
        return reaction;
    }

    public float GetFriction()
    {
        return friction;
    }


}
