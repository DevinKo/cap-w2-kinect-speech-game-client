using UnityEngine;
using UnityEngine.UI;
using Assets.DataContracts;
using Assets.Toolbox;
using Windows.Kinect;
using System;
using System.Collections.Generic;
using Assets.Scripts.Utility;
using System.Linq;

public class AudioThresholdTracker : MonoBehaviour
{
	// Used for displaying audio threshold (testing only)
	public Text testText;
	public Text timerText;
	public Text instructionText;
	float maxTime = 5.0f; //change calibration time here
	float timeLeft;

	private Toolbox _toolbox;

    private List<float> volumeSamples = new List<float>();

	private float currentVolume;

	// Mock storage for threshold
	private float audioThresh;

	// Use this for initialization
	void Start ()
	{
		_toolbox = FindObjectOfType<Toolbox>();
		timeLeft = maxTime;
		
	}

	// Update is called once per frame
	void Update ()
	{
		if (_toolbox.VolumeSourceManager == null) { return; }

		currentVolume = _toolbox.VolumeSourceManager.Decibel();

        volumeSamples.Add(currentVolume);
        
		// countdown from maxTime to zero
		timeLeft -= Time.deltaTime;
		timerText.text = "Time left: " + timeLeft.ToString("f0");

		if (timeLeft <= 0)
		{
            // take average volume
            var averageVolume = volumeSamples.Average();

            var stdDev = MathExt.CalculateStdDev(volumeSamples);

            // This allows any listeners to save the audio threshold
            _toolbox.EventHub.CalibrationScene.RaiseAudioThresholdCaptured(averageVolume + stdDev);
			
            // end audio threshold calibration and enter pointer zone calibration
			GetComponent<PointerZoneTracker> ().enabled = !GetComponent<PointerZoneTracker> ().enabled;
			this.enabled = !this.enabled;
		}

		printReach();
	}

	// For testing
	void printReach()
	{
		// Display reach distance
		testText.text = "Lowest Volume: " + lowestVolume; 

		// Display instructions
		instructionText.text = "Please be as quiet as possible for the remaining time.";
	}
}
