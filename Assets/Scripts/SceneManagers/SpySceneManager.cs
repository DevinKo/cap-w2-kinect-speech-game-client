using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Toolbox;
using Constants;
using System;

public class SpySceneManager : BaseSceneManager
{
    // GameObject references
    GameObject _dialogManagerRef;
    public bool AreHandsOutside { get; set; }
    // Data collector instances
    Toolbox ToolBox;

    private bool _isDescribeCurrent;

    private bool doneMoving = false;
    
    private Cursor _cursor;

    private float _pointingZoneTimer = 5;

    // is set to false during first update
    private bool _firstUpdate = true;

    public new void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        _dialogManagerRef = GameObject.FindGameObjectWithTag("DialogManager");

        // init toolbox
        ToolBox = FindObjectOfType<Toolbox>();

        // initialize cursor object based on type
        var cursorType = ToolBox.AppDataManager.GetGameSettings().CursorType;
        _cursor = CursorFactory.Create(cursorType);

    }

    // Update is called once per frame
    void Update()
    {
        // This lets folks know that the scenes Start loop has finished.
        if (_firstUpdate)
        {
            _firstUpdate = false;
            ToolBox.EventHub.SpyScene.RaiseLoadComplete();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToolBox.EventHub.SpyScene.RaiseDescribeComplete();
        }
        if (_isDescribeCurrent)
        {
            var describeHandLeft = Instance.GetObjectWithName(GameObjectName.DescribeHandLeft);
            var describeHandRight = Instance.GetObjectWithName(GameObjectName.DescribeHandRight);
            if (describeHandLeft != null && describeHandRight != null)
            {
                AreHandsOutside = Cursor.Instance.IsOutsideOfX(describeHandLeft, describeHandRight);
            }
        }
    }
    
    

}
