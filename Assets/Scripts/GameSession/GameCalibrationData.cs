using Assets.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

[DataContract]
public class GameCalibrationData {

    private Toolbox _toolbox;

    public DateTime _startTime;
    public DateTime _endTime;


    [DataMember]
    public string StartTime { get; set; }
    [DataMember]
    public string EndTime { get; set; }

    [OnSerializing]
    void OnSerializing(StreamingContext ctx)
    {
        StartTime = _startTime.ToString("s");
        EndTime = _endTime.ToString("s");
    }

    [DataMember]
    public float Radius;
    [DataMember]
    public float MaxReachLeft;
    [DataMember]
    public float MaxReachRight;
    [DataMember]
    public float AudioThreshold;
    [DataMember]
    public float PointingZoneTimerSec;

    public GameCalibrationData(Toolbox toolbox)
    {
        _toolbox = toolbox;
        _startTime = DateTime.Now;

        _toolbox.EventHub.SpyScene.LoadComplete += OnCalibrationComplete;
    }

    #region Event Handlers
    private void OnCalibrationComplete(object sender, EventArgs e)
    {
        _endTime = DateTime.Now;
    }
    #endregion Event Handlers
}
