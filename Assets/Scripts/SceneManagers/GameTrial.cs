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
    public OBJECTIVE CurrentObjective;
    
    // A list which defines the order of objectives
    private List<OBJECTIVE> _objectiveSequence = new List<OBJECTIVE>()
    {
        OBJECTIVE.DESCRIBE,
        OBJECTIVE.LOCATE,
        OBJECTIVE.NONE
    };

    public Dictionary<OBJECTIVE, GameObjective> Objectives;

    public GameTrial(Toolbox toolbox)
    {
        _toolbox = toolbox;
        _gameObjectiveFactory = new GameObjectiveFactory(toolbox);
        StartTime = System.DateTime.Now;
        CurrentObjective = OBJECTIVE.NONE;

        // Create the obectives.
        Objectives = new Dictionary<OBJECTIVE, GameObjective>();
    }

    public GameObjective AddObjective(GameObjective objective)
    {
        if (Objectives.ContainsKey(objective.objectiveType)) throw new ArgumentException("Objective already added");
        
        _objectiveSequence.Add(objective.objectiveType);
        Objectives.Add(objective.objectiveType, objective);
        return objective;
    }

    public void InitNewObjective(OBJECTIVE currentObjective)
    {
        CurrentObjective = currentObjective;
    }

    public GameObjective GetCurrentObjective()
    {
        return Objectives[CurrentObjective];
    }

    public void SetCurrentObjective(OBJECTIVE currentObjective)
    {
        CurrentObjective = currentObjective;
    }

    public GameObjective GetNextObjective()
    {
        var nextObjectiveIdx = 1 + _objectiveSequence.IndexOf(CurrentObjective);
        var nextObjectiveEnum = _objectiveSequence[nextObjectiveIdx];
        return Objectives[nextObjectiveEnum];
    }
}
