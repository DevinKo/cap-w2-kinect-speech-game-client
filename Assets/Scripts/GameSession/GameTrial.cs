using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using Assets.Toolbox;
using System;

public class GameTrial {

    private Toolbox _toolbox;
    private GameObjectiveFactory _gameObjectiveFactory;

    public System.DateTime StartTime { get; set; }
    public System.DateTime EndTime { get; set; }
    public int Difficulty { get; set; }

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
        _toolbox.EventHub.SpyScene.ZoneComplete += OnTrialEnd;
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
        StartTime = DateTime.Now;
    }

    public void End()
    {
        EndTime = DateTime.Now;
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
