using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class BodySnapshot
    {
        public Joint[] Joints;
        public float Time;
    }
}
