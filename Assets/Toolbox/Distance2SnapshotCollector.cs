using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.DataContracts;

namespace Assets.Toolbox
{
    public class Distance2SnapshotCollector : MonoBehaviour {

        private Toolbox _toolbox;

        public struct Client
        {
            public object sender;
            public List<Distance2Snapshot> snapshots;
        }

        private List<Client> _clients = new List<Client>();

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();

            StartCoroutine("CollectDistance2Snapshot");
        }

        public void StartCollectDistance2Snapshot(object sender, List<Distance2Snapshot> distance2SnapshotList)
        {
            _clients.Add(new Client() { sender = sender, snapshots = distance2SnapshotList });
        }

        public void StopCollectDistance2Snapshot(object sender)
        {
            _clients.Remove(_clients.Find(c => c.sender == sender));
        }

        public IEnumerator CollectDistance2Snapshot()
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

                var rightHand = body.Joints[Windows.Kinect.JointType.HandRight];
                var leftHand = body.Joints[Windows.Kinect.JointType.HandLeft];
                var upperSpine = body.Joints[Windows.Kinect.JointType.SpineShoulder];

                Vector3 rightHandPos = new Vector3(rightHand.Position.X, rightHand.Position.Y, rightHand.Position.Z);
                Vector3 leftHandPos = new Vector3(leftHand.Position.X, leftHand.Position.Y, leftHand.Position.Z);
                Vector3 upperSpinePos = new Vector3(upperSpine.Position.X, upperSpine.Position.Y, upperSpine.Position.Z);

                Vector3 handsMidpoint = Vector3.Lerp(rightHandPos, leftHandPos, 0.5f);

                Distance2Snapshot dist2Snapshot = new Distance2Snapshot();
                dist2Snapshot.setSnapshot(Vector3.Distance(rightHandPos, leftHandPos), Vector3.Distance(handsMidpoint, upperSpinePos));

                foreach(var client in _clients)
                {
                    client.snapshots.Add(dist2Snapshot);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }


    }
}
