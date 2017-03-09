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
    
    // Data collector instances
    Toolbox ToolBox;
    
    private bool doneMoving = false;
    
    private Cursor _cursor;
    private TaskList _taskList = new TaskList();
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

        // prompt user to point at obj
        _dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.Where);

        ToolBox.EventHub.SpyScene.ZoneComplete += OnZoneCountDownComplete;

        ToolBox.EventHub.SpyScene.RaiseLoadComplete();
    }

    // Update is called once per frame
    void Update()
    {
        
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    var dataClient = ToolBox.DataServerProxy;
        //    var sessionContract = Session.ConvertToDataContract(Session);
        //    dataClient.SendSession(sessionContract);
        //}
    }
    


    public void OnZoneCountDownComplete(object sender, EventArgs e)
    {
        // prompt player to indicate size
        _dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.HowBig);
    }
    
    public bool CheckIsDescribeComplete()
    {
        RaycastHit hit;
        if (_cursor.IsTouchingPoints(null, out hit))
        {

        }
        return true;
    }

}
