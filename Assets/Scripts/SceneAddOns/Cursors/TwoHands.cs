using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TwoHands : Cursor
{
    public TwoHands() : base() { }
    private Hand handLeft = new Hand(CursorTypes.LeftHand);
    private Hand handRight = new Hand(CursorTypes.RightHand);

    // returns true if either hands is touching the same point
    public override bool IsTouching(string colliderTag, out RaycastHit hit)
    {
        return handLeft.IsTouching(colliderTag, out hit) || handRight.IsTouchingPoint(colliderTag, out hit);
    }

    // Returns true if both hands a touching same point
    public override bool IsTouchingPoint(string colliderTag, out RaycastHit hit)
    {
        return handLeft.IsTouching(colliderTag, out hit) && handRight.IsTouchingPoint(colliderTag, out hit);
    }

    // Returns true if both hands a touching different points
    // The points must have tag = colliderTag + "Left" and colliderTag + "Right"
    public override bool IsTouchingPoints(string colliderTag, out RaycastHit hit)
    {
        return handLeft.IsTouching(colliderTag + "Left", out hit) && handRight.IsTouchingPoint(colliderTag + "Right", out hit);
    }
}
