﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.DataContracts;
using Assets.Toolbox;
using Constants;

public class GameDescribeObjective : GameObjective {

    public string kind = "DescribeObjective";

    public List<Distance2Snapshot> Distance2Snapshots = new List<Distance2Snapshot>();

    public GameDescribeObjective(Toolbox toolbox, Func<bool> isComplete)
        : base(toolbox, isComplete)
    {
        objectiveType = OBJECTIVE.DESCRIBE;
    }

    public override void Start()
    {
        base.Start();
        _toolbox.Distance2Collector.StartCollectDistance2Snapshot(Distance2Snapshots);
    }

    public override void End()
    {
        base.End();
        _toolbox.Distance2Collector.StopCollectDistance2Snapshot();
    }

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
