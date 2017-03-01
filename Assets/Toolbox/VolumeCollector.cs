using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Assets.Scripts;
using Assets.DataContracts;
using UnityEngine;
using System;

namespace Assets.Toolbox
{
    public class VolumeCollector : MonoBehaviour
    {
        private Toolbox _toolbox;

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();
        }

        public void Update()
        {

        }

        public IEnumerator CollectAudioSnapshots(List<AudioSnapshot> audioSnapshotList)
        {
            while (true)
            {
                if (!_toolbox.VolumeSourceManager)
                {
                    yield return null;
                    continue;
                }
                var audioSnapshot = new AudioSnapshot()
                {
                    Intensity = _toolbox.VolumeSourceManager.Decibel,
                    Time = DateTime.Now.ToString("s"),
                };

                audioSnapshotList.Add(audioSnapshot);

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
