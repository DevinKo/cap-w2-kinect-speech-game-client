using Assets.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CalibrationSceneManager : BaseSceneManager
{
    private bool _firstUpdate = true;

    private Toolbox _toolbox;
    private Cursor _cursor;

    private void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();

        // initialize cursor object based on type
        var cursorType = _toolbox.AppDataManager.GetGameSettings().CursorType;
        _cursor = CursorFactory.Create(cursorType);
    }

    private void Update()
    {
            // This lets folks know that the scenes Start loop has finished.
            if (_firstUpdate)
            {
                _firstUpdate = false;
                _toolbox.EventHub.CalibrationScene.RaiseLoadComplete();
            }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _toolbox.EventHub.CalibrationScene.RaiseSceneEnd();
        }
    }
}