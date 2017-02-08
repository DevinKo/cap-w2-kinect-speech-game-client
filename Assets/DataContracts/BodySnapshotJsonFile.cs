﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class BodySnapshotJsonFile
    {
        public BodySnapshot[] Snapshots;
        public AudioSnapshot[] AudioSnapshots;
    }
}
