using UnityEngine;
using System.Collections.Generic;
using Assets.GameScripts;
using Assets.DataContracts;

namespace Assets.Toolbox
{
    public class AppDataManager : AbstractAppDataManager
    {
        private List<BodySnapshot> _bodySnapshots = new List<BodySnapshot>();
        private List<AudioSnapshot> _audioSnapshots = new List<AudioSnapshot>();
        private Toolbox toolbox;

        private void Start()
        {
            toolbox = gameObject.GetComponent<Toolbox>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var dataClient = toolbox.DataServerProxy;
                dataClient.SendAsFile(new BodySnapshotJsonFile()
                {
                    BodySnapshots = _bodySnapshots.ToArray(),
                    AudioSnapshots = _audioSnapshots.ToArray(),
                });
                _bodySnapshots = new List<BodySnapshot>();
            }
        }

        public override void Save(BodySnapshot data)
        {
            _bodySnapshots.Add(data);

        }

        public override void Save(AudioSnapshot data)
        {
            _audioSnapshots.Add(data);
        }

    }

}