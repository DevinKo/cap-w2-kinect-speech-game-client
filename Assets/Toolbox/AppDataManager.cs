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

        private GameSession _session = new GameSession("p.mcpatientface@email.com", "mFjDhCdzCw");

        private Toolbox toolbox;

        private void Start()
        {
            toolbox = gameObject.GetComponent<Toolbox>();
        }

        public void Update()
        {
            
        }

        public override GameSession GetSession()
        {
            return _session;
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