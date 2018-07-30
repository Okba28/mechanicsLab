using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Attached to the cannon ball projectile - produces a trail to show the projectile path.
public class MomentumTrailScript : MonoBehaviour
{

    private GameObject trailParticle;       //the particle that makes up the projectile path      
    public float deltaTime = 0.1f;         //The time difference between trail particles
    private float nextTime;             //the next time that a trail particle should be formed
    private float stopTime;             //stop making the trails after a sufficiently long time
    public float lifetime = 60;         //lifetime for making trail set to 60 as default

    public float particle_size = 0.05f;
    public Color particle_colour;
    //public float death_time;      //momentum trail lasts until reset.

    private GameObject previous_trail_particle;
    private Rigidbody rb;           //the rigidbody attached to this transform
    private Vector3 velocity;
    // a list of all the trail particle gameobjects for easy destruction
    // public List<GameObject> particles = new List<GameObject>();

    public Button launchButton;         //trail particles should only be created after launch button is clicked


    // Use this for initialization
    void Start()
    {
        trailParticle = Resources.Load("TrailParticle") as GameObject;
        nextTime = Time.time + deltaTime;
        rb = transform.GetComponent<Rigidbody>();

        launchButton.onClick.AddListener(delegate { StartTimer(); });
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get the velocity of the puck
        velocity = rb.velocity;
        if (Time.time >= nextTime && !launchButton.IsActive() && Time.time < stopTime && velocity.magnitude > 0)
        {
            GameObject particle = Instantiate(trailParticle) as GameObject;     //create the trail particle
            
            //particle.transform.position = transform.position;                   //position it where the cannon ball is
            particle.transform.position = previous_trail_particle.transform.position + velocity * deltaTime;
            particle.transform.localScale = new Vector3(particle_size, particle_size, particle_size);
            particle.GetComponent<Renderer>().material.SetColor("_Color", particle_colour);
            //rend.material.shader = Shader.Find("_Color");
            //particles.Add(particle);                                            //add it to the list of particles
            nextTime = Time.time + deltaTime;                                   //update the next time a trail particle is formed
            //Destroy(particle);
            //make this the previous particle for the next loop
            previous_trail_particle = particle;
        }
    }

    private void StartTimer()
    {
        stopTime = Time.time + lifetime;
        previous_trail_particle = transform.gameObject;
        
    }
    
}
