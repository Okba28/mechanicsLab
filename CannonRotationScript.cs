using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//For rotating the cannon
public class CannonRotationScript : MonoBehaviour {

    public int rotationOffset = 90;
    public Text angle_text;
    private bool cannonClicked;
    public Transform cannonParent;


    
    // Update is called once per frame
    void Update () {
        if (cannonClicked == true)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - cannonParent.position;
            difference.Normalize();

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            cannonParent.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);

            //update the UI to reflect the new angle
            UpdateAngle(Quaternion.Angle(cannonParent.rotation, Quaternion.identity));
        }
	}

    private void UpdateAngle(float angle)
    {
        angle_text.text = angle.ToString("F1");
    }

    private void OnMouseDown()
    {
        cannonClicked = true;
    }

    private void OnMouseUp()
    {
        cannonClicked = false;
    }
}
