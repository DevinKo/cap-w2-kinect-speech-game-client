using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class BodySnapshotsMessage
    {
        public BodySnapshot[] Snapshots;
    }
}
