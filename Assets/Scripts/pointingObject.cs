using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pointingObject : MonoBehaviour {

    public GameObject sphereZonePrefab;
    public Vector3 largerBy;

	// Use this for initialization
	void Start () {

        var listOfPointObjects = GameObject.FindGameObjectsWithTag("pointing_object");
        for (int i=0; i<listOfPointObjects.Length; i++)
        {
            var zone = (GameObject)Instantiate(sphereZonePrefab, listOfPointObjects[i].transform.position, listOfPointObjects[i].transform.rotation);
            zone.transform.parent = listOfPointObjects[i].transform;
            zone.transform.localScale = listOfPointObjects[i].transform.localScale + largerBy;
        }

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
