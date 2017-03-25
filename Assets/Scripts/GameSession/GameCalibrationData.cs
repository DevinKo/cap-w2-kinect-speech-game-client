using Assets.Toolbox;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;

[JsonObject(MemberSerialization.OptIn)]
public class GameCalibrationData {

    private Toolbox _toolbox;
    [JsonProperty]
    public DateTime StartTime;
    [JsonProperty]
    public DateTime EndTime;

    [JsonProperty]
    public float Radius;
    [JsonProperty]
    public float MaxReachLeft;
    [JsonProperty]
    public float MaxReachRight;
    [JsonProperty]
    public float AudioThreshold;
    [JsonProperty]
    public float PointingZoneTimerSec;

    public GameCalibrationData(Toolbox toolbox)
    {
        _toolbox = toolbox;

        _toolbox.EventHub.CalibrationScene.LoadComplete += OnCalibrationStart;
        _toolbox.EventHub.CalibrationScene.SceneEnd += OnCalibrationComplete;
        _toolbox.EventHub.CalibrationScene.RadiusCaptured += OnRadiusCaptured;
        _toolbox.EventHub.CalibrationScene.PointerZoneDurationCaptured += OnPointingZoneTimerDurationCaptured;
        _toolbox.EventHub.CalibrationScene.AudioThresholdCaptured += OnAudioThresholdCaptured;
        _toolbox.EventHub.CalibrationScene.MaxReachCaptured += OnMaxReachCaptured;
    }

    #region Event Handlers
    private void OnCalibrationComplete(object sender, EventArgs e)
    {
        EndTime = DateTime.Now;
    }

    private void OnCalibrationStart(object sender, EventArgs e)
    {
        StartTime = DateTime.Now;
    }

    private void OnAudioThresholdCaptured(object sender, EventArgs e, float audioThreshold)
    {
        AudioThreshold = audioThreshold;
    }

    private void OnRadiusCaptured(object sender, EventArgs e, float radius)
    {
        Radius = radius;
    }

    private void OnPointingZoneTimerDurationCaptured(object sender, EventArgs e, float duration)
    {
        PointingZoneTimerSec = duration;
    }

    private void OnMaxReachCaptured(object sender, EventArgs e, Vector3 left, Vector3 right)
    {
        MaxReachLeft = left.magnitude;
        MaxReachRight = right.magnitude;
    }
    #endregion Event Handlers
}
