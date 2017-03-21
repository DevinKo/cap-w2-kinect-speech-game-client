using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.DataContracts;
using Constants;

namespace Assets.Toolbox
{
    public class DistanceCollector : MonoBehaviour {

        private Toolbox _toolbox;

        public struct Client
        {
            public object sender;
            public List<LocateDistanceSnapshot> snapshots;
        }

        private List<Client> _clients = new List<Client>();

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();

            StartCoroutine("CollectDistanceSnapshot");
        }

        public void StartCollectDistanceSnapshot(object sender, List<LocateDistanceSnapshot> DistanceSnapshotList)
        {
            _clients.Add(new Client() { sender = sender, snapshots = DistanceSnapshotList });
        }

        public void StopCollectDistanceSnapshot(object sender)
        {
            _clients.Remove(_clients.Find(c => c.sender == sender));
        }

        public IEnumerator CollectDistanceSnapshot()
        {
            while (true)
            {
                if (_clients.Count == 0) yield return null;

                if (Cursor.Instance.TrackingState() == Windows.Kinect.TrackingState.NotTracked)
                    yield return null;

                var distSnap = new LocateDistanceSnapshot();
                var midPosition = Cursor.Instance.MidPosition();
                var clueObject = BaseSceneManager.Instance.GetObjectWithName(GameObjectName.Clue);
                var cluePosition = clueObject.transform.position;

                var inGameDistance = Vector3.Distance(midPosition, cluePosition);

                var scalar = Cursor.Instance.GetScale();

                var absoluteDistance = new Vector3(inGameDistance.x / scalar.x, inGameDistance.y / scalar.y, 0);

                distSnap.Distance = absoluteDistance.magnitude;

                distSnap.Time = DateTime.Now;

                foreach(var client in _clients)
                {
                    client.snapshots.Add(distSnap);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}
