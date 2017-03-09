using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.DataContracts;
using Assets.Toolbox;
using Constants;

public class GameLocateObjective : GameObjective {
    
    public string kind = "LocateObjective";
    public DateTime ActivationTime;

    public List<LocateDistanceSnapshot> DistanceSnapshots = new List<LocateDistanceSnapshot>();

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
        _toolbox.DistanceCollector.StartCollectDistanceSnapshot(this, DistanceSnapshots);
    }

    public override void End()
    {
        base.End();
        _toolbox.DistanceCollector.StopCollectDistanceSnapshot(this);
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

    public override Objectives ToDataContract()
    {
        var objectiveContract = new Assets.DataContracts.Objectives();
        objectiveContract.StartTime = StartTime.ToString("s");
        objectiveContract.EndTime = EndTime.ToString("s");
        objectiveContract.AudioSnapshots = AudioSnapshots.ToArray();
        objectiveContract.BodySnapshots = BodySnapshots.ToArray();
        objectiveContract.kind = kind;
        var distList = new List<Distances>();
        foreach (var dist in DistanceSnapshots)
        {
            distList.Add(dist.ToDataContract());
        }
        objectiveContract.Distances = distList.ToArray();
        objectiveContract.ActivationTime = ActivationTime.ToString("s");
        return objectiveContract;
    }
    
}
