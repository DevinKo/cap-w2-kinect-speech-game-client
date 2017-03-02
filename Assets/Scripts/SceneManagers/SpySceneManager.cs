using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Toolbox;
using Constants;
using System;

public class SpySceneManager : MonoBehaviour
{



    // GameObject references
    GameObject cameraObject;
    GameObject dialogManagerRef;
    GameObject gameData;

    // Define Session variables
    OBJECTIVE CurrentObjectiveType;
    GameSession Session;
    GameTrial CurrentTrial;

    // Data collector instances
    Toolbox ToolBox;
    private GameObjective _currentObjective;

    private bool doneMoving = false;
    private bool[] coroutinesRunning = new bool[] { false, false };
    public bool[] isObjectiveComplete = new bool[] { false, false };

    private Cursor _cursor;
    private TaskList _taskList = new TaskList();
    private float _pointingZoneTimer = 5;

    public CursorTypes CursorType;

    // Use this for initialization
    void Start()
    {
        _cursor = CursorFactory.Create(CursorType);

        // init object instance refs
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        dialogManagerRef = GameObject.FindGameObjectWithTag("DialogManager");

        // init toolbox
        ToolBox = FindObjectOfType<Toolbox>();
        Session = ToolBox.AppDataManager.GetSession();

        // prompt user to point at obj
        dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.Where);

        // add a new trial to current session
        CurrentTrial = Session.AddTrial();
        
        // configure locate objective
        var locateObjective = CurrentTrial.AddObjective(OBJECTIVE.LOCATE, null);

        // configure describe objective
        var describeObjective = CurrentTrial.AddObjective(OBJECTIVE.DESCRIBE, null);

        _currentObjective = CurrentTrial.Start();

        _taskList.Add(TaskName.CheckClueTouched, CheckTouchingClue);
    }

    // Update is called once per frame
    void Update()
    {
        _taskList.ExecuteAll();
        if (_currentObjective.IsComplete)
        {
            _currentObjective = CurrentTrial.StartNextObjective();
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    var dataClient = ToolBox.DataServerProxy;
        //    var sessionContract = Session.ConvertToDataContract(Session);
        //    dataClient.SendSession(sessionContract);
        //}
    }
    
    public bool CheckTouchingClue()
    {
        RaycastHit hit;
        if (_cursor.IsTouching("zone_collider", out hit))
        {
            hit.collider.gameObject.GetComponent<zone_shader_modifier>().gotHit();
            ToolBox.EventHub.SpyScene.OnZoneActivated();
        }
        _taskList.Remove(TaskName.CheckClueTouched);
        _taskList.Add(TaskName.EvaluateZoneCountDown, EvaluateZoneCountDown);
        return true;
    }

    public bool EvaluateZoneCountDown()
    {
        RaycastHit hit;
        if (_cursor.IsTouching("zone_collider", out hit))
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
                dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.HowBig);
            }
        }
        return true;
    }
    
}
