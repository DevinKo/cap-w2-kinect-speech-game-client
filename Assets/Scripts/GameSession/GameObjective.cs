using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.DataContracts;
using Assets.Toolbox;
using System;
using Constants;
using System.Runtime.Serialization;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class GameObjective {
    
    protected Toolbox _toolbox;

    public OBJECTIVE objectiveType;
    [JsonProperty]
    public System.DateTime StartTime;
    [JsonProperty]
    public System.DateTime EndTime;

    [JsonProperty]
    public List<BodySnapshot> BodySnapshots = new List<BodySnapshot>();
    [JsonProperty]
    public List<AudioSnapshot> AudioSnapshots = new List<AudioSnapshot>();

    public GameObjective(Toolbox toolBox)
    {
        _toolbox = toolBox;
    }

    public virtual void Start()
    {
        _toolbox.BodySnapshotCollector.StartCollectBodySnapshots(this, BodySnapshots);
        _toolbox.VolumeCollector.StartCollectAudioSnapshots(this, AudioSnapshots);
        StartTime = System.DateTime.Now;
    }

    public virtual void End()
    {
        _toolbox.BodySnapshotCollector.StopCollectBodySnapshots(this);
        _toolbox.VolumeCollector.StopCollectAudioSnapshots(this);
        EndTime = System.DateTime.Now;
    }

}
