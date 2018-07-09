using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProjectilesUIScript : MonoBehaviour {

    public Text vel_text;
    public Text drag_text;
    public InputField grav_text;
    public float min_speed = 5;
    public float max_speed = 20;
    private float min_drag = 0;
    private float max_drag = 10;

    private void Start()
    {
        vel_text.text = "10.0";
        drag_text.text = "0.0";
        grav_text.text = "-9.8";

        //listener for gravity setting being changed
        grav_text.onEndEdit.AddListener(delegate { SetGravity(); });

    }
    //For the UI buttons for increasing and decreasing velocity
    public void IncreaseVelocity()
    {
        float newVel = float.Parse(vel_text.text);
        newVel += 1;
        if (newVel <= max_speed)       //maximum speed for projectile
        {
            vel_text.text = newVel.ToString("F1");
        }
    }

    public void DecreaseVelocity()
    {
        float newVel = float.Parse(vel_text.text);
        newVel -= 1;
        if(newVel >= min_speed) //minimum speed for projectile
        {
            vel_text.text = newVel.ToString("F1");
        }
        
    }

    public void IncreaseDrag()
    {
        float newDrag = float.Parse(drag_text.text);
        newDrag += 0.1f;
        if (newDrag <= max_drag) //max drag = 10
        {
            drag_text.text = newDrag.ToString("F1");
        }

    }

    public void DecreaseDrag()
    {
        float newDrag = float.Parse(drag_text.text);
        newDrag -= 0.1f;
        if (newDrag >= min_drag) //min drag = 0
        {
            drag_text.text = newDrag.ToString("F1");
        }

    }

    //When the UI value of gravity is changed via the increase button, automatically update the Physics engine gravity setting
    public void IncreaseGravity()
    {
        float newGrav = float.Parse(grav_text.text);
        newGrav -= 0.1f;        //increasing gravity assumed to mean making it more negative
        grav_text.text = newGrav.ToString("F1");
        Physics.gravity = new Vector3(0, newGrav, 0);       //gravity acts in the y direction
    }

    public void DecreaseGravity()
    {
        float newGrav = float.Parse(grav_text.text);
        newGrav += 0.1f;        //decreasing gravity assumed to mean making it less negative
        grav_text.text = newGrav.ToString("F1");
        Physics.gravity = new Vector3(0, newGrav, 0);       //gravity acts in the y direction
    }

    //if a value for gravity is input, then set gravity to that value
    public void SetGravity()
    {
        float g;
        if(float.TryParse(grav_text.text,out g))
        {
            g = -1*Mathf.Abs(g);        //ensure that g is always negative
            Physics.gravity = new Vector3(0, g, 0);
        }
        
    }
}
