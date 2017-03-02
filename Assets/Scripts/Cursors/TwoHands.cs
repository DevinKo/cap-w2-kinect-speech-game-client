using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TwoHands : Cursor
{
    private Hand handLeft = new Hand(CursorTypes.LeftHand);
    private Hand handRight = new Hand(CursorTypes.RightHand);

    public override bool IsTouching(string colliderTag, out RaycastHit hit)
    {
        return handLeft.IsTouching(colliderTag, out hit) && handRight.IsTouching(colliderTag, out hit);
    }
}
