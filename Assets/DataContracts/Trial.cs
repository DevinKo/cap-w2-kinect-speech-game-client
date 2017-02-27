using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class Trial
    {
        public string StartTime;
        public string EndTime;
        public int Difficulty;
        public Objectives[] Objectives;
    }
}
