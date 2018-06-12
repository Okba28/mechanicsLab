using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilesSceneMaster : MonoBehaviour {

    CreatePlinthAndTarget runplinth;

    public int hit_count;       //number of targets hit
    public int fire_count;      //number of shots fired

    

	// Use this for initialization
	void Start () {
        hit_count = 0;
        fire_count = 0;
        runplinth = transform.GetComponent<CreatePlinthAndTarget>();
	}
	
	public void AddHit()
    {
        hit_count++;
    }

    public void AddShot()
    {
        fire_count++;
    }

    public int GetHitCount()
    {
        return hit_count;
    }

    public void CreatNewPlinthAndTarget()
    {
        runplinth.enabled = false;
        runplinth.enabled = true;
    }
}
