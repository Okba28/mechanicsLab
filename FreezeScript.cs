using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Freezes the position of the objects in the scene

public class FreezeScript : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Unpause()
    {
        Time.timeScale = 1;
    }
}
