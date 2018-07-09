using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddFixedPulley : MonoBehaviour {

    
    private GameObject mass;         //the mass in the scene
    private PulleyMass pulley_mass;     //the pulleymass component
    private int num_fixed_pulleys;

    private void Start()
    {
        mass = GameObject.FindGameObjectWithTag("Mass");
        pulley_mass = mass.GetComponent<PulleyMass>();
        Debug.Log("STARTED");
    }


    public void AddNewFixedPulley()
    {
        //fixed pulleys add a support rope to the mass
        pulley_mass.AddSupportRope();
        PulleyController.AddFixedPulleyToScene();
        PulleyController.GetPulleysInScene();   //add all the pulleys back to the list of pulleys
        PulleyController.CalculateMechanicalAdvantage();
        num_fixed_pulleys++;

    }


}
