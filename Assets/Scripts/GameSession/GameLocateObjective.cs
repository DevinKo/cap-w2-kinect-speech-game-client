using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.DataContracts;
using Assets.Toolbox;
using Constants;
using System.Runtime.Serialization;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class GameLocateObjective : GameObjective {
    
    [JsonProperty]
    public string kind = "LocateObjective";
    [JsonProperty]
    public DateTime ActivationTime;

    [JsonProperty]
    public List<LocateDistanceSnapshot> Distances = new List<LocateDistanceSnapshot>();

    public GameLocateObjective(Toolbox toolbox)
        : base(toolbox)
    {
        objectiveType = OBJECTIVE.LOCATE;

        // subscribe to events.
        _toolbox.EventHub.SpyScene.LoadComplete += OnObjectiveStart;
        _toolbox.EventHub.SpyScene.ZoneActivated += OnZoneActivated;
        _toolbox.EventHub.SpyScene.ZoneComplete += OnObjectiveEnd;
    }

    public override void Start()
    {
        base.Start();
        _toolbox.DistanceCollector.StartCollectDistanceSnapshot(this, Distances);
    }

    public override void End()
    {
        base.End();
        _toolbox.DistanceCollector.StopCollectDistanceSnapshot(this);

        // unsubscribe to events.
        _toolbox.EventHub.SpyScene.LoadComplete -= OnObjectiveStart;
        _toolbox.EventHub.SpyScene.ZoneActivated -= OnZoneActivated;
        _toolbox.EventHub.SpyScene.ZoneComplete -= OnObjectiveEnd;
    }

    #region Event Handlers
    private void OnObjectiveStart(object sender, EventArgs e)
    {
        Start();
    }

    private void OnZoneActivated(object sender, EventArgs e)
    {
        ActivationTime = DateTime.Now;
    }

    private void OnObjectiveEnd(object sender, EventArgs e)
    {
        End();
    }
    #endregion Event Handlers

    
}
