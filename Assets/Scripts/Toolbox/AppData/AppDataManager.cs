using UnityEngine;
using System.Collections.Generic;
using Assets.DataContracts;
using Windows.Kinect;
using System;

namespace Assets.Toolbox
{
    public class AppDataManager : AbstractAppDataManager
    {
        private List<BodySnapshot> _bodySnapshots = new List<BodySnapshot>();
        private List<AudioSnapshot> _audioSnapshots = new List<AudioSnapshot>();
        private Dictionary<JointType, MaxReach> _maxReach = new Dictionary<JointType, MaxReach>();

        private GameSettings _gameSettings = new GameSettings();

        private GameSession _session;

        private Toolbox toolbox;

        private void Start()
        {
            toolbox = gameObject.GetComponent<Toolbox>();
        }

        public void Update()
        {
            
        }
        
        public override void Save(GameSession session)
        {
            _session = session;
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

        public override GameSettings GetGameSettings()
        {
            return _gameSettings;
        }

        public override void Save(GameSettings settings)
        {
            _gameSettings = settings;
        }
    }

}