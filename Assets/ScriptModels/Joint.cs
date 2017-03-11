using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Windows.Kinect;

namespace Assets.Models
{
    public class Joint
    {
        public Vector3 Position { get; set; }

        public JointType JointType;

        public TrackingState TrackingState { get; set; }

        public static implicit operator Joint(Windows.Kinect.Joint kinectJoint)
        {
            var joint = new Joint();
            joint.Position = new Vector3(
                kinectJoint.Position.X,
                kinectJoint.Position.Y,
                kinectJoint.Position.Z);
            joint.JointType = kinectJoint.JointType;
            joint.TrackingState = kinectJoint.TrackingState;
            return joint;
        }
    }
}
