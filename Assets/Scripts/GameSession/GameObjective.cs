using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.DataContracts;
using Assets.Toolbox;
using System;
using Constants;

public class GameObjective {
    
    protected Toolbox _toolbox;

    public OBJECTIVE objectiveType;
    public System.DateTime StartTime;
    public System.DateTime EndTime;
    public List<BodySnapshot> BodySnapshots = new List<BodySnapshot>();
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
        StartTime = System.DateTime.Now;
    }

    public virtual void End()
    {
        _toolbox.BodySnapshotCollector.StopCollectBodySnapshots(this);
        _toolbox.VolumeCollector.StopCollectAudioSnapshots(this);
        EndTime = System.DateTime.Now;
    }

}
