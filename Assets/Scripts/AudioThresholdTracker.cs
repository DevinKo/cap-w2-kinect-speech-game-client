using UnityEngine;
using UnityEngine.UI;
using Assets.DataContracts;
using Assets.Toolbox;
using Windows.Kinect;
using System;

public class AudioThresholdTracker : MonoBehaviour
{
    // Used for displaying audio threshold (testing only)
    public Text testText;
    public Text timerText;
    public Text instructionText;
    float maxTime = 15.0f; //change calibration time here
    float timeLeft;

    private Toolbox _toolbox;
    private float lowestVolume;
    private float currentVolume;

    // Mock storage for threshold
    private float audioThresh;

    // Use this for initialization
    void Start ()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        timeLeft = maxTime;
        lowestVolume = 0; //it can only get louder
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (_toolbox.BodySourceManager == null) { return; }

        currentVolume = _toolbox.VolumeSourceManager.RawEnergy * 1000;

        if (currentVolume > lowestVolume)
        {
            lowestVolume = currentVolume;
        }

        // countdown from maxTime to zero
        timeLeft -= Time.deltaTime;
        timerText.text = "Time left: " + timeLeft.ToString("f0");

        if (timeLeft <= 0)
        {
            // store lowest volume as the lowest audio threshold value
            // ... still need to assign threshold to calibration contract instead of mock variable
            audioThresh = lowestVolume;

            // end audio threshold calibration and enter pointer zone calibration
            gameObject.SetActive(false);
            GameObject.Find("CalibrationManager/PointerZoneManager").SetActive(true);
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
