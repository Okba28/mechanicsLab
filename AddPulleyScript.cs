using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddPulleyScript : MonoBehaviour {

    bool fixedNext;         //true if a fixed pulley is next to be added

    private GameObject mass;         //the mass in the scene
    private PulleyMass pulley_mass;     //the pulleymass component

    private int num_moveable_pulleys;
    private int num_fixed_pulleys;

    //UI elements
    public Text mechAdvantageText;


    private void Start()
    {
        fixedNext = true;       //we want to start with a fixed pulley

        mass = GameObject.FindGameObjectWithTag("Mass");
        pulley_mass = mass.GetComponent<PulleyMass>();
    }

    public void AddNextPulley()
    {
        if (fixedNext)
        {
            AddNewFixedPulley();
        }
        else
        {
            AddNewMoveablePulley();
        }
        //inverted fixed next
        fixedNext = !fixedNext;
        mechAdvantageText.text = PulleyController.GetMechanicalAdvantage().ToString("F0");
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
