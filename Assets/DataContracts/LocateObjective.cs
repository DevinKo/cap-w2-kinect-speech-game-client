using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class LocateObjective :Objectives
    {
        public string kind = "LocateObjective";

        // time at which the pointing zone was activated
        public string ActivationTime;

        // distance from the hands mid-point to the object to identify
        public DistanceSnapshot[] DistanceSnapshots;
    }
}
