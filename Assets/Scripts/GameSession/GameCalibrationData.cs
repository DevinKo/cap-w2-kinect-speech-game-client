using Assets.Toolbox;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

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
        StartTime = DateTime.Now;

        _toolbox.EventHub.SpyScene.LoadComplete += OnCalibrationComplete;
    }

    #region Event Handlers
    private void OnCalibrationComplete(object sender, EventArgs e)
    {
        EndTime = DateTime.Now;
    }
    #endregion Event Handlers
}
