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


    public float density;           //density of ball material
    public float viscosity;         // viscosity of liquid
    public float radius;            //radius of ball
    private Rigidbody rb;

    //UI
    public Dropdown ball_material_dropdown;
    public Dropdown liquid_dropdown;


	// Use this for initialization
	void Start () {
        //get the rigidbody attached to the falling ball
        rb = transform.GetComponent<Rigidbody>();
        //set listeners for changing materials
        ball_material_dropdown.onValueChanged.AddListener(delegate { ChangeBallMaterial(); });
        liquid_dropdown.onValueChanged.AddListener(delegate { ChangeLiquid(); });
	}
	
	// Update is called once per frame
	void Update () {

        float drag = CalculateDrag();
        rb.AddForce(new Vector3(0, drag, 0), ForceMode.Force);
	}

    //Stoke's Law used to find the drag on the spherical falling ball.
    //Only recalculate the drag when a component changes
    private float CalculateDrag()
    {
        float drag = -1 * 6 * Mathf.PI * radius * viscosity * rb.velocity.y;     //ball will fall in the y direction and acts in the opposite direction to the velocity
        return drag;
    }

    private void ChangeBallMaterial()
    {

    }

    private void ChangeLiquid()
    {

    }

}
