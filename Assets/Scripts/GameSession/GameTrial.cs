using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTrial {

    public System.DateTime StartTime { get; set; }
    public System.DateTime EndTime { get; set; }
    public int Difficulty { get; set; }

    public GameObjective[] Objectives = new GameObjective[] { new GameLocateObjective(), new GameDescribeObjective()};

    public GameTrial()
    {
        StartTime = System.DateTime.Now;
    }


}
