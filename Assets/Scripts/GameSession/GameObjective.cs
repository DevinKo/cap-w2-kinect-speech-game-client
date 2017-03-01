using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.DataContracts;
using Assets.Toolbox;
using System;
using Constants;

public class GameObjective {
    
    // A method which defines the completion requirements.
    private Func<bool> _isComplete;
    protected Toolbox _toolbox;

    public OBJECTIVE objectiveType;
    public System.DateTime StartTime;
    public System.DateTime EndTime;
    public List<BodySnapshot> BodySnapshots = new List<BodySnapshot>();
    public List<AudioSnapshot> AudioSnapshots = new List<AudioSnapshot>();
    public virtual Objectives ToDataContract() { return null; }

    public bool IsComplete
    {
        get
        {
            return _isComplete();
        }
    }

    public GameObjective(Toolbox toolBox, Func<bool> isComplete)
    {
        _isComplete = isComplete;
        _toolbox = toolBox;
    }

    public virtual void Start()
    {
        _toolbox.BodySnapshotCollector.StartCollectBodySnapshots(BodySnapshots);
        _toolbox.VolumeCollector.StartCollectAudioSnapshots(AudioSnapshots);
        StartTime = System.DateTime.Now;
    }

    public virtual void End()
    {
        _toolbox.BodySnapshotCollector.StopCollectBodySnapshots();
        _toolbox.VolumeCollector.StopCollectAudioSnapshots();
        EndTime = System.DateTime.Now;
    }

}
