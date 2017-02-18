using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataCollector : MonoBehaviour {

    private float taskStartTime;
    private int currentSession;

    public List<GameDataStorage.Session> Sessions = new List<GameDataStorage.Session>();

	// Use this for initialization
	void Start () {

        currentSession = -1;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void newSession()
    {
        Sessions.Add(new GameDataStorage.Session());
        currentSession++;
        Sessions[currentSession].currentTask = (int)DialogManager.PROMPT.Where;
    }

    public void setTaskStartTime()
    {
        taskStartTime = Time.time;
    }

    public void storeTimeToCompleteTask()
    {
        if (Sessions[currentSession].currentTask == (int)DialogManager.PROMPT.Where)
        {
            Sessions[currentSession].setIdentifyObjectTime(Time.time - taskStartTime);
            print(Sessions[currentSession].getIdentifyObjectTime());
            Sessions[currentSession].currentTask = (int)DialogManager.PROMPT.HowBig;
        }
        else if (Sessions[currentSession].currentTask == (int)DialogManager.PROMPT.HowBig)
        {
            Sessions[currentSession].setIndicateSizeTime(Time.time - taskStartTime);
            print(Sessions[currentSession].getIndicateSizeTime());
        }
    }

    public int getCurrentTask()
    {
        return Sessions[currentSession].currentTask;
    }
    public void setCurrentTask(DialogManager.PROMPT currentTask)
    {
        Sessions[currentSession].currentTask = (int)currentTask;
    }
}
