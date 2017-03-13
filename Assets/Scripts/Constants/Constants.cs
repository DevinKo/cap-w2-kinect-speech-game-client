using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Constants
{
    public enum OBJECTIVE
    {
        LOCATE,
        DESCRIBE,
        NONE
    }

    public enum CursorTypes
    {
        LeftHand,
        RightHand,
        BothHands,
        Mouse
    }

    public enum TaskName
    {
        CheckClueTouched,
        EvaluateZoneCountDown
    }

    public enum GameObjectName
    {
        HandLeft,
        HandRight,
        Clue,
        DescribeHandLeft,
        DescribeHandRight,
    }
}
