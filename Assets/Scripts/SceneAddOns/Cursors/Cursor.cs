using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Cursor : MonoBehaviour, IHasCursor
{
    protected GameObject _handRightObject { get; set; }
    protected GameObject _handLeftObject { get; set; }
    public void SetHandLeft(GameObject leftObject)
    {
        _handLeftObject = leftObject;
    }
    public void SetHandRight(GameObject rightObject)
    {
        _handRightObject = rightObject;
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
