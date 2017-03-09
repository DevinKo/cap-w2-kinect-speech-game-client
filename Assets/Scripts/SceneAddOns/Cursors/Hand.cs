using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
}