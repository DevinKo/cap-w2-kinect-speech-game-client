using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneAllocator : MonoBehaviour {

    public GameObject sphereZonePrefab;
    public Vector3 largerBy;

	// Use this for initialization
	void Start () {

        var listOfPointObjects = GameObject.FindGameObjectsWithTag("pointing_object");
        foreach (var pointingObject in listOfPointObjects)
        {
            pointingObject.AddComponent<PointingObject>();
            var zone = (GameObject)Instantiate(sphereZonePrefab, pointingObject.transform.position, pointingObject.transform.rotation);
            zone.transform.parent = pointingObject.transform;
            zone.transform.localScale = pointingObject.transform.localScale + largerBy;



        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void DrawQuad(Rect position, Color color)
    {
        Texture2D texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, color);
        texture.Apply();
        GUI.skin.box.normal.background = texture;
        GUI.Box(position, GUIContent.none);
    }
}
