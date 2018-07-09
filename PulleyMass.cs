using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//There will only be 1 mass in the scene
public  class PulleyMass : MonoBehaviour{

    int numPulleysAttached;         //each pulley attached corresponds to 2 support ropes
    bool supportRopeAttached = false;       //whether a support rope is attached directly to the mass 

    float weight = 10;                //default it to 10N;                   //the weight of the attached mass

   
    public int GetNumPulleysAttached()
    {
        return numPulleysAttached;
    }

    public bool GetSupportRopeAttached()
    {
        return supportRopeAttached;
    }

    /// <summary>
    /// Moves the mass along the given vector
    /// </summary>
    /// <param name="displacement"></param>
    public void MoveMass(Vector3 displacement)
    {
        Vector3 newPosition = transform.position + displacement;
        transform.position = newPosition;
    }

    public float GetWeight()
    {
        return weight;
    }

    public void SetWeight(float w)
    {
        weight = w;
    }

    public void AddSupportRope()
    {
        supportRopeAttached = true;
    }

    public void RemoveSupportRope()
    {
        supportRopeAttached = false;
    }

    public void AddPulleyToMass()
    {
        numPulleysAttached++;
    }

    public void RemovePulleyFromMass()
    {
        numPulleysAttached--;
    }
}
