using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_shader_modifier : MonoBehaviour {
    Vector3 targetPos;

    GameObject pointObjRef;

    // Use this for initialization
    void Start () {

        Material mat = new Material(Shader.Find("Transparent/Diffuse"));

        Color newColor = Color.red;
        newColor.a = 0.5f;
        mat.color = newColor;
        GetComponent<Renderer>().material = mat;
        GetComponent<MeshRenderer>().enabled = false;

        pointObjRef = this.transform.parent.gameObject;
    }
	
	// Update is called once per frame
	void Update () {
	}

    public void gotHit()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    IEnumerator bringPointingObjToCam()
    {
        //bool doneMoving = false;
        GameObject pointObjRef = this.transform.parent.gameObject;
        GameObject.FindGameObjectWithTag("pointing_object");

        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");

        Vector3 camPos = cameraObject.transform.position;

        float camDir = cameraObject.transform.rotation.y;

        targetPos = camPos;

        switch ((int)camDir)
        {
            case -180:
                targetPos.z -= .5f;
                break;

            case 0:
                targetPos.z += .5f;
                break;

            case 90:
                targetPos.x += .5f;
                break;

            case -90:
                targetPos.x -= .5f;
                break;
        }

        //targetPos.z = 1.132f;

        while (true)
        {
            float step = .5f * Time.deltaTime;

            if (pointObjRef.transform.position == targetPos)
            {
                yield break;
            }

            pointObjRef.transform.position = Vector3.MoveTowards(pointObjRef.transform.position, targetPos, step);
            yield return null;
        }
    }

    public void moveParent()
    {
        GetComponent<MeshRenderer>().enabled = false;

        // bring pointing object to the camera
        StartCoroutine(bringPointingObjToCam());
    }


}
