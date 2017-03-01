using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.DataContracts;
using Assets.Toolbox;
using Windows.Kinect;
using System;

public class PointerZoneTracker : MonoBehaviour
{
    // Used for displaying audio threshold (testing only)
    public Text testText;
    public Text timerText;
    public Text instructionText;
    float maxTime = 15.0f; //change calibration time here
    float timeLeft;

    private Toolbox _toolbox;
    private float distance; //current distance from center
    private float radius; //radius to be stored to calibration contract
    private float pointerTime; //maxTime to be sent to calibration contract


    // Use this for initialization
    void Start ()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        timeLeft = maxTime;
        pointerTime = maxTime;

        //spawn primitive object sphere for testing
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(-0.01F, -0.01F, -0.01F);
        sphere.transform.localScale = new Vector3(.2F, .2F, .2F);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_toolbox.BodySourceManager == null) { return; }

        timeLeft -= Time.deltaTime;
        timerText.text = "Time left: " + timeLeft.ToString("f0");

        if (timeLeft <= 0)
        {
            //store distance into radius
            // ... need to change radius into variable pointing to calibration contract
            radius = pointerTime;
        }

        printReach();
    }

    // For testing
    void printReach()
    {
        // Display pointing zone distance
        testText.text = "Radius: " + radius;

        // Display instructions
        instructionText.text = "Keep your hand on the circle.";
    }
}
