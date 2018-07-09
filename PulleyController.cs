using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pulley controller is attached to the pulley handle in the scene. Pulling on the handle moves the mass/unfixed pulleys the 
//appropriate distance vertically.
//It also displays the required force
public class PulleyController : MonoBehaviour{

    static GameObject[] pulleys;          //list of all the pulleys involved
    public static GameObject massAttached;        //the mass object in the scene

  

    //pulley position fields
    static Vector3 current_fixed_pulley_position;
    static Vector3 next_fixed_pulley_position;
    static Vector3 current_moveable_pulley_position;
    static Vector3 next_moveable_pulley_position;

    //pulley prefabs
    static public GameObject moveable_pulley_prefab;
    static public GameObject fixed_pulley_prefab;

    static float mech_advantage;           //the mechanical advantage is the ratio of the weight of the mass to the required pulling force
                                           //it is equal to the number of support ropes supporting the mass

    private void Start()
    {
        moveable_pulley_prefab = Resources.Load("MovingPulley") as GameObject;
        fixed_pulley_prefab = Resources.Load("FixedPulley") as GameObject;
        massAttached = GameObject.FindGameObjectWithTag("Mass");
        current_fixed_pulley_position = new Vector3(2f, 5, 0);
        current_moveable_pulley_position = new Vector3(0, 0, 0);
    }




    #region Methods

    public static void GetPulleysInScene()
    {
        pulleys = GameObject.FindGameObjectsWithTag("Pulley");      //get all the pulleys in the scene
    }

    public void GetMassInScene()
    {
        massAttached = GameObject.FindGameObjectWithTag("Mass");
    }

    public static void CalculateMechanicalAdvantage()
    {
        int numConnected = 2*massAttached.GetComponent<PulleyMass>().GetNumPulleysAttached();   //each pulley connected provides 2 support forces
        if (massAttached.GetComponent<PulleyMass>().GetSupportRopeAttached())
        {
            numConnected++;         //if a support rope is also connected then the number of supports increases
        }

        mech_advantage = numConnected;
        
    }

    public int GetNumPulleysInScene()
    {
        int count = pulleys.Length;
        return count;
    }

    public static float GetMechanicalAdvantage()
    {
        return mech_advantage;
    }

    public static void AddMoveablePulleyToScene()
    {
        GameObject new_moveable_pulley = Instantiate(moveable_pulley_prefab);
        new_moveable_pulley.transform.position = current_moveable_pulley_position;
        next_moveable_pulley_position = new Vector3(current_moveable_pulley_position.x - 4, current_moveable_pulley_position.y, current_moveable_pulley_position.z);
        current_moveable_pulley_position = next_moveable_pulley_position;
        
        
    }

    public static void AddFixedPulleyToScene()
    {
        GameObject new_fixed_pulley = Instantiate(fixed_pulley_prefab);
        new_fixed_pulley.transform.position = current_fixed_pulley_position;
        next_fixed_pulley_position = new Vector3(current_fixed_pulley_position.x - 4, current_fixed_pulley_position.y, current_fixed_pulley_position.z);
        current_fixed_pulley_position = next_fixed_pulley_position;


    }

    public static float AverageMoveablePulleyPositionX()
    {
        if (pulleys != null)
        {
            Vector3 pos = new Vector3();
            //the number of moveable pulleys
            int count = 0; ;
            foreach (GameObject pulley in pulleys)
            {
                Pulley p = pulley.GetComponent<Pulley>();
                if (!p.fixedPulley)
                {
                    pos += pulley.transform.position;
                    count++;
                }


            }
            if (count > 0)
            {
                Vector3 average_pos = pos / count;
                return average_pos.x;
            }
            else
            {
                return 0f;
            }
        } else
        {
            return 1f;
        }
    }

    #endregion
}
