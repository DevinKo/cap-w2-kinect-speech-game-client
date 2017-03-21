using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.DataContracts {

	[System.Serializable]
    public class LocateDistanceSnapshot
    {
        public float Distance;
        public DateTime Time;


        public void setSnapshot(float distance)
        {
            Distance = distance;
            Time = System.DateTime.Now;
        }
    }

}
