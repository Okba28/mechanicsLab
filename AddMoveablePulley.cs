using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMoveablePulley : MonoBehaviour {

    private GameObject mass;         //the mass in the scene
    private PulleyMass pulley_mass;     //the pulleymass component

    private int num_moveable_pulleys;

    private void Start()
    {
        mass = GameObject.FindGameObjectWithTag("Mass");
        pulley_mass = mass.GetComponent<PulleyMass>();
    }

    //when a new pulley is added
    public void AddNewMoveablePulley()
    {
        pulley_mass.AddPulleyToMass();          //add a support pulley to the mass
        //when a moveable pulley is added there is no longer a SupportRope
        pulley_mass.RemoveSupportRope();
        PulleyController.AddMoveablePulleyToScene();
        PulleyController.GetPulleysInScene();   //add all the pulleys back to the list of pulleys
        PulleyController.CalculateMechanicalAdvantage();
        num_moveable_pulleys++;
        
    }
}
