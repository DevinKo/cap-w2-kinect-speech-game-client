using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class FakeBodySourceManager : MonoBehaviour, IBodySourceManager
{
    private Body _fakeBody;

    public void Awake()
    {
        _fakeBody = BuildFakeBody();
    }

    public Body BuildFakeBody()
    {
        var body = new Body();
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

    public Body GetFirstTrackedBody()
    {
        return _fakeBody; 
    }
}
