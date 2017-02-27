using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataContracts
{
    [Serializable]
    public class Session
    {
        public string Email;
        public string Password;
        public string StartTime;
        public string EndTime;

        public Trial[] Trials;
    }
}
