using UnityEngine;
using Assets.Toolbox;
using Windows.Kinect;
using System;

public class ReachTracker : MonoBehaviour
{
    private Toolbox _toolbox;
    public JointType _jointType;
    private Vector3 _maxReach = new Vector3();

    public void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        
    }

    public void Update()
    {
        if (_toolbox.BodySourceManager == null)
        {
            return;
        }

        // find relative distance of joint from top of spine.
        var relativePosition = _toolbox.BodySourceManager.GetRelativeJointPosition(JointType.SpineShoulder, _jointType);
        if (Math.Abs(_maxReach.x) < Math.Abs(relativePosition.x))
        {
            _maxReach.x = Math.Abs(relativePosition.x);
        }
        if (Math.Abs(_maxReach.y) < Math.Abs(relativePosition.y))
        {
            _maxReach.y = Math.Abs(relativePosition.y);
        }
    }
}

