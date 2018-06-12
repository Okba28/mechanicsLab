using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attached to the target object in the Projectiles scene
public class TargetHitScript : MonoBehaviour {

    private GameObject sceneMaster;
    private ProjectilesSceneMaster projectilemaster;
	// Use this for initialization
	void Start () {
        sceneMaster = GameObject.FindGameObjectWithTag("sceneMaster");      //attach the scene master
        projectilemaster = sceneMaster.GetComponent<ProjectilesSceneMaster>();
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "cannonball")
        {
            Destroy(transform.gameObject, 1f);     //destroy the target object after a delay
            Destroy(sceneMaster.GetComponent<CreatePlinthAndTarget>().plinth);  //destroy plinth instantly
            projectilemaster.AddHit();
            projectilemaster.CreatNewPlinthAndTarget();
        }
        //if a target falls off a plinth without being hit then it should be destroyed and a new plinth formed
        else if (collision.collider.tag == "destroyRegion")
        {
            //destroy target transform, whether a cannon ball or target
            Destroy(transform.gameObject);     //destroy the transform object instantly
            //if a target object falls off the plinth without being hit then create a new plinth and target
            if (transform.tag == "target")
            {
                Destroy(sceneMaster.GetComponent<CreatePlinthAndTarget>().plinth);  //destroy plinth instantly
                projectilemaster.CreatNewPlinthAndTarget();
            }
            
        }
    }
}
