using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//attached to the falling ball object itself
//Calculates the Stokes law drag force acting on the ball and applies the force each fixed update
//Gravitational force acts due to the rigidbody component added to the falling ball
//Can change the material the ball is made from.
//Can set the radius of the ball - which should then set the rigidbody mass after calculating from density and r.
//Can change viscous liquid.
//Can change timescale in order to allow easier measurements for less viscous liquids.
public class FallingBallViscometerScript : MonoBehaviour {

    public float density;           //density of ball material in kg/m3
    public float viscosity;         // viscosity of liquid in Pa.s
    public float radius;            //radius of ball in cm
    private Rigidbody rb;           //mass units of kg
    private float weight;

    //UI
    public Dropdown ball_material_dropdown;
    public Dropdown liquid_dropdown;
    public InputField radius_input;

	// Use this for initialization
	void Start () {
        //get the rigidbody attached to the falling ball
        rb = transform.GetComponent<Rigidbody>();
        //set listeners for changing materials
        ball_material_dropdown.onValueChanged.AddListener(delegate { ChangeBallMaterial(); });
        liquid_dropdown.onValueChanged.AddListener(delegate { ChangeLiquid(); });
        radius_input.onEndEdit.AddListener(delegate { ChangeRadius(); });

        //set starting properties
        radius = 1f;
        viscosity = 8.9E-4f;
        density = 8050f;
        rb.mass = CalculateMass(density, radius/100);
        weight = rb.mass * Physics.gravity.y;
        radius_input.text = "1";

        //set physics timescale
        Time.fixedDeltaTime = 0.01f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float drag = CalculateDrag();
        rb.AddForce(new Vector3(0,weight,0), ForceMode.Force);
        rb.AddForce(new Vector3(0, drag, 0), ForceMode.Force);
        
        
    }

    //Stoke's Law used to find the drag on the spherical falling ball.
    //Only recalculate the drag when a component changes
    private float CalculateDrag()
    {
        float vel = rb.velocity.y;
        //drag is only applied if the ball is falling 
        if (vel < 0)
        {
            float drag = -1 * 6 * Mathf.PI * radius / 100 * viscosity * vel;     //ball will fall in the y direction and drag acts in the opposite direction to the velocity
            return drag;
        }
        else
        {
            return 0;
        }
    }

    // Change material runs when the material dropdown menu is changed.
    // The radius of the ball is set from an input field.
    // This function needs to set the mass of the ball's rigidbody
    private void ChangeBallMaterial()
    {
        //steel density = 8050 kg/m3
        if(ball_material_dropdown.value == 0)
        {
            density = 8050f;
            
        }
        //aluminium density = 2700 kg/m3
        else if(ball_material_dropdown.value == 1)
        {
            density = 2700f;
        }
        //copper density = 8960 kg/m3
        else
        {
            density = 8960f;
        }
        //set the mass of the ball based on the newly set density and previously set radius
        rb.mass = CalculateMass(density, radius/100);
        weight = rb.mass * Physics.gravity.y;
        ResetBallPosition();
    }

    //ChangeLiquid sets the viscosity of the liquid
    private void ChangeLiquid()
    {
        //water viscosity = 8.9 E-4 Pa.s
        if (liquid_dropdown.value == 0)
        {
            viscosity = 8.9E-4f;
        }
        //honey viscosity = 10 Pa.s
        else if (liquid_dropdown.value == 1)
        {
            viscosity = 10f;
        }
        //motor oil 0.2 Pa.s
        else if(liquid_dropdown.value == 2)
        {
            viscosity = 0.2f;
        }
        //air 20 E-6 Pa.s
        else
        {
            viscosity = 20E-6f;
        }

        ResetBallPosition();
    }

    //Change radius sets the radius variable of the ball
    // Also changes the physical scale of ball in the simulation
    //Runs when a new value is entered in the radius input field
    private void ChangeRadius()
    {
        float r;
        if(float.TryParse(radius_input.text,out r))
        {
            radius = r;         //set the radius variable
            transform.localScale = new Vector3(r, r, r);    //set the scale of the falling ball gameobject
        }
        rb.mass = CalculateMass(density, radius/100);
        weight = rb.mass * Physics.gravity.y;
        ResetBallPosition();
    }

    private float CalculateMass(float density, float r)
    {
        float M = (4 / 3) * Mathf.PI * (r * r * r) * density;
        return M;
    }

    private void ResetBallPosition()
    {
        transform.position = new Vector3(0, 10, 0);
        rb.velocity = new Vector3(0, 0, 0);
    }

}
