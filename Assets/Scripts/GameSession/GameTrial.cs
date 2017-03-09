using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using Assets.Toolbox;
using System;
using System.Runtime.Serialization;

public class GameTrial {

    private Toolbox _toolbox;
    private GameObjectiveFactory _gameObjectiveFactory;

    public System.DateTime _startTime { get; set; }
    public System.DateTime _endTime { get; set; }

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
    public int Difficulty { get; set; }

    [DataMember]
    public Dictionary<OBJECTIVE, GameObjective> Objectives;

    public GameTrial(Toolbox toolbox)
    {
        _toolbox = toolbox;
        _gameObjectiveFactory = new GameObjectiveFactory(toolbox);
        
        // Create the obectives.
        Objectives = new Dictionary<OBJECTIVE, GameObjective>();

        // configure locate objective
        AddObjective(OBJECTIVE.LOCATE);
        // configure describe objective
        AddObjective(OBJECTIVE.DESCRIBE);

        // Subscribe to events
        _toolbox.EventHub.SpyScene.LoadComplete += OnTrialStart;
        _toolbox.EventHub.SpyScene.DescribeComplete += OnTrialEnd;
    }

    public GameObjective AddObjective(OBJECTIVE objectiveEnum)
    {
        if (Objectives.ContainsKey(objectiveEnum)) throw new ArgumentException("Objective already added");

        var objective = _gameObjectiveFactory.Create(objectiveEnum);
        Objectives.Add(objectiveEnum, objective);
        return objective;
    }

    public void Start()
    {
        _startTime = DateTime.Now;
    }

    public void End()
    {
        _endTime = DateTime.Now;
    }

    #region Event handlers
    private void OnTrialStart(object sender, EventArgs e)
    {
        Start();
    }

    private void OnTrialEnd(object sender, EventArgs e)
    {
        End();
    }
    #endregion
}
