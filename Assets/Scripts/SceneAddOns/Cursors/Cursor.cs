using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
}
