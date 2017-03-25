using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Windows.Kinect;

public class Hand : Cursor
{
    private CursorTypes _type;
    private GameObject _hand;

    public Hand(CursorTypes type): base()
    {
        _type = type;
        GetHandObject(type);
    }

    public override bool IsTouching(GameObject gameObject, out RaycastHit hit)
    {
        var hand = GetHandObject(_type);
        if (hand == null)
        {
            hit = new RaycastHit();
            return false;
        }

        var ray = Camera.main.ScreenPointToRay(hand.transform.position);

        if (Physics.Raycast(ray, out hit)
            && hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
        {
            return true;
        }
        return false;
    }
    
    public GameObject GetHandObject(CursorTypes type)
    {
        if (_hand != null) return _hand;

        var name = type == CursorTypes.LeftHand ?
            GameObjectName.HandLeft : GameObjectName.HandRight;
        _hand = BaseSceneManager.Instance.GetObjectWithName(name);
        return _hand;
    }

    public override bool IsOutsideOfX(GameObject objectLeft, GameObject objectRight)
    {
        if (_type == CursorTypes.LeftHand)
        {
            return IsLeftOfX(objectLeft);
        }
        else
        {
            return IsRightOfX(objectRight);
        }
    }

    public bool IsLeftOfX(GameObject gameObject)
    {
        return gameObject.transform.position.x > GetHandObject(_type).transform.position.x;
    }

    public bool IsRightOfX(GameObject gameObject)
    {
        return gameObject.transform.position.x < GetHandObject(_type).transform.position.x;
    }

    public override Vector3 MidPosition()
    {
        return GetHandObject(_type).transform.position;
    }

    public override Vector3 GetScale()
    {
        var hand = GetHandObject(_type);
        if (hand == null) return new Vector3();

        var uiHand = hand.GetComponent<KinectUICursor>();
        if (uiHand == null) return new Vector3();

        return uiHand._reachScalar;
    }

    public override TrackingState TrackingState()
    {
        var handObject = GetHandObject(_type);
        if (handObject == null) return Windows.Kinect.TrackingState.NotTracked;

        var hand = handObject.GetComponent<KinectUICursor>();
        if (hand == null) return Windows.Kinect.TrackingState.NotTracked;

        return hand.TrackingState;
    }
}