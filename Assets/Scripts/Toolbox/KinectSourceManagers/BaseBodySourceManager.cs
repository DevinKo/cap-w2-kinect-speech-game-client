using Assets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Windows.Kinect;

public abstract class BaseBodySourceManager : MonoBehaviour
{
    public abstract Assets.Models.Body GetFirstTrackedBody();

    /// <summary>
    /// Returns the position of joint relative to originJoint
    /// </summary>
    /// <param name="originJoint"></param>
    /// <param name="joint"></param>
    /// <returns></returns>
    public Vector3 GetRelativeJointPosition(JointType originJointType, JointType jointType)
    {
        var body = GetFirstTrackedBody();
        if (body == null)
        {
            return new Vector3();
        }
        var originJoint = body.Joints[originJointType];
        var joint = body.Joints[jointType];
        var vectorOrigin = new Vector3(originJoint.Position.x, originJoint.Position.y, originJoint.Position.z);
        var vector = new Vector3(joint.Position.x, joint.Position.y, originJoint.Position.z);

        return vector - vectorOrigin;
    }
}
