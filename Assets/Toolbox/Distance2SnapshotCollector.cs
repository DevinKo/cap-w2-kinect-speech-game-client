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

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();
        }

        public void StartCollectDistance2Snapshot(List<Distance2Snapshot> distance2SnapshotList)
        {
            StartCoroutine("CollectDistance2Snapshot", distance2SnapshotList);
        }

        public void StopCollectDistance2Snapshot()
        {
            StopCoroutine("CollectDistance2Snapshot");
        }

        public IEnumerator CollectDistance2Snapshot(List<Distance2Snapshot> distance2SnapshotList)
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

                distance2SnapshotList.Add(dist2Snapshot);

                yield return new WaitForSeconds(0.1f);
            }
        }


    }
}
