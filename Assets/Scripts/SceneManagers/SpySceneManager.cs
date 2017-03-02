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

    // Use this for initialization
    void Start()
    {

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
        ToolBox.EventHub.SpyScene.ZoneComplete += OnZoneComplete;

        // configure describe objective
        var describeObjective = CurrentTrial.AddObjective(OBJECTIVE.DESCRIBE, IsDescribeObjectiveComplete);

        _currentObjective = CurrentTrial.Start();
    }

    // Update is called once per frame
    void Update()
    {
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
    


    public void OnZoneComplete(object sender, EventArgs e)
    {
            // turn off sphere zone
            var listOfZones = GameObject.FindGameObjectsWithTag("zone_collider");
            for (int i = 0; i < listOfZones.Length; i++)
            {
                listOfZones[i].GetComponent<MeshRenderer>().enabled = false;
            }

            // prompt player to indicate size
            dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.HowBig);

    }

    public bool IsDescribeObjectiveComplete()
    {
        return false;
    }
}
