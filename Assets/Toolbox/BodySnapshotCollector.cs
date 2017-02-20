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
        private BodySourceManager _bodyManager;
        private Toolbox _toolbox;

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();
            StartCoroutine("RecordSnapshot");
            StartCoroutine("RecordAudioSnapshots");
        }

        public void Update()
        {

        }

        public IEnumerator RecordAudioSnapshots()
        {
            while (true)
            {
                if (!_toolbox.VolumeCollector)
                {
                    yield return null;
                    continue;
                }
                var audioSnapshot = new AudioSnapshot()
                {
                    Intensity = _toolbox.VolumeCollector.Decibel,
                    Time = DateTime.Now.ToString("s"),
                };
                _toolbox.AppDataManager.Save(audioSnapshot);
                yield return new WaitForSeconds(0.1f);
            }
        }

        public IEnumerator RecordSnapshot()
        {
            while (true)
            {
                ////////// Test
                //var s = Assets.MockDataContracts.MockSnapshots.BodySnapshot1;
                //s.Time = Time.time;
                //_toolbox.AppDataManager.Save(s);
                //yield return new WaitForSeconds(3);
                ////////////// Test
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

                foreach (var joint in body.Joints)
                {
                    var pos = joint.Value.Position;
                    joints.Add(new DataContracts.Joint()
                    {
                        JointType = joint.Key.ToString(),
                        X = pos.X,
                        Y = pos.Y,
                        Z = pos.Z
                    });
                }

                var snapshot = new BodySnapshot()
                {
                    Joints = joints.ToArray(),
                    Time = DateTime.Now.ToString("s"),
                };

                _toolbox.AppDataManager.Save(snapshot);
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
                x = handLeft.Position.X,
                y = handLeft.Position.Y,
                z = handLeft.Position.Z
            };
            var handRightVec = new Vector3
            {
                x = handRight.Position.X,
                y = handRight.Position.Y,
                z = handRight.Position.Z,
            };
            
            return Vector3.Distance(handLeftVec, handRightVec);

        }
    }
}
