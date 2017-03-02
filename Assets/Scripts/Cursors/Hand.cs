using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Hand : Cursor
{
    private KinectUICursor _uiHand;

    public Hand(CursorTypes type)
    {
        var handTag = type == CursorTypes.LeftHand ? "UIHandLeft" : "UIHandRight";
        var hand = GameObject.FindGameObjectWithTag(handTag);
        _uiHand = hand.GetComponent<KinectUICursor>();
    }

    public override bool IsTouching(string colliderTag, out RaycastHit hit)
    {
        var ray = Camera.main.ScreenPointToRay(_uiHand.CurrentPosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == colliderTag)
            {
                return true;
            }
        }
        return false;
    }
}