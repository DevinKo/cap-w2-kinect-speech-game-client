using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.DataContracts;

namespace Assets.MockDataContracts
{
    public static class MockSnapshots
    {
        public static BodySnapshot BodySnapshot1 = new BodySnapshot()
        {
            Joints = new Joint[]
            {
                MockJoints.AnkleLeft,
                MockJoints.AnkleRight,
                MockJoints.ElbowLeft,
                MockJoints.ElbowRight,
                MockJoints.FootLeft,
                MockJoints.FootRight,
                MockJoints.HandLeft,
                MockJoints.HandRight,
                MockJoints.HandTipLeft,
                MockJoints.HandTipRight,
                MockJoints.Head,
                MockJoints.HipLeft,
                MockJoints.HipRight,
                MockJoints.KneeLeft,
                MockJoints.KneeRight,
                MockJoints.Neck,
                MockJoints.ShoulderLeft,
                MockJoints.ShoulderRight,
                MockJoints.SpineBase,
                MockJoints.SpineMid,
                MockJoints.SpineShoulder,
                MockJoints.ThumbLeft,
                MockJoints.ThumbRight,
                MockJoints.WristLeft,
                MockJoints.WristRight
            }
        };
    }
}
