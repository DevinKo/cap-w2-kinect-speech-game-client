using UnityEngine;
using System.Collections.Generic;
using Assets.GameScripts;
using Assets.DataContracts;
using Windows.Kinect;

namespace Assets.Toolbox
{
    public class AppDataManager : AbstractAppDataManager
    {
        private List<BodySnapshot> _bodySnapshots = new List<BodySnapshot>();
        private List<AudioSnapshot> _audioSnapshots = new List<AudioSnapshot>();
        private Dictionary<JointType, MaxReach> _maxReach = new Dictionary<JointType, MaxReach>();

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
                _audioSnapshots = new List<AudioSnapshot>();
            }
        }

        public override void Save(MaxReach maxReach, JointType joint)
        {
            _maxReach.Add(joint, maxReach);
        }

        public override MaxReach GetMaxReach(JointType joint)
        {
            if (!_maxReach.ContainsKey(joint)) return null;
            return _maxReach[joint];
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