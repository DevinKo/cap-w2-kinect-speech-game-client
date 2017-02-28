using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class Session
    {
        public string email;
        public string password;
        public string StartTime;
        public string EndTime;

		public CalibrationData CalibrationData;

        public Trial[] Trials;
    }
}
