using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Windows.Kinect;

public interface IBodySourceManager
{
    Assets.Models.Body GetFirstTrackedBody();
    Vector3 GetRelativeJointPosition(JointType originJointType, JointType jointType);
}
