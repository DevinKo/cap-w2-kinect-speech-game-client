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
    public override bool IsTouching(GameObject gameObject, out RaycastHit hit)
    {
        return handLeft.IsTouching(gameObject, out hit) || handRight.IsTouchingPoint(gameObject, out hit);
    }

    // Returns true if both hands a touching same point
    public override bool IsTouchingPoint(GameObject gameObject, out RaycastHit hit)
    {
        return handLeft.IsTouching(gameObject, out hit) && handRight.IsTouchingPoint(gameObject, out hit);
    }

    // Returns true if both hands a touching different points
    // The points must have tag = colliderTag + "Left" and colliderTag + "Right"
    public override bool IsTouchingPoints(GameObject gameObject, out RaycastHit hit)
    {
        return handLeft.IsTouching(gameObject, out hit) && handRight.IsTouchingPoint(gameObject, out hit);
    }
}
