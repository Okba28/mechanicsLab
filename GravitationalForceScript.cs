using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Calculate the force between two bodies - called star and planet to differentiate them
// Use the rigidbody addforce after calculating the correct force between the two objects.
// Remove gravity from the simulation as the force of gravity is being calculated and shouldn't be acting towards -y.

    //The distance scale has been chosen so that 1 unit in Unity space is 1AU
    //Masses are in solar masses


public class GravitationalForceScript : MonoBehaviour {

    //The scenemaster gameobject
    public GameObject scenemaster;

    public Transform star;
    public Transform planet;
    private Rigidbody rb_star;
    private Rigidbody rb_planet;

    //private float G = 6.67408E-11f;     //m^3kg^-1s^-2
    //private float G = 4 * Mathf.PI * Mathf.PI;  //G in units of AU^3yr^-2M(sun)^-1 solar masses, AU and time period of yrs.
    private float G_real = 3.964E-14f;                   //G in units AU^3 M(sun)^-1 s^-2: since the time for the fixed update is in seconds, the velocity needs to be in seconds.
    private float G = 10f;      //increase G to speed up simulations
    private float vel_ratio;

    //properties of the two bodies
    private float star_mass;        //mass is in solar masses
    private float planet_mass;
    private float planet_velocity_start;        //the initial velocities of the bodies in the z direction
    private float star_velocity_start;

    //UI objects
    public InputField planet_mass_UI;
    public InputField star_mass_UI;
    public InputField planet_vel_UI;
    public InputField star_vel_UI;
    public InputField start_distance;
    public Button resetButton;
    public Text total_energy;
    public Text current_distance;
    public Text planet_current_vel;
    public Text star_current_vel;

    //For calculating the speed we need to store the previous position of the star and planet
    Vector3 lastPos_planet;
    Vector3 lastPos_star;

    // Use this for initialization
    //The initial system is the earth orbiting the sun
    void Start () {
        rb_star = star.GetComponent<Rigidbody>();
        rb_planet = planet.GetComponent<Rigidbody>();
        //for a circular orbit
        float distance = CalculateDistance(star,planet).magnitude;
        float[] velocities = CalculateCircularVelocity(rb_planet.mass, rb_star.mass, distance);    
        rb_planet.velocity = new Vector3(0, 0, velocities[0]);  //bodies start along the x axis, so require velocity tangentially along the z axis
        rb_star.velocity = new Vector3(0, 0, -velocities[1]);

        //Add listeners for when the reset button is clicked
        resetButton.onClick.AddListener(delegate { ResetSimulation(); });

        lastPos_planet = planet.position;       //set last positions to the initial positions
        lastPos_star = star.position;

        Time.fixedDeltaTime = 0.001f;       //in order for orbits to be calculated relatively accurately, need more frequent physics calculations

        vel_ratio = Mathf.Sqrt(G_real / G);               //get the real velocity of the object rather than the increased simulation rate

    }
    

    //Fixed update should be used with rigidbody physics.
    void FixedUpdate () {

        star_mass = rb_star.mass;           //set masses here incase the mass is changed during the sim
        planet_mass = rb_planet.mass;
        Vector3 r = CalculateDistance(star, planet);
        Vector3 force = CalculateForce(star_mass, planet_mass, r);
        rb_planet.AddForce(force, ForceMode.Force);      //apply this force to the planet
        rb_star.AddForce(-force, ForceMode.Force);       //apply the force in the opposite direction to the star


        //calculate the speeds of the planet and star since rigidbody velocity (even when dividing by time.fixeddelta time gives incorrect speed)
        float v_planet = CalculateSpeed(planet.position,lastPos_planet);
        float v_star = CalculateSpeed(star.position, lastPos_star);
        //set the last position so that the next velocity can be calculated when fixed update runs again
        lastPos_planet = planet.position;
        lastPos_star = star.position;


        //output energy of planet
        float KE_planet = CalculateKE(planet_mass, v_planet);   //get velocity from distance/frame to distance/sec
        float GPE = CalculateGPE(star_mass, planet_mass, r.magnitude);
        
        //output energy values for star
        float KE_star = CalculateKE(star_mass, v_star);

        //total energy
        float E = KE_planet + KE_star + GPE;
        //output energy in units M(sun)(AU^2)(s^-2) to UI
        total_energy.text = E.ToString("E1");
        //output distance and velocities
        current_distance.text = (r.magnitude).ToString("F2");
        planet_current_vel.text = (v_planet*vel_ratio).ToString("E2");
        star_current_vel.text = (v_star*vel_ratio).ToString("E2");
    }

    //Calculate the gravitational force between the two masses
    private Vector3 CalculateForce(float star_mass, float planet_mass, Vector3 r)
    {
        float r_mag = r.magnitude;      //get the distance between the two bodies
        Vector3 direction = r.normalized;       //get the direction of the force
        float F = G * star_mass * planet_mass / (r_mag*r_mag);
        return F*direction;       //resultant force vector in direction of r: towards the star
    }
    // the distance between the bodies as a vector in order to give the force a direction.
    //the direction is from the planet to the star
    //remember that the force direction for the star to planet will be the reverse.
    private Vector3 CalculateDistance(Transform star, Transform planet)
    {
        Vector3 distance = star.position - planet.position;
        return distance;

    }

    //Calculates the velocity required for the planet to be in a circular orbit around the star.
    private float[] CalculateCircularVelocity(float planet_mass, float star_mass, float distance)
    {
        float[] vel_circ = new float[2];
        //The planet orbits with radius from the centre of mass dependent on the ratio of the star_mass to total mass
        // m1r1 = m2r2
        float mass_ratio = star_mass / (star_mass + planet_mass);
        float planet_vel_circ = Mathf.Sqrt(mass_ratio * (G * star_mass / distance));
        float star_vel_circ = (planet_mass / star_mass) * planet_vel_circ;
        vel_circ[0] = planet_vel_circ;
        vel_circ[1] = star_vel_circ;
        return vel_circ;
    }

    //Read in the velocity values provided by the user in the UI
    //If no values are set in UI then apply the velocity that gives a circular orbit around the centre of mass
    private void SetVelocities()
    {
        //if a velocity isn't set then use a circular velocity
        float distance = CalculateDistance(star, planet).magnitude;
        float[] circ_velocities = CalculateCircularVelocity(rb_planet.mass, rb_star.mass, distance);
        
       
        if (float.TryParse(planet_vel_UI.text, out planet_velocity_start))      //hmmm no need to then reassign it
        {
            //velocities will be input in terms of the real velocity in AU/s, but need to be converted to the simulation speed
            // via the vel_ratio.
            rb_planet.velocity = new Vector3(0,0,float.Parse(planet_vel_UI.text)/vel_ratio);
        }
        else
        {
            rb_planet.velocity = new Vector3(0, 0, circ_velocities[0]);  //bodies start along the x axis, so require velocity tangentially along the z axis
        }
        

        if (float.TryParse(star_vel_UI.text, out star_velocity_start))      //hmmm no need to then reassign it
        {
            rb_star.velocity = new Vector3(0,0,float.Parse(star_vel_UI.text)/vel_ratio);
        }
        else
        {
            rb_star.velocity = new Vector3(0, 0, -circ_velocities[1]);
        }
    }

    //set the mass of the star and planet from the UI input
    private void SetRigidBodyMass()
    {
        if (float.TryParse(planet_mass_UI.text, out planet_mass))      //hmmm no need to then reassign it
        {
            rb_planet.mass = float.Parse(planet_mass_UI.text);
        }

        if (float.TryParse(star_mass_UI.text, out star_mass))      //hmmm no need to then reassign it
        {
            rb_star.mass = float.Parse(star_mass_UI.text);
        }
    }

    //When the reset button is clicked, planet and star positions should be set to the starting positions
    // Velocities and masses should be set to whatever is in the UI or their default values
    private void ResetSimulation()
    {
        float initial_dist;
        if(float.TryParse(start_distance.text, out initial_dist))
        {
            planet.position = new Vector3(-initial_dist, 0, 0);
        }
        else
        {
            planet.position = new Vector3(-1, 0, 0);        //if nothing input then start it at a distance of 1 (AU)
        }
        
        star.position = new Vector3(0, 0, 0);
        SetRigidBodyMass();         //set the masses first as they are required to set correct velocities
        SetVelocities();
        
        
    }

    //CALCULATIONS OF THE ORBITAL ENERGIES
    private float CalculateKE(float mass, float speed)
    {
        //float vel_ratio = Mathf.Sqrt(G_real / G);               //get the real velocity of the object rather than the increased simulation rate
        return 0.5f * mass * vel_ratio*speed * vel_ratio*speed;     //convert the energy into units of M(sun)AU^2s^-2
    }

    private float CalculateGPE(float othermass, float mass, float distance)
    {
        return -1 * G_real * othermass * mass / distance;       //energy in units as with KE
    }

    //Calculate the speed 
    private float CalculateSpeed(Vector3 currentPos, Vector3 lastPos)
    {
        float v = (currentPos - lastPos).magnitude / Time.fixedDeltaTime;       //speed in units/sec --> AU/sec
        return v;
    }
}
