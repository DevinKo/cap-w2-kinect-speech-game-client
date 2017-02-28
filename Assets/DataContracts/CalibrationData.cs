using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Assets.DataContracts
{
	[Serializable]
	public class CalibrationData
	{


		public string StartTime;
		public string EndTime;
		public float Radius;
		public float MaxReachLeft;
		public float MaxReachRight;
		public float AudioThreshold;
		public float PointingZoneTimerSec;

        public CalibrationData()
        {
            StartTime = DateTime.Now.ToString("s");
            EndTime = DateTime.Now.ToString("s");
        }
	}
}