using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.DataContracts;

public class GameDescribeObjective : GameObjective {

    public string kind = "DescribeObjective";

    public List<Distance2Snapshot> Distance2Snapshots = new List<Distance2Snapshot>();

    public override Objectives ToDataContract()
    {
        var objectiveContract = new Objectives();
        objectiveContract.StartTime = StartTime.ToString("s");
        objectiveContract.EndTime = EndTime.ToString("s");
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
