using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
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
        }

        public void Update()
        {
            
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
                    Time = DateTime.Now,
                };

                _toolbox.AppDataManager.Save(snapshot);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
