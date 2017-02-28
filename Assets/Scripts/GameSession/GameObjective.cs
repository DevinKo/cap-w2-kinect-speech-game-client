using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.DataContracts;

public class GameObjective {

    public System.DateTime StartTime;
    public System.DateTime EndTime;
    public List<BodySnapshot> BodySnapshots = new List<BodySnapshot>();
    public List<AudioSnapshot> AudioSnapshots = new List<AudioSnapshot>();
    public virtual Objectives ToDataContract() { return null; }

}
