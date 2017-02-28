using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GameCalibrationData {

    public DateTime StartTime;
    public DateTime EndTime;
    public float Radius;
    public float MaxReachLeft;
    public float MaxReachRight;
    public float AudioThreshold;
    public float PointingZoneTimerSec;

    public GameCalibrationData()
    {
        StartTime = DateTime.Now;
    }

}
