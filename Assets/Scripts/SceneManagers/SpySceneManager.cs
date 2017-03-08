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
        
        _taskList.Add(TaskName.CheckClueTouched, CheckClueTouched);
    }

    // Update is called once per frame
    void Update()
    {
        _taskList.ExecuteAll();
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    var dataClient = ToolBox.DataServerProxy;
        //    var sessionContract = Session.ConvertToDataContract(Session);
        //    dataClient.SendSession(sessionContract);
        //}
    }
    
    public bool CheckClueTouched()
    {
        RaycastHit hit;
        if (_cursor.IsTouchingPoint("zone_collider", out hit))
        {
            hit.collider.gameObject.GetComponent<zone_shader_modifier>().gotHit();
            _taskList.Remove(TaskName.CheckClueTouched);
            _taskList.Add(TaskName.EvaluateZoneCountDown, EvaluateZoneCountDown);
            ToolBox.EventHub.SpyScene.OnZoneActivated();
        }
        
        return true;
    }

    public bool EvaluateZoneCountDown()
    {
        RaycastHit hit;
        if (_cursor.IsTouchingPoint("zone_collider", out hit))
        {
            _pointingZoneTimer -= Time.deltaTime;

            if (_pointingZoneTimer < 0)
            {
                ToolBox.EventHub.SpyScene.OnZoneComplete();
                // turn off sphere zone
                var listOfZones = GameObject.FindGameObjectsWithTag("zone_collider");
                for (int i = 0; i < listOfZones.Length; i++)
                {
                    listOfZones[i].GetComponent<MeshRenderer>().enabled = false;
                }

                // prompt player to indicate size
                _dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.HowBig);
            }
        }
        return true;
    }
    
    public bool CheckIsDescribeComplete()
    {
        RaycastHit hit;
        if (_cursor.IsTouchingPoints("DescribeObject", out hit))
        {

        }
        return true;
    }

}
