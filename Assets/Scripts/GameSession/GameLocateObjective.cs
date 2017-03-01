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

    public GameLocateObjective(Toolbox toolbox, Func<bool> isComplete)
        : base(toolbox, isComplete)
    {
        objectiveType = OBJECTIVE.LOCATE;
        GameEventHub.SpySceneEvents.ZoneActivated += OnZoneActivated;
    }

    public override void Start()
    {
        base.Start();
        _toolbox.DistanceCollector.StartCollectDistanceSnapshot(DistanceSnapshots);
    }

    public override void End()
    {
        base.End();
        _toolbox.DistanceCollector.StopCollectDistanceSnapshot();
    }

    public void OnZoneActivated(object sender, EventArgs e)
    {
        ActivationTime = DateTime.Now;
    }

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
