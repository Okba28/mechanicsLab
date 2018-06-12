using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CannonBallFireScript : MonoBehaviour {

    public Transform exitPoint;
    public GameObject cannonball_prefab;
    //the UI text fields from which the velocity and drag are taken
    public Text vel_text;       
    public Text drag_text;

    private GameObject cannon_ball;
    private GameObject scenemaster;
    private ProjectilesSceneMaster psm;
    private ProjectileTrailScript pts;
    
	// Use this for initialization
	void Start () {
        scenemaster = GameObject.FindGameObjectWithTag("sceneMaster");
        psm = scenemaster.GetComponent<ProjectilesSceneMaster>();       //get the scenemaster for keeping track of the number of shots
        
        
     
    }
	
	public void FireCannonBall()
    {
        //no trail to begin with so check that there is a trail script before attempting to delete the trail
        if(pts != null)
        {
            pts.DeleteTrailParticles();
        }
        Destroy(cannon_ball);       //remove the previous cannonball before firing the next
        cannon_ball = Instantiate(cannonball_prefab) as GameObject;     //create the new cannon ball
        pts = cannon_ball.GetComponent<ProjectileTrailScript>();        //get the associated trail script for that cannon ball
        cannon_ball.transform.position = exitPoint.position;            //position the ball correctly
        Rigidbody rb = cannon_ball.GetComponent<Rigidbody>();           // get the cannon ball rigidbody for adding the velocity
        rb.drag = float.Parse(drag_text.text);                          //the drag to apply to the ball
        rb.velocity = exitPoint.right * float.Parse(vel_text.text);     // give the ball the correct velocity
        psm.AddShot();                                                  // add a shot to the scene master
    }

    
}
