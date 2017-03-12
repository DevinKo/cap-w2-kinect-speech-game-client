using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CursorFactory
{
    public static Cursor Create(CursorTypes type)
    {
        switch (type)
        {
            case CursorTypes.BothHands:
                return new TwoHands();
            case CursorTypes.LeftHand:
                return new Hand(CursorTypes.LeftHand);
            case CursorTypes.RightHand:
                return new Hand(CursorTypes.RightHand);
            case CursorTypes.Mouse:
                return new Mouse();
            default:
                return new Mouse();
        }
    }
}
