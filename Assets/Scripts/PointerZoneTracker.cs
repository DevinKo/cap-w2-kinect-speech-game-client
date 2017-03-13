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
    private JointType _jointType;
    private float distance; //current distance from center
    private float radius; //radius to be stored to calibration contract
    private float pointerTime; //maxTime to be sent to calibration contract
    GameObject sphere;

    // Use this for initialization
    void Start ()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        timeLeft = maxTime;
        pointerTime = maxTime;
        //Start calibration with right hand
        _jointType = JointType.HandRight;

        //spawn primitive object sphere for testing
        GameObject sphere1 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere1.transform.position = new Vector3(-0.01F, -0.01F, -0.01F);
        sphere1.transform.localScale = new Vector3(.2F, .2F, .2F);
        sphere = sphere1;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (_toolbox.BodySourceManager == null) { return; }

        timeLeft -= Time.deltaTime;
        timerText.text = "Time left: " + timeLeft.ToString("f0");

        var relativePosition = _toolbox.BodySourceManager.GetRelativeJointPosition(JointType.SpineShoulder, _jointType);
        distance = Vector3.Distance(relativePosition, sphere.transform.position);
        if (Math.Abs(radius) < distance)
        {
            radius = distance;
        }

        if (timeLeft <= 0)
        {
            //store distance into radius
            // ... need to change radius into variable pointing to calibration contract
            //radiusSendToContract = radius;
            if (_jointType == JointType.HandRight)
            {
                // switch to left hand and reset timer
                _jointType = JointType.HandLeft;
                timeLeft = maxTime;

            }
            else
            {
                //turn off ReachManager object and activate AudioThresholdManager
                gameObject.SetActive(false);
                GameObject.Find("CalibrationManager/AudioThresholdManager").SetActive(true);
            }
        } 
        printReach();
    }

    // For testing
    void printReach()
    {
        // Display pointing zone distance
        testText.text = _jointType + " Radius: " + radius;

        // Display instructions
        instructionText.text = "Keep " + _jointType + " on the circle.";
    }
}
