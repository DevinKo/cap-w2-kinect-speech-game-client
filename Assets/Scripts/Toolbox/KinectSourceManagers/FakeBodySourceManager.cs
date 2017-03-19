using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Windows.Kinect;

public class FakeBodySourceManager : BaseBodySourceManager, IBodySourceManager
{
    private Assets.Models.Body _fakeBody;

    public void Awake()
    {
        _fakeBody = BuildFakeBody();
    }

    public Assets.Models.Body BuildFakeBody()
    {
        var body = new Assets.Models.Body();
        var jointTypes = Enum.GetValues(typeof(Windows.Kinect.JointType)).Cast<Windows.Kinect.JointType>();
        foreach (var jointType in jointTypes)
        {
            var joint = new Assets.Models.Joint();
            joint.Position = new Vector3(1.252532f, 1.545345f, 3.35253f);
            joint.JointType = jointType;
            body.Joints.Add(jointType, joint);
        }

        return body;
    }

    public override Assets.Models.Body GetFirstTrackedBody()
    {
        return _fakeBody; 
    }

    public override Assets.Models.Joint GetJoint(JointType jointType)
    {
        return _fakeBody.Joints[jointType];
    }
}
