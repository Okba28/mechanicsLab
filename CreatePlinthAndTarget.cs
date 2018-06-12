using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//For the projectile scene
//Instantiate a plinth, reposition and rescale it randomly
// Then instantiate a target on top
public class CreatePlinthAndTarget : MonoBehaviour {

    private GameObject plinth_prefab;
    private GameObject target_prefab;
    private GameObject target;
    public GameObject plinth;
    public Transform cannon;        //the cannon object in order to position the plinths relative to it.

    public bool target_created;
    

    private float position_x;       //the x range from the cannon of the plinth
    private float height;           // the scale size of the plinth

    public float growth_rate = 0.2f;
    // Use this for initialization
    

    private void OnEnable()
    {
        plinth_prefab = Resources.Load("Plinth") as GameObject;
        target_prefab = Resources.Load("Target") as GameObject;

        //target_prefab = GameObject.FindGameObjectWithTag("target");
        height = Random.Range(2f, 30f);     //the randomly assigned height and position of the plinth
        position_x = Random.Range(4f, 20f);

        //instantiate the objects
        //target = Instantiate(target_prefab);

        plinth = Instantiate(plinth_prefab);
        plinth.transform.position = new Vector3(cannon.position.x + position_x, plinth.transform.position.y, cannon.position.z);
        target_created = false;         //start false so that everything runs
    }

    // Update is called once per frame
    //Update should have the plinth grown into position and then form a target on the top
    void Update () {
        if (!target_created)
        {
            //grow the plinth until it reaches the randomly assigned height
            float current_height = plinth.transform.localScale.y;
            if (current_height < height)
            {
                plinth.transform.localScale = new Vector3(1, current_height + growth_rate, 1);
            }
            else
              //once the height is reached, create the target
            {
                float target_x = plinth.transform.position.x;
                float target_y = plinth.transform.position.y + plinth.transform.localScale.y / 2 + 2;
                float target_z = plinth.transform.position.z;
                target = Instantiate(target_prefab);
                target.transform.position = new Vector3(target_x, target_y, target_z);

                target_created = true;      //set target created to true so that update stops running
            }
        }

	}
}
