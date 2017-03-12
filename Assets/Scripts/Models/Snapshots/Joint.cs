using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class Joint
    {
        public string JointType;
        public float X;
        public float Y;
        public float Z;
    }
}
