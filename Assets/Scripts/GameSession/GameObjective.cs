using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.DataContracts;
using Assets.Toolbox;
using System;
using Constants;
using System.Runtime.Serialization;

[DataContract]
public class GameObjective {
    
    protected Toolbox _toolbox;

    public OBJECTIVE objectiveType;
    public System.DateTime _startTime;
    public System.DateTime _endTime;


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
    public List<BodySnapshot> BodySnapshots = new List<BodySnapshot>();
    [DataMember]
    public List<AudioSnapshot> AudioSnapshots = new List<AudioSnapshot>();

    public virtual Objectives ToDataContract() { return null; }

    public GameObjective(Toolbox toolBox)
    {
        _toolbox = toolBox;
    }

    public virtual void Start()
    {
        _toolbox.BodySnapshotCollector.StartCollectBodySnapshots(this, BodySnapshots);
        _toolbox.VolumeCollector.StartCollectAudioSnapshots(this, AudioSnapshots);
        _startTime = System.DateTime.Now;
    }

    public virtual void End()
    {
        _toolbox.BodySnapshotCollector.StopCollectBodySnapshots(this);
        _toolbox.VolumeCollector.StopCollectAudioSnapshots(this);
        _endTime = System.DateTime.Now;
    }

}
