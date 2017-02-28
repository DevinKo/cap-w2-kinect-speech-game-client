using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Assets.DataContracts
{
	[Serializable]
	public class Calibration
	{
		public string StartTime;
		public string EndTime;
		public float Radius;
		public MaxReach MaxReachLeft;
		public MaxReach MaxReachRight;
		public float AudioThreshold;
		public float PointingZoneTimerSec;
	}
}