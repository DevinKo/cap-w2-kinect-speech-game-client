using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.DataContracts;
using Assets.Toolbox;
using Constants;
using System.Runtime.Serialization;

[DataContract]
public class GameDescribeObjective : GameObjective {

    [DataMember]
    public string kind = "DescribeObjective";
    [DataMember]
    public List<Distance2Snapshot> Distance2Snapshots = new List<Distance2Snapshot>();

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
        _toolbox.Distance2Collector.StartCollectDistance2Snapshot(this, Distance2Snapshots);
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

    public override Objectives ToDataContract()
    {
        var objectiveContract = new Objectives();
        objectiveContract.StartTime = _startTime.ToString("s");
        objectiveContract.EndTime = _endTime.ToString("s");
        objectiveContract.AudioSnapshots = AudioSnapshots.ToArray();
        objectiveContract.BodySnapshots = BodySnapshots.ToArray();
        objectiveContract.kind = kind;
        var distList = new List<Distances>();
        foreach (var dist in Distance2Snapshots)
        {
            distList.Add(dist.ToDataContract());
        }
        objectiveContract.Distances = distList.ToArray();
        objectiveContract.ActivationTime = DateTime.Now.ToString("s");

        return objectiveContract;
    }

}
