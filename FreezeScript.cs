using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Freezes the position of the objects in the scene

public class FreezeScript : MonoBehaviour {

    //Add the objects that need to be frozen
    public Rigidbody mass;
    public Rigidbody ramp;

	// Use this for initialization
	void Start () {
		
	}
	
	public void Pause()
    {
        mass.constraints = RigidbodyConstraints.FreezeAll;
        ramp.constraints = RigidbodyConstraints.FreezeAll;
    }

    public void Unpause()
    {
        mass.constraints = RigidbodyConstraints.None;
        ramp.constraints = RigidbodyConstraints.None;
    }
}
