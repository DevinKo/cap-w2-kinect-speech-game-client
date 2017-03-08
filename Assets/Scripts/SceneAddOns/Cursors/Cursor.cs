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

    public abstract bool IsTouching(string colliderTag, out RaycastHit hit);

    public virtual bool IsTouchingPoint(string colliderTag, out RaycastHit hit)
    {
        return IsTouching(colliderTag, out hit);
    }

    public virtual bool IsTouchingPoints(string colliderTag, out RaycastHit hit)
    {
        return IsTouching(colliderTag, out hit);
    }
}
