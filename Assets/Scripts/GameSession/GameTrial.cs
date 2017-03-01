using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;

public class GameTrial {

    public System.DateTime StartTime { get; set; }
    public System.DateTime EndTime { get; set; }
    public int Difficulty { get; set; }
    public OBJECTIVE CurrentObjective;

    public Dictionary<OBJECTIVE, GameObjective> Objectives = new Dictionary<OBJECTIVE, GameObjective>()
    {
        {OBJECTIVE.LOCATE, new GameLocateObjective() },
        {OBJECTIVE.DESCRIBE, new GameDescribeObjective() }
    };

    public GameTrial()
    {
        StartTime = System.DateTime.Now;
        CurrentObjective = OBJECTIVE.NONE;
    }

    public void InitNewObjective(OBJECTIVE currentObjective)
    {
        CurrentObjective = currentObjective;
        Objectives[currentObjective].StartTime = System.DateTime.Now;
    }

    public GameObjective GetCurrentObjective()
    {
        return Objectives[CurrentObjective];
    }

    public void SetCurrentObjectiveIndex(OBJECTIVE currentObjective)
    {
        CurrentObjective = currentObjective;
    }
}
