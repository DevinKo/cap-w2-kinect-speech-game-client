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

            toolbox.EventHub.CalibrationScene.RadiusCaptured += OnRadiusCaptured;
            toolbox.EventHub.CalibrationScene.PointerZoneDurationCaptured += OnPointerZoneDurationCaptured;
            toolbox.EventHub.CalibrationScene.AudioThresholdCaptured += OnAudioThresholdCaptured;
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
            /*
            _gameSettings.PointingZoneDuration = PlayerPrefs.GetFloat("PointerZoneDuration", 5);
            _gameSettings.PointingZoneRadius = PlayerPrefs.GetFloat("PointerZoneRadius");
            _gameSettings.AudioThreshold = PlayerPrefs.GetFloat("AudioThreshold");
            */
            
            _gameSettings.PointingZoneDuration = _gameSettings.PointingZoneDuration == 0 ? 5 : _gameSettings.PointingZoneDuration;
            _gameSettings.AudioThreshold = _gameSettings.AudioThreshold == 0 ? -80f : _gameSettings.AudioThreshold;

            return _gameSettings;
        }

        public override void Save(GameSettings settings)
        {
            _gameSettings = settings;

            PlayerPrefs.GetFloat("AudioThreshold", settings.AudioThreshold);
            PlayerPrefs.SetFloat("PointerZoneDuration", settings.PointingZoneDuration);
            PlayerPrefs.SetFloat("PointerZoneRadius", settings.PointingZoneRadius);
        }

        #region EventHandlers
        private void OnRadiusCaptured(object sender, EventArgs e, float radius)
        {
            var settings = GetGameSettings();
            settings.PointingZoneRadius = radius;
            Save(settings);
        }

        private void OnPointerZoneDurationCaptured(object sender, EventArgs e, float duration)
        {
            var settings = GetGameSettings();
            settings.PointingZoneDuration = duration;
            Save(settings);
        }

        private void OnAudioThresholdCaptured(object sender, EventArgs e, float threshold)
        {
            var settings = GetGameSettings();
            settings.AudioThreshold = threshold;
            Save(settings);
        } 
        #endregion EventHandlers
    }

}