using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StopClockScript : MonoBehaviour {

    //UI INterface buttons and display
    public Button startButton;
    public Text stopclockDisplay;

    private bool startClicked;      //booleans which state whether we have started or stopped the stopwatch

    private float startTime;        //the time in seconds when the start button was clicked
	// Use this for initialization
	void Start () {
        startClicked = false;   //initialised to be off
        
	}
	
	// Update is called once per frame
	void Update () {
		if(startClicked == true)
        {
            float deltaTime = Time.time - startTime;
            stopclockDisplay.text = deltaTime.ToString("F2");
        }
	}

    //Simply sets start to clicked. Will be run from editor when start button clicked.
    public void Clicked()
    {
        if (startClicked == false)
        {
            startClicked = true;
            startTime = Time.time;      //startTime set to the time when the button clicked - the time in seconds after the scene loaded
            startButton.GetComponentInChildren<Text>().text = "Stop";
        }
        else
        {
            startClicked = false;
            startButton.GetComponentInChildren<Text>().text = "Start";
        }

    }
    
}
