using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script for generating driven oscillations of a harmonic oscillator
// Oscillating mass keeps the SHMFreeOscillations script and this script then adds an additional force.

public class DrivenOscillationsScript : MonoBehaviour
{

    public Transform oscillator;    //the mass that is oscillating
    
    public Transform driver;                //the anchor block which is driving the oscillations
    public float driver_amplitude;            //the maximum displacement of the driver from its initial position
    private float equilibrium_position_y;

    private Rigidbody rb;           // the rigidbody component of the oscillator
    private float period = 0;           //the period of the driving force, defaults to 0.

    private float startTime;

    //UI fields
    public InputField driving_frequency;        //the frequency of the driver
    public Slider driving_freq_slider;          //slider for changing frequency

    // Use this for initialization
    void Start()
    {
        rb = oscillator.GetComponent<Rigidbody>();      //get the rigidbody component
        //Update the period/frequency if the driving frequency inputfield is changed
        driving_frequency.onValueChanged.AddListener(delegate { UpdatePeriod(); });
        //the slider updates the value in the frequency input field, thereby calling the above listener as well as that below
        driving_freq_slider.onValueChanged.AddListener(delegate { UpdateUI(driving_freq_slider.value); });
        equilibrium_position_y = driver.position.y;
    }

    // Update is called once per frame
    //The driving force needs to be a sinusoidal force - varying continuously over the period of the driver.
    void FixedUpdate()
    {
        if (period > 0) //only add a force if a period is defined - 0 period removes the driving force
        {
            float cycle_fraction = ((Time.time - startTime) % period)/period;
            driver.position = new Vector3(driver.position.x, equilibrium_position_y + Mathf.Sin(cycle_fraction*2*Mathf.PI) * driver_amplitude, driver.position.z);
            
        }


    }

    //changes the period of the driving force
    private void UpdatePeriod()
    {
        float freq;
        if (float.TryParse(driving_frequency.text,out freq))
        {
            if (freq > 0)
            {
                period = 1 / freq;
                startTime = Time.time;      //the time at which the latest driving freq started
            }
            else
            {
                period = 0;
            }

            UpdateUI(freq);
        }
    }

    //updates the value of the frequency depending on the slider value
    private void UpdateUI(float value)
    {
        driving_frequency.text = value.ToString("F2");
        driving_freq_slider.value = value;
    }
}
