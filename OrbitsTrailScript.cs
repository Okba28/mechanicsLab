using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Attached to the cannon ball projectile - produces a trail to show the projectile path.
public class OrbitsTrailScript : MonoBehaviour
{

    private GameObject trailParticle;       //the particle that makes up the projectile path      
    public float deltaTime = 0.1f;         //The time difference between trail particles
    private float nextTime;             //the next time that a trail particle should be formed

    public float particle_size = 0.05f;
    public Color particle_colour;
    public float death_time;

    // a list of all the trail particle gameobjects for easy destruction
   // public List<GameObject> particles = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        trailParticle = Resources.Load("TrailParticle") as GameObject;
        nextTime = Time.time + deltaTime;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > nextTime)
        {
            GameObject particle = Instantiate(trailParticle) as GameObject;     //create the trail particle
            particle.transform.position = transform.position;                   //position it where the cannon ball is
            particle.transform.localScale = new Vector3(particle_size, particle_size, particle_size);
            particle.GetComponent<Renderer>().material.SetColor("_Color", particle_colour);
            //rend.material.shader = Shader.Find("_Color");
            //particles.Add(particle);                                            //add it to the list of particles
            nextTime = Time.time + deltaTime;                                   //update the next time a trail particle is formed
            Destroy(particle, death_time);
        }
    }

    //loops through all the trail particles created and destroys them
    //public void DeleteTrailParticles()
    //{
    //    for (int i = 0; i < particles.Count; i++)
    //    {
    //        Destroy(particles[i].gameObject);
    //    }
    //}
}
