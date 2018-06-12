using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowHideTextScript : MonoBehaviour {

    public Text text;

    public void ShowHideText()
    {
        if(text.IsActive())
        {
            text.enabled = false;
        }
        else
        {
            text.enabled = true;
        }
    }
}
