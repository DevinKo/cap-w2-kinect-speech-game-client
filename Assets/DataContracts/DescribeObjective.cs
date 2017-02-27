using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    public class DescribeObjective : Objectives
    {
        public string kind = "DescribeObjective";

        public Distance2Snapshot[] Distance2Snapshot;
    }
}
