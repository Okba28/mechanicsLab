using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProjectilesUIScript : MonoBehaviour {

    public Text vel_text;
    public Text drag_text;
    public float min_speed = 5;
    public float max_speed = 20;
    private float min_drag = 0;
    private float max_drag = 1;

    private void Start()
    {
        vel_text.text = "10.0";
        drag_text.text = "0.0";
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
        if (newDrag >= min_drag) //max drag = 1
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
}
