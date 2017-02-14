using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_shader_modifier : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Material mat = new Material(Shader.Find("Transparent/Diffuse"));

        Color newColor = Color.red;
        newColor.a = 0.5f;
        mat.color = newColor;
        GetComponent<Renderer>().material = mat;
        GetComponent<MeshRenderer>().enabled = false;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
