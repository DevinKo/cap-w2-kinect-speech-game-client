using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Assets.DataContracts;
using UnityEngine;
using System;

namespace Assets.Toolbox
{
    public class VolumeCollector : MonoBehaviour
    {
        private Toolbox _toolbox;

        public struct Client
        {
            public object sender;
            public List<AudioSnapshot> snapshots;
        }

        private List<Client> _clients = new List<Client>();

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();

            StartCoroutine("CollectAudioSnapshots");
        }

        public void Update()
        {

        }

        public void StartCollectAudioSnapshots(object sender, List<AudioSnapshot> audioSnapshotList)
        {
            _clients.Add(new Client() { sender = sender, snapshots = audioSnapshotList });
        }

        public void StopCollectAudioSnapshots(object sender)
        {
            _clients.Remove(_clients.Find(c => c.sender == sender));
        }

        public IEnumerator CollectAudioSnapshots()
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

                foreach(var client in _clients)
                {
                    client.snapshots.Add(audioSnapshot);
                }

                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
