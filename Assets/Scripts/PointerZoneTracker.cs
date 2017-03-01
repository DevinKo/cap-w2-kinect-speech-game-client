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
    private float radius;
    private float pointerTime;


    // Use this for initialization
    void Start ()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        timeLeft = maxTime;

        //spawn primitive object sphere for testing
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = new Vector3(-0.01F, -0.01F, -0.01F);
        sphere.transform.localScale = new Vector3(.2F, .2F, .2F);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_toolbox.BodySourceManager == null) { return; }

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
