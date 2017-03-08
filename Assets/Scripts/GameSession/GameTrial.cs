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
        CurrentObjective = OBJECTIVE.NONE;
        
        // Create the obectives.
        Objectives = new Dictionary<OBJECTIVE, GameObjective>();

        // configure locate objective
        AddObjective(OBJECTIVE.LOCATE);
        // configure describe objective
        AddObjective(OBJECTIVE.DESCRIBE);

    }

    public GameObjective AddObjective(OBJECTIVE objectiveEnum)
    {
        if (Objectives.ContainsKey(objectiveEnum)) throw new ArgumentException("Objective already added");
        
        _objectiveSequence.Add(objectiveEnum);
        var objective = _gameObjectiveFactory.Create(objectiveEnum);
        Objectives.Add(objectiveEnum, objective);
        return objective;
    }

    public GameObjective GetCurrentObjective()
    {
        return Objectives[CurrentObjective];
    }

    public void SetCurrentObjective(OBJECTIVE currentObjective)
    {
        CurrentObjective = currentObjective;
    }

    public GameObjective Start()
    {
        StartTime = DateTime.Now;
        CurrentObjective = _objectiveSequence[0];
        var firstObjective = Objectives[CurrentObjective];
        firstObjective.Start();
        return firstObjective;
    }

    public GameObjective GetNextObjective()
    {
        var nextObjectiveIdx = 1 + _objectiveSequence.IndexOf(CurrentObjective);
        var nextObjectiveEnum = _objectiveSequence[nextObjectiveIdx];
        CurrentObjective = nextObjectiveEnum;
        return Objectives[nextObjectiveEnum];
    }

    public GameObjective StartNextObjective()
    {
        Objectives[CurrentObjective].End();
        var nextObjective = GetNextObjective();
        nextObjective.Start();
        return nextObjective;
    }
}
