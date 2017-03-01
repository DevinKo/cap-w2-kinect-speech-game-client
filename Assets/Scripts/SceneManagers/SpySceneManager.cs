using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Toolbox;
using Constants;

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
        gameData = GameObject.FindGameObjectWithTag("GameDataCollector");

        // init toolbox
        ToolBox = FindObjectOfType<GameManager>().Toolbox;
        Session = ToolBox.AppDataManager.GetSession();

        // prompt user to point at obj
        dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.Where);

        // add a new trial to current session
        CurrentTrial = Session.AddTrial();
        
        // configure locate objective
        var locateObjective = CurrentTrial.AddObjective(OBJECTIVE.LOCATE, IsLocateObjectiveComplete);
        ToolBox.EventHub.SpyScene.OnZoneActivated();
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
    
    public IEnumerator CheckForIsFound()
    {
        print("looking for pointing zone");
        while (true)
        {
            OBJECTIVE currentObjectiveNow = Session.GetCurrentObjective();
            if (currentObjectiveNow != OBJECTIVE.LOCATE)
            {
                print("session is not in locate objective.\ncurrent Objective: " + currentObjectiveNow);
                yield break;
            }
            if (cameraObject.GetComponent<raycast_mouse_cursor>().isFound)
            {
                print("pointing zone found");
                Session.SetZoneActiviationTime();
                yield break;
            }
            yield return null;
        }
    }


    public bool IsLocateObjectiveComplete()
    {
        if (cameraObject.GetComponent<raycast_mouse_cursor>().isComplete && CurrentObjectiveType == OBJECTIVE.LOCATE)
        {
            print("entering task one completion method\n");
            // store time to complete object identification task
            Session.Trials[Session.GetCurrentTrial()].Objectives[(int)Session.GetCurrentObjective()].EndTime = System.DateTime.Now;

            // turn off sphere zone
            var listOfZones = GameObject.FindGameObjectsWithTag("zone_collider");
            for (int i = 0; i < listOfZones.Length; i++)
            {
                listOfZones[i].GetComponent<MeshRenderer>().enabled = false;
            }

            // prompt player to indicate size
            dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.HowBig);

            // update current task
            Session.SetCurrentObjective(OBJECTIVE.DESCRIBE);
            CurrentObjectiveType = Session.GetCurrentObjective();

            // set task start time
            Session.Trials[Session.GetCurrentTrial()].Objectives[(int)Session.GetCurrentObjective()].StartTime = System.DateTime.Now;
            return true;
        }
        return false;
    }

    public bool IsDescribeObjectiveComplete()
    {
        return false;
    }
}
