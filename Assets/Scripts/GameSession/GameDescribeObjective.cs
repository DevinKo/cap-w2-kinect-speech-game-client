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
public class GameDescribeObjective : GameObjective {

    [JsonProperty]
    public string kind = "DescribeObjective";
    [JsonProperty]
    public List<Distance2Snapshot> Distances = new List<Distance2Snapshot>();

    public GameDescribeObjective(Toolbox toolbox)
        : base(toolbox)
    {
        objectiveType = OBJECTIVE.DESCRIBE;

        //Subscribe to events
        _toolbox.EventHub.SpyScene.ZoneComplete += OnObjectiveStart;
        _toolbox.EventHub.SpyScene.DescribeComplete += OnObjectiveEnd;
    }

    public override void Start()
    {
        base.Start();
        _toolbox.Distance2Collector.StartCollectDistance2Snapshot(this, Distances);
    }

    public override void End()
    {
        base.End();
        _toolbox.Distance2Collector.StopCollectDistance2Snapshot(this);
    }

    #region Event Handlers
    private void OnObjectiveStart(object sender, EventArgs e)
    {
        Start();
    }

    private void OnObjectiveEnd(object sender,EventArgs e)
    {
        End();
    }
    #endregion Event Handlers


}
