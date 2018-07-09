using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pulley class contains the properties of the pulley
public class Pulley : MonoBehaviour{

    float tensionForce;         //the tension in the rope around the pulley
    float supportForce;            //the single force on the support rail
    public bool fixedPulley;               //if the pulley is a fixed pulley then = true else it is a moveable pulley
                                            //if a pulley is fixed then it isn't attached to the mass, if it is moveable then it is attached
    

    private void Start()
    {
        tensionForce = 0;
        supportForce = 0;
    }

    //Getters and setters for the tension force, support force and whether it is a fixed pulley or not
    public void ChangeTensionForce(float force)
    {
        tensionForce = force;
    }

    public float GetTensionForce()
    {
        return tensionForce;
    }

    public void ChangeSupportForce(float force)
    {
        supportForce = force;
    }

    public float GetSupportForce()
    {
        return supportForce;
    }

    public void SetToFixedPulley()
    {
        fixedPulley = true;
    }

    public void SetToSupportPulley()
    {
        fixedPulley = false;
    }

    /// <summary>
    /// Moves the pulley along the given vector
    /// </summary>
    /// <param name="displacement"></param>
    public void MovePulley(Vector3 displacement)
    {
        Vector3 newPosition = transform.position + displacement;
        transform.position = newPosition;
    }

}
