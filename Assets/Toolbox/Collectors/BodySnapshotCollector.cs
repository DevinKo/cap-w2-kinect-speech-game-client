using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Assets.Scripts;
using Assets.DataContracts;
using UnityEngine;
using System;

namespace Assets.Toolbox
{
    public class BodySnapshotCollector : MonoBehaviour
    {
        private Toolbox _toolbox;

        public struct Client
        {
            public object sender;
            public List<BodySnapshot> snapshots;
        }

        private List<Client> _clients = new List<Client>();

        public List<JointType> JointsOfInterest = new List<JointType>{JointType.ElbowLeft,
            JointType.ElbowRight,
            JointType.HandLeft,
            JointType.HandRight,
            JointType.HandTipLeft,
            JointType.HandTipRight,
            JointType.Head,
            JointType.Neck,
            JointType.ShoulderLeft,
            JointType.ShoulderRight,
            JointType.SpineBase,
            JointType.SpineMid,
            JointType.SpineShoulder,
            JointType.ThumbLeft,
            JointType.ThumbRight,
            JointType.WristLeft,
            JointType.WristRight
        };

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();

            StartCoroutine("CollectBodySnapshots");
        }

        public void Update()
        {
           
        }

        public void StartCollectBodySnapshots(object sender, List<BodySnapshot> bodySnapshotList)
        {
            _clients.Add(new Client() { sender = sender, snapshots = bodySnapshotList });
        }

        public void StopCollectBodySnapshots(object sender)
        {
            _clients.Remove(_clients.Find(c => c.sender == sender));
        }

        public IEnumerator CollectBodySnapshots()
        {
            while (true)
            {
                if (_toolbox.BodySourceManager == null)
                {
                    yield return null;
                    continue;
                }
                var body = _toolbox.BodySourceManager.GetFirstTrackedBody();
                if (body == null)
                {
                    yield return null;
                    continue;
                }

                var joints = new List<Assets.DataContracts.Joint>();

                foreach (var jointType in JointsOfInterest)
                {
                    var joint = body.Joints[jointType];
                    var pos = joint.Position;
                    joints.Add(new DataContracts.Joint()
                    {
                        JointType = jointType.ToString(),
                        X = pos.x,
                        Y = pos.y,
                        Z = pos.z
                    });
                }

                var snapshot = new BodySnapshot()
                {
                    Joints = joints.ToArray(),
                    Time = DateTime.Now.ToString("s"),
                };

                foreach(var client in _clients)
                {
                    client.snapshots.Add(snapshot);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

        /// <summary>
        /// Returns 0 if value can not be determined. value is the distance bewteen
        /// the players hands in meters.
        /// </summary>
        /// <returns></returns>
        public float GetHandSeparation()
        {
            if (_toolbox.BodySourceManager == null)
            {
                return 0;
            }

            var body = _toolbox.BodySourceManager.GetFirstTrackedBody();
            if (body == null)
            {
                return 0;
            }

            var handRight = body.Joints[JointType.HandRight];
            var handLeft = body.Joints[JointType.HandLeft];
            if (handRight.TrackingState == TrackingState.NotTracked
                || handLeft.TrackingState == TrackingState.NotTracked)
            {
                return 0;
            }

            var handLeftVec = new Vector3
            {
                x = handLeft.Position.x,
                y = handLeft.Position.y,
                z = handLeft.Position.z
            };
            var handRightVec = new Vector3
            {
                x = handRight.Position.x,
                y = handRight.Position.y,
                z = handRight.Position.z,
            };
            
            return Vector3.Distance(handLeftVec, handRightVec);

        }
    }
}
