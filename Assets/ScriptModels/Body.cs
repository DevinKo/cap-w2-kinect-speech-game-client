using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Models
{
    public class Body
    {
        public IDictionary<Windows.Kinect.JointType, Joint> Joints = new Dictionary<Windows.Kinect.JointType, Joint>();

        public static implicit operator Body(Windows.Kinect.Body kinectBody)
        {
            var body = new Body();
            foreach(var kinectJoint in kinectBody.Joints)
            {
                Joint joint = kinectJoint.Value;
                body.Joints.Add(kinectJoint.Key, joint);
            }

            return body;
        }
    }
}
