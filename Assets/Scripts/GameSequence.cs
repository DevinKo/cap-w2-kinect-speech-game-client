using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Toolbox;

public class GameSequence : MonoBehaviour {
    
    // GameObject references
    GameObject cameraObject;
    GameObject dialogManagerRef;
    GameObject gameData;

    // Define Session variables
    GameDataStorage.PlayerSession Session = new GameDataStorage.PlayerSession("prashant", "qwerty");
    GameDataStorage.OBJECTIVE CurrentState;

    // Data collector instances
    Toolbox ToolBox;

    private bool doneMoving = false;

    // Use this for initialization
    void Start () {

        // init object instance refs
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        dialogManagerRef = GameObject.FindGameObjectWithTag("DialogManager");
        gameData = GameObject.FindGameObjectWithTag("GameDataCollector");

        // init toolbox
        ToolBox = FindObjectOfType<GameManager>().Toolbox;

        // prompt user to point at obj
        dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.Where);

        // add a new trial to current session
        Session.NewTrial();
        CurrentState = GameDataStorage.OBJECTIVE.LOCATE;

    }
	
	// Update is called once per frame
	void Update () {

        CheckCurrentObjective();
        CheckForLocateObjectiveComplete();

    }

    public void CheckCurrentObjective()
    {
        GameDataStorage.OBJECTIVE CurrentSessionObjective = Session.GetCurrentObjective();

        if (CurrentState == GameDataStorage.OBJECTIVE.LOCATE && CurrentSessionObjective == GameDataStorage.OBJECTIVE.NONE)
        {
            Session.StartObjective(GameDataStorage.OBJECTIVE.LOCATE);
            StartCoroutine(CheckForIsFound());
            StartCoroutine(CollectDistanceSnapshot());
            StartCoroutine(GameDataStorage.CollectBodySnapshot(ToolBox, Session.GetCurrentObjectiveBodySnapshotList()));
            StartCoroutine(GameDataStorage.CollectAudioSnapshots(ToolBox, Session.GetCurrentObjectiveAudioSnapshotList()));
            print("running locate objective coroutines\n");
        }
        else if (CurrentState == GameDataStorage.OBJECTIVE.DESCRIBE && CurrentSessionObjective == GameDataStorage.OBJECTIVE.LOCATE)
        {
            StopAllCoroutines();
            StartCoroutine(CollectDistance2Snapshot());
            Session.StartObjective(GameDataStorage.OBJECTIVE.DESCRIBE);
            StartCoroutine(GameDataStorage.CollectBodySnapshot(ToolBox, Session.GetCurrentObjectiveBodySnapshotList()));
            StartCoroutine(GameDataStorage.CollectAudioSnapshots(ToolBox, Session.GetCurrentObjectiveAudioSnapshotList()));
            print("running describe objective coroutines\n");
        }
        else
        {
            //StopAllCoroutines();
        }
    }

    public IEnumerator CollectDistanceSnapshot()
    {
        while (true)
        {
            // collect distance information
            
            yield return null;
        }
    }

    public IEnumerator CollectDistance2Snapshot()
    {
        while (true)
        {
            // collect distance2 information
            yield return null;
        }
    }

    public IEnumerator CheckForIsFound()
    {
        print("looking for pointing zone");
        while (true)
        {
            GameDataStorage.OBJECTIVE currentObjectiveNow = Session.GetCurrentObjective();
            if (currentObjectiveNow != GameDataStorage.OBJECTIVE.LOCATE)
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


    public void CheckForLocateObjectiveComplete()
    {
        if (cameraObject.GetComponent<raycast_mouse_cursor>().isComplete && CurrentState == GameDataStorage.OBJECTIVE.LOCATE)
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
            Session.SetCurrentObjective(GameDataStorage.OBJECTIVE.DESCRIBE);
            CurrentState = Session.GetCurrentObjective();

            // set task start time
            Session.Trials[Session.GetCurrentTrial()].Objectives[(int)Session.GetCurrentObjective()].StartTime = System.DateTime.Now;
        }
    }

    public void checkForTaskTwoComplete()
    {
        
    }
}
