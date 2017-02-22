using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Toolbox;
using Windows.Kinect;
using Assets.DataContracts;

public class CalibrationReach : MonoBehaviour {

    //===Send maxReachX/Y values over to KinectUICursor===
    //GameObject _player = GameObject.Find("KinectUICursor");
    //_player.GetComponent<KinectUICursor>();

    [SerializeField]
    protected KinectUIHandType _handType;

    public Text _text;
    private Toolbox _toolbox;
    private JointType _handJoint;

    private float _maxReachX;
    private float _maxReachY;

	// Use this for initialization
	void Start () {
        _toolbox = FindObjectOfType<Toolbox>();
        _handJoint = _handType == KinectUIHandType.Right ? JointType.HandRight : JointType.HandLeft;
	}
	
	// Update is called once per frame
	void Update () {
        printReach();

        // Get joints
        var body = _toolbox.BodySourceManager.GetFirstTrackedBody();
        if (body == null) return;
        var hand = body.Joints[_handJoint];
        var centerJoint = body.Joints[JointType.SpineShoulder];

        // calculate hand position relative to shoulder spine joint
        var distanceX = hand.Position.X - centerJoint.Position.X;
        var distanceY = hand.Position.Y - centerJoint.Position.Y;

        // Change _maxReachX/Y if its higher than current
        if (_maxReachX < distanceX)
        {
            _maxReachX = distanceX;
        }
        if (_maxReachY < distanceY)
        {
            _maxReachY = distanceY;
        }
        _toolbox.AppDataManager.Save(
            new MaxReach { X = _maxReachX, Y = _maxReachY }, JointType.HandRight);
        
	}

    void printReach()
    {
        _text.text = "Reach as far as you can to the star (Test)\n" +
            "Max X Reach = " + _maxReachX + "\n" +
            "Max Y Reach = " + _maxReachY;
    }
}
