using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.DataContracts;
using Assets.Toolbox;
using Windows.Kinect;
using System;
using Constants;
using System.Linq;
using Assets.Scripts.Utility;

public class PointerZoneTracker : MonoBehaviour
{
	public Text testText; // Used for displaying audio threshold (testing only)
	public Text timerText;
	public Text instructionText;
	public float maxTime = 5.0f; //change calibration time here
	float timeLeft;

	private Toolbox _toolbox;
	private JointType _jointType;
	private float distance; //current distance from center
	private float radius; //radius to be stored to calibration contract
	private float pointerTime; //maxTime to be sent to calibration contract
	GameObject sphere;
    private List<float> radiusSamples = new List<float>();

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

        var handLeftObject = BaseSceneManager.Instance.GetObjectWithName(GameObjectName.HandLeft);
        var handRightObject = BaseSceneManager.Instance.GetObjectWithName(GameObjectName.HandRight);
        if (handLeftObject == null || handRightObject == null) { return; }

        var handLeft = handLeftObject.GetComponent<KinectUICursor>();
        var handRight = handRightObject.GetComponent<KinectUICursor>();
        if (handLeft == null || handRight == null) { return; }
        else if (handLeft.TrackingState == TrackingState.NotTracked || handRight.TrackingState == TrackingState.NotTracked) { return; }

        // convert to absolute values
        var scalar = Cursor.Instance.GetScale();

        var relativePosRight = sphere.transform.position - handLeft.transform.position;
        var relativePosLeft = sphere.transform.position - handRight.transform.position;

        var kinectPosRight = new Vector3(relativePosRight.x / scalar.x, relativePosRight.y / scalar.y, 0);
        var kinectPosLeft = new Vector3(relativePosLeft.x / scalar.x, relativePosLeft.y / scalar.y, 0);

        var kinectDistanceRight = kinectPosRight.magnitude;
        var kinectDistanceLeft = kinectPosLeft.magnitude;
        
        var maxDistance = kinectDistanceLeft > kinectDistanceRight ? kinectDistanceLeft : kinectDistanceRight;

        radiusSamples.Add(maxDistance);

        timeLeft -= Time.deltaTime;
		timerText.text = "Time left: " + timeLeft.ToString("f0");
        
		if (timeLeft <= 0)
		{
            var averageRadius = radiusSamples.Average();
            var stdDev = MathExt.CalculateStdDev(radiusSamples);


            // Send radius info to listeners
            _toolbox.EventHub.CalibrationScene.RaiseRadiusCaptured(averageRadius + stdDev);
            // Send timer zone duration infor to listeners
            _toolbox.EventHub.CalibrationScene.RaisePointerZoneDurationCaptured(5f);
            // End scene
            _toolbox.EventHub.CalibrationScene.RaiseSceneEnd();

        }
        printReach();
	}

	// For testing
	void printReach()
	{
		// Display pointing zone distance - TEST ONLY
		/*
		testText.text = _jointType + " Radius: " + radius;
		*/

		// Display instructions
		instructionText.text = "  Keep both hands on the circle.";
	}
}
