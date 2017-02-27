using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class Objectives
    {
        public string StartTime;
        public string EndTime;
        public BodySnapshot[] BodySnapshots;
        public AudioSnapshot[] AudioSnapshots;
    }
}
