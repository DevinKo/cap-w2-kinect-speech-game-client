using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using Assets.Toolbox;
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class GameTrial {

    private Toolbox _toolbox;
    private GameObjectiveFactory _gameObjectiveFactory;
    [JsonProperty]
    public System.DateTime StartTime { get; set; }
    [JsonProperty]
    public System.DateTime EndTime { get; set; }

    [JsonProperty]
    public int Difficulty { get; set; }

    [JsonProperty]
    public List<GameObjective> Objectives;

    public GameTrial(Toolbox toolbox)
    {
        _toolbox = toolbox;
        _gameObjectiveFactory = new GameObjectiveFactory(toolbox);
        
        // Create the obectives.
        Objectives = new List<GameObjective>();

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
        var objective = _gameObjectiveFactory.Create(objectiveEnum);
        Objectives.Add(objective);
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
