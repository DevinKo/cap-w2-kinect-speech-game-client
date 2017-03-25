using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Mouse : Cursor
{
    public Mouse(): base() { }

    public override bool IsTouching(GameObject gameObject, out RaycastHit hit)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) 
            && hit.collider.gameObject.GetInstanceID() == gameObject.GetInstanceID())
        {
            return true;
        }
        return false;
    }

    public override bool IsTouchingPoint(GameObject gameObject, out RaycastHit hit)
    {
        return IsTouching(gameObject, out hit);
    }

    public override bool IsTouchingPoints(GameObject gameObject, out RaycastHit hit)
    {
        return IsTouching(gameObject, out hit);
    }

    public override bool IsOutsideOfX(GameObject objectLeft, GameObject objectRight)
    {
        // treat mouse as right hand
        return objectRight.transform.position.x < Input.mousePosition.x;
    }

    public override Vector3 MidPosition()
    {
        return Input.mousePosition;
    }

    public override Vector3 GetScale()
    {
        throw new NotImplementedException();
    }
}