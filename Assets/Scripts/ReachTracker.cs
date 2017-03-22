using UnityEngine;
using UnityEngine.UI;
using Assets.DataContracts;
using Assets.Toolbox;
using Windows.Kinect;
using System;

public class ReachTracker : MonoBehaviour
{
	// Used for displaying max reach distance (testing only)
	public Text testText;
	public Text timerText;
	public Text instructionText;
	float maxTime = 5.0f; //change calibration time per hand here
	float timeLeft;

	private Toolbox _toolbox;
	private JointType _jointType;
	private Vector3 _maxReach = new Vector2();
    private Vector3 _maxReachRight = new Vector3();

	// Use this for initialization
	void Start()
	{
		_toolbox = FindObjectOfType<Toolbox>();
		timeLeft = maxTime;
		//Start calibration with right hand
		_jointType = JointType.HandRight;
	}

	// Update is called once per frame
	void Update()
	{
		if (_toolbox.BodySourceManager == null) { return; }

        // Check if joint is being tracked
        var joint = _toolbox.BodySourceManager.GetJoint(_jointType);
        var shoulderJoint = _toolbox.BodySourceManager.GetJoint(JointType.SpineShoulder);
        if (joint == null) { return; }

        if (joint.TrackingState == TrackingState.NotTracked
            || shoulderJoint.TrackingState == TrackingState.NotTracked)
        {
            return;
        }

		// find relative distane of joint from top of spine.
		var relativePosition = _toolbox.BodySourceManager.GetRelativeJointPosition(JointType.SpineShoulder, _jointType);
		if (Math.Abs(_maxReach.x) < Math.Abs(relativePosition.x))
		{
			_maxReach.x = Math.Abs(relativePosition.x);
		}
		if (Math.Abs(_maxReach.y) < Math.Abs(relativePosition.y))
		{
			_maxReach.y = Math.Abs(relativePosition.y);
		}

		// countdown from maxTime to zero
		timeLeft -= Time.deltaTime;
		timerText.text = "Time left: " + timeLeft.ToString("f0");

		if (timeLeft <= 0)
		{
			// store left and right hand max reach distances
			_toolbox.AppDataManager.Save(
				new MaxReach { X = _maxReach.x, Y = _maxReach.y }, _jointType);


			// transition from right hand to left hand
			if (_jointType == JointType.HandRight)
			{
                _maxReachRight = _maxReach;
				// switch to left hand and reset timer
				_jointType = JointType.HandLeft;
				_maxReach.x = 0;
				_maxReach.y = 0;
				timeLeft = maxTime;
			}
			// transition to next calibration manager
			else
			{
                // Give max reach values to listeners
                _toolbox.EventHub.CalibrationScene.RaiseMaxReachCaptured(_maxReach, _maxReachRight);

				timeLeft = 0.5f;
				//turn off ReachManager object and activate AudioThresholdManager
				GetComponent<AudioThresholdTracker> ().enabled = !GetComponent<AudioThresholdTracker> ().enabled;
				this.enabled = !this.enabled;
			}
		}

		printReach();
	}

	// For testing
	void printReach()
	{
		// Display reach distance
		testText.text = "HandType = " + _jointType.ToString() + "\n" +
			"Max X Reach = " + _maxReach.x + "\n" +
			"Max Y Reach = " + _maxReach.y;

		// Display instructions
		instructionText.text = "Instructions: Draw a circle with your " + _jointType.ToString();
	}
}
