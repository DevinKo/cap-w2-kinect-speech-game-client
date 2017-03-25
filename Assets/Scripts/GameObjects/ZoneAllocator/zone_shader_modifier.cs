using Assets.Toolbox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zone_shader_modifier : MonoBehaviour {
    Vector3 targetPos;
    private Toolbox _toolbox;
    GameObject pointObjRef;

    private float _pointingZoneTimer = 5;
    private bool _clueFound = false;
    private bool _countDownComplete = false;

    // Use this for initialization
    void Start () {
        _toolbox = FindObjectOfType<Toolbox>();

        Material mat = new Material(Shader.Find("Transparent/Diffuse"));

        Color newColor = Color.red;
        newColor.a = 0.5f;
        mat.color = newColor;
        GetComponent<Renderer>().material = mat;
        GetComponent<MeshRenderer>().enabled = false;

        pointObjRef = this.transform.parent.gameObject;

        _toolbox.EventHub.SpyScene.ZoneComplete += OnZoneCountDownComplete;
    }

    private void OnDestroy()
    {
        // unsubscribe
        _toolbox.EventHub.SpyScene.ZoneComplete -= OnZoneCountDownComplete;
    }

    // Update is called once per frame
    void Update () {
        RaycastHit hit;
        if (Cursor.Instance.IsTouchingPoint(gameObject, out hit))
        {
            if (!_clueFound)
            {
                _clueFound = true;
                GetComponent<MeshRenderer>().enabled = true;
                _toolbox.EventHub.SpyScene.RaiseZoneActivated();
            }
            else if (!_countDownComplete)
            {
                _pointingZoneTimer -= Time.deltaTime;

                if (_pointingZoneTimer < 0)
                {
                    _countDownComplete = true;
                    _toolbox.EventHub.SpyScene.OnZoneComplete();
                    
                    // turn off sphere zone
                    GetComponent<MeshRenderer>().enabled = false;
                }
            }
        }
    }

    public bool OnClueTouched(object sender, EventArgs e)
    {
        

        return true;
    }

    public void gotHit()
    {
        GetComponent<MeshRenderer>().enabled = true;
    }

    public void OnZoneCountDownComplete(object sedner, EventArgs e)
    {
       
    }

}
