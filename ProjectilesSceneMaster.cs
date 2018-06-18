using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProjectilesSceneMaster : MonoBehaviour {

    CreatePlinthAndTarget runplinth;

    public int hit_count;       //number of targets hit
    public int fire_count;      //number of shots fired

    public Text score;

	// Use this for initialization
	void Start () {
        hit_count = 0;
        fire_count = 0;
        runplinth = transform.GetComponent<CreatePlinthAndTarget>();
	}
	
	public void AddHit()
    {
        hit_count++;
        UpdateScore();
    }

    public void AddShot()
    {
        fire_count++;
        UpdateScore();
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

    private void UpdateScore()
    {
        score.text = "Score: " + hit_count.ToString() + "/" + fire_count.ToString(); 
    }
}
