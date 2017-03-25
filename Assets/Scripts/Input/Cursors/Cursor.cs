using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Windows.Kinect;

public abstract class Cursor
{
    private static Cursor _instance;
    public static Cursor Instance { get { return _instance;  } }

    protected Cursor()
    {
        _instance = this;
    }

    // True if either hand is touching the point
    public abstract bool IsTouching(GameObject gameObject, out RaycastHit hit);

    // True if both hands are touching the same point at the same time
    public virtual bool IsTouchingPoint(GameObject gameObject, out RaycastHit hit)
    {
        return IsTouching(gameObject, out hit);
    }

    // True if both objects are being touched at the same time by different hands
    public virtual bool IsTouchingPoints(GameObject leftObject, out RaycastHit hit)
    {
        return IsTouching(leftObject, out hit);
    }

    public abstract bool IsOutsideOfX(GameObject objectLeft, GameObject objectRight);

    // Gets the position of the mid point between two hands, or just the postion of the
    // single cursor object in the case of one hand or mouse
    public abstract Vector3 MidPosition();

    // The scalar used to convert kinect distance (m) to unity distance
    public abstract Vector3 GetScale();

    // Return the tracking state of the cursor object.
    public virtual TrackingState TrackingState()
    {
        return Windows.Kinect.TrackingState.Tracked;
    }
}
