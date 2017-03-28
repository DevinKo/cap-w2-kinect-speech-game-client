using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class ClueBoxGenerator : MonoBehaviour
{
    public GameObject backgroundObject;
    public Image clueImage;

    private int _firstUpdate;

    public void Start()
    {
        gameObject.transform.rotation = Camera.main.transform.rotation;
        var mesh = backgroundObject.GetComponent<Renderer>();
        mesh.material.color = Color.white;
        GameObject pointingObjectOriginal = GameObject.FindGameObjectsWithTag("pointing_object").First();
        
        // Copy clue object
        var pointingObject = Instantiate(pointingObjectOriginal, gameObject.transform);

        // deactivate script
        pointingObject.GetComponent<PointingObject>().enabled = false;

        // position clue object
        var cameraPosition = gameObject.transform.position;
        var cameraDirection = gameObject.transform.rotation;
        pointingObject.transform.position = cameraPosition + gameObject.transform.forward * 5;

        var clueBounds = pointingObject.GetComponent<MeshFilter>().mesh.bounds;
        var camera = gameObject.GetComponent<Camera>();
        var distance = 5;
        var v3ViewPort = new Vector3();

        v3ViewPort.Set(0, 0, distance);
        var v3BottomLeft = camera.ViewportToWorldPoint(v3ViewPort);
        v3ViewPort.Set(1, 1, distance);
        var v3TopRight = camera.ViewportToWorldPoint(v3ViewPort);

        var cluePosition = pointingObject.transform.position;
        var max = clueBounds.extents.x;
        var scale = 0f;
        if (v3BottomLeft.x > v3TopRight.x)
        {
            scale = Math.Abs((cluePosition.x - v3BottomLeft.x)/max);
        }
        else
        {
            scale = Math.Abs((cluePosition.x - v3TopRight.x) / max);
        }
        if (max < clueBounds.extents.y)
        {
            max = clueBounds.extents.y;
            if (v3BottomLeft.y > v3TopRight.y)
            {
                scale = Math.Abs((cluePosition.y - v3BottomLeft.y) / max);
            }
            else
            {
                scale = Math.Abs((cluePosition.y - v3TopRight.y) / max);
            }
        }

        scale *= 0.9f;

        pointingObject.transform.localScale = new Vector3(pointingObject.transform.localScale.x * scale,
            pointingObject.transform.localScale.y * scale,
            pointingObject.transform.localScale.z * scale);
        
        // Position background
        var backgroundBounds = backgroundObject.GetComponent<MeshFilter>().mesh.bounds;
    }
}
