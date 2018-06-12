using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMaterialScript : MonoBehaviour {

    public Transform mass;
    public Transform ramp;
    //materials used for the ramp
    public PhysicMaterial wood;
    public PhysicMaterial copper;
    public PhysicMaterial ice;
    public PhysicMaterial graphite;
    public PhysicMaterial glass;
    public PhysicMaterial frictionless;

    // Use this for initialization
    void Start () {
		
	}

    //The material friction coefficient is applied to the calculations when the collision between the mass and ramp occurs.
    //Need to set the ramp material and then raise the mass slightly in order for the collision to register
    public void SetMaterial(string material)
    {
        PhysicMaterial material_set;
        if (material == "wood")
        {
            mass.GetComponent<BoxCollider>().material = wood;
            ramp.GetComponent<BoxCollider>().material = wood;
            material_set = wood;
        }
        else if (material == "ice")
        {
            mass.GetComponent<BoxCollider>().material = ice;
            ramp.GetComponent<BoxCollider>().material = ice;
            material_set = ice;
        }
        else if (material == "graphite")
        {
            mass.GetComponent<BoxCollider>().material = graphite;
            ramp.GetComponent<BoxCollider>().material = graphite;
            material_set = graphite;
        }
        else if (material == "glass")
        {
            mass.GetComponent<BoxCollider>().material = glass;
            ramp.GetComponent<BoxCollider>().material = glass;
            material_set = glass;
        }
        else if (material == "copper")
        {
            mass.GetComponent<BoxCollider>().material = copper;
            ramp.GetComponent<BoxCollider>().material = copper;
            material_set = copper;
        }
        else
        {
            material_set = frictionless;
        }

        mass.position = new Vector3(mass.position.x, mass.position.y + 1f, mass.position.z);


    }
}
