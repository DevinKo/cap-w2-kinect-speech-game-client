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

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();
        }

        public void StartCollectDistanceSnapshot(List<LocateDistanceSnapshot> DistanceSnapshotList)
        {
            StartCoroutine("CollectDistanceSnapshot", DistanceSnapshotList);
        }

        public void StopCollectDistanceSnapshot()
        {
            StopCoroutine("CollectDistanceSnapshot");
        }

        public IEnumerator CollectDistanceSnapshot(List<LocateDistanceSnapshot> DistanceSnapshotList)
        {
            while (true)
            {
                var distSnap = new LocateDistanceSnapshot();

                distSnap.setSnapshot(0f);

                DistanceSnapshotList.Add(distSnap);

                yield return new WaitForSeconds(0.1f);
            }
        }

    }
}
