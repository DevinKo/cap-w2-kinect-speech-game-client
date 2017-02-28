using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.DataContracts {

	[System.Serializable]
    public class DistanceSnapshot
    {
        public float Distance;
        public string Time;

        public Distances ToDataContract()
        {
            var dist = new Distances()
            {
                Distance = Distance,
                Time = Time,
            };
            return dist;
        }

        public void setSnapshot(float distance)
        {
            Distance = distance;
            Time = System.DateTime.Now.ToString("s");
        }
    }

}
