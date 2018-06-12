using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script that attaches to a slider UI for adjusting the rate at which time flows in a scene.
//Rate of 1 is regular time
// Rate < 1 is slow motion. 0 is paused. Updates do not happen at 0 rate.

public class TimeRateScript : MonoBehaviour {

    public Slider timescale_slider;
    public InputField timescale_input;

	// Use this for initialization
	void Start () {
        timescale_slider.onValueChanged.AddListener(delegate { UpdateTimeScale(timescale_slider.value); });
        timescale_input.onEndEdit.AddListener(delegate { UpdateTimeScale(float.Parse(timescale_input.text)); });
	}
	
	

    private void UpdateTimeScale(float value)
    {
        //Update the slider and inputfield to ensure the correct value shown
        timescale_input.text = value.ToString("F2");
        timescale_slider.value = value;
        //Update the time scale.
        Time.timeScale = value;
        
    }
}
