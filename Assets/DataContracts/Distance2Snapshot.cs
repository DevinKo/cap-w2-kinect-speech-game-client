using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.DataContracts
{

    [System.Serializable]
    public class Distance2Snapshot
    {
        public float HandToHandDistance;
        public  float HandsToSpineDistance;
        public string Time;

        public void setSnapshot(float handToHandDist, float handToSpineDist)
        {
            HandToHandDistance = handToHandDist;
            HandsToSpineDistance = handToSpineDist;
            Time = System.DateTime.Now.ToString("s");
        }
    }

}
