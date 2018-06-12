using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The weight vector should not rotate with the mass, but remaining aiming at the ground.
//Its position should be changed though in order for it to move with the mass

public class WeightVectorScript : MonoBehaviour {

    public Transform mass;      // the mass object that the weight vector is attached to

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, mass.position.y - 1, mass.position.z);
       
	}
}
