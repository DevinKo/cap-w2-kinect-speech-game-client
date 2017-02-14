using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class game_sequence : MonoBehaviour {

    GameObject cameraObject;
    Vector3 targetPos;

    // Use this for initialization
    void Start () {
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
    }
	
	// Update is called once per frame
	void Update () {
		if (cameraObject.GetComponent<raycast_mouse_cursor>().isComplete)
        {
            // turn off sphere zone
            var listOfZones = GameObject.FindGameObjectsWithTag("zone_collider");
            for (int i = 0; i < listOfZones.Length; i++)
            {
                listOfZones[i].GetComponent<MeshRenderer>().enabled = false;
            }
            // bring pointing object to the camera
            var listOfPointObjects = GameObject.FindGameObjectsWithTag("pointing_object");
            Vector3 camPos = cameraObject.transform.position;
            targetPos = camPos;
            targetPos.z += 0.5f;
            for (int i=0; i < listOfPointObjects.Length; i++)
            {
                float step = 1.5f * Time.deltaTime;
                listOfPointObjects[i].transform.position = Vector3.MoveTowards(listOfPointObjects[i].transform.position, targetPos, step);
            }
        }
	}
}
