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

    // use to set cursor to mouse or hands
    public CursorTypes CursorType;

    public new void Awake()
    {
        base.Awake();
    }

    // Use this for initialization
    void Start()
    {
        // initialize cursor object based on type
        _cursor = CursorFactory.Create(CursorType);

        _dialogManagerRef = GameObject.FindGameObjectWithTag("DialogManager");

        // init toolbox
        ToolBox = FindObjectOfType<Toolbox>();
        
        ToolBox.EventHub.SpyScene.RaiseLoadComplete();
    }

    // Update is called once per frame
    void Update()
    {
        
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
