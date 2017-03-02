using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Mouse : Cursor
{
    public override bool IsTouching(string colliderTag, out RaycastHit hit)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && hit.collider.tag == colliderTag)
        {
            return true;
        }
    }
}