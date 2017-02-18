using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSequence : MonoBehaviour {
    
    GameObject cameraObject;
    GameObject dialogManagerRef;
    GameObject gameData;

    private bool doneMoving = false;

    // Use this for initialization
    void Start () {

        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        dialogManagerRef = GameObject.FindGameObjectWithTag("DialogManager");
        gameData = GameObject.FindGameObjectWithTag("GameDataCollector");

        // prompt user to point at obj
        dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.Where);

        // create the first session
        gameData.GetComponent<GameDataCollector>().newSession();

        // set the first task start time
        gameData.GetComponent<GameDataCollector>().setTaskStartTime();

        //StartCoroutine(bringPointingObjToCam());


    }
	
	// Update is called once per frame
	void Update () {

        checkForTaskOneComplete();

    }

    IEnumerator bringPointingObjToCam()
    {
        doneMoving = false;
        GameObject pointObjRef = GameObject.FindGameObjectWithTag("pointing_object");

        Vector3 camPos = cameraObject.transform.position;

        Vector3 targetPos = camPos;
        targetPos.z += 0.5f;

        float step = 1.5f * Time.deltaTime;

        while (true)
        {
            Vector3 currentPos = pointObjRef.transform.position;
            if (currentPos == targetPos)
            {
                yield break;
            }

            pointObjRef.transform.position = Vector3.MoveTowards(pointObjRef.transform.position, targetPos, step);

            yield return null;
        }
    }
    

    public void checkForTaskOneComplete()
    {
        if (cameraObject.GetComponent<raycast_mouse_cursor>().isComplete && gameData.GetComponent<GameDataCollector>().getCurrentTask() == (int)DialogManager.PROMPT.Where)
        {
            // store time to complete object identification task
            gameData.GetComponent<GameDataCollector>().storeTimeToCompleteTask();

            // turn off sphere zone
            var listOfZones = GameObject.FindGameObjectsWithTag("zone_collider");
            for (int i = 0; i < listOfZones.Length; i++)
            {
                listOfZones[i].GetComponent<MeshRenderer>().enabled = false;
            }

            // bring pointing object to the camera
            StartCoroutine(bringPointingObjToCam());

            // prompt player to indicate size
            dialogManagerRef.GetComponent<DialogManager>().updateDialogBox((int)DialogManager.PROMPT.HowBig);

            // update current task
            gameData.GetComponent<GameDataCollector>().setCurrentTask(DialogManager.PROMPT.HowBig);

            // set task start time
            gameData.GetComponent<GameDataCollector>().setTaskStartTime();
        }
    }

    public void checkForTaskTwoComplete()
    {
        
    }
}
