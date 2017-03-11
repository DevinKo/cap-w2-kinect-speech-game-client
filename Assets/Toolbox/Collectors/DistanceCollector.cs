using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Assets.DataContracts;

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
                var distSnap = new LocateDistanceSnapshot();

                distSnap.setSnapshot(0f);

                foreach(var client in _clients)
                {
                    client.snapshots.Add(distSnap);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}
