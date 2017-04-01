using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Toolbox;
using Windows.Kinect;

public class CalibrationStart : MonoBehaviour 
{
	public Text testText; // Used for displaying audio threshold (testing only)
	public Text timerText;
	public Text instructionText;
	public float maxTime = 5.0f; //change calibration time here
	float timeLeft;

	private Toolbox _toolbox;

	// Use this for initialization
	void Start () 
	{
		_toolbox = FindObjectOfType<Toolbox>();
		timeLeft = maxTime;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Check if joint is being tracked
		var rightJoint = _toolbox.BodySourceManager.GetJoint(JointType.HandRight);
		var leftJoint = _toolbox.BodySourceManager.GetJoint(JointType.HandLeft);
		if (rightJoint == null || leftJoint == null) { return; }

		// Don't countdown if any of the hands aren't tracked
		if (rightJoint.TrackingState == TrackingState.NotTracked
			|| leftJoint.TrackingState == TrackingState.NotTracked)
		{
			return;
		}

		timeLeft -= Time.deltaTime;
		timerText.text = "Time left: " + timeLeft.ToString("f0");

		if (timeLeft <= 0)
		{
			// turn off ReachManager object and activate PointerZoneTracker
			GetComponent<ReachTracker> ().enabled = !GetComponent<ReachTracker> ().enabled;
			this.enabled = !this.enabled;
		}

		// Display instructions
		instructionText.text = "  Raise your hands and get ready!";
	}
}
