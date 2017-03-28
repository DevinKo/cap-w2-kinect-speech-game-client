﻿using Assets.Toolbox;
using Constants;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PointingObject : MonoBehaviour
{
    private Toolbox _toolbox;

    private void Awake()
    {

    }

    private void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        // Add object reference for clue to scene manager.
        BaseSceneManager.Instance.AddGameObject(GameObjectName.Clue, gameObject);

        // Subscribe to events
        _toolbox.EventHub.SpyScene.ZoneComplete += MoveParent;
    }

    private void Update()
    {

    }


    IEnumerator bringPointingObjToCam()
    {
        var targetPos = Camera.main.transform.position + Camera.main.transform.forward * 0.5f;
        
        while (true)
        {
            float step = Time.deltaTime;

            if (gameObject.transform.position == targetPos)
            {
                gameObject.transform.eulerAngles = new Vector3(0, 0, 0);
                //gameObject.transform.localScale = new Vector3(1.0F, 1.0F, 1.0F);
                _toolbox.EventHub.SpyScene.RaiseClueMoved();
                yield break;
            }

            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPos, step);
            yield return null;
        }
    }

    public void MoveParent(object sender, EventArgs e)
    {
        // bring pointing object to the camera
        StartCoroutine(bringPointingObjToCam());
    }

    public void OnDestroy()
    {
        // UnSubscribe to events
        _toolbox.EventHub.SpyScene.ZoneComplete -= MoveParent;
    }
}

