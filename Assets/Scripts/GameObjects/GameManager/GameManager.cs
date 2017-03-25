using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;       //Allows us to use Lists. 
using Assets.Toolbox;
using System;
using GameMenuKit;

public class GameManager : MonoBehaviour
{
    // use to set cursor to mouse or hands
    public Constants.CursorTypes CursorType;
    public bool _testing = false;

    public static GameManager instance = null;

    public Toolbox Toolbox;

    //Awake is always called before any Start functions
    void Awake()
    {
        //Check if instance already exists
        if (instance == null)
        {
            //if not, set instance to this
            instance = this;
            Toolbox = gameObject.AddComponent<Toolbox>();
        }
        //If instance already exists and it's not this:
        else if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
        //Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
        
    }

    public void Start()
    {
        // create new session track game data
        var session = new GameSession("p.mcpatientface@email.com", "mFjDhCdzCw", Toolbox);
        Toolbox.AppDataManager.Save(session);

        // Set game settings
        Toolbox.AppDataManager.Save(new GameSettings() { CursorType = this.CursorType });

        // Start a new trial here so 

        Toolbox.EventHub.CalibrationScene.SceneEnd += OnCalibrationSceneEnd;
        Toolbox.EventHub.SpyScene.Completed += OnSpySceneComplete;
    }

    public void LoadSpyScene()
    {
        SceneManager.LoadScene("demo_Kenny");
    }

    public void LoadLevelSelect()
    {
        SceneManager.LoadScene("LevelSelectMenu");
    }

    public void CompleteAndSendSession()
    {
        Toolbox.EventHub.GameManager.RaiseSessionComplete();
        OnSessionComplete(this, new EventArgs());
    }

    //Update is called every frame.
    void Update()
    {

    }

    #region Event Handlers
    private void OnSessionComplete(object sender, EventArgs e)
    {
        var session = Toolbox.AppDataManager.GetSession();
        Toolbox.DataServerProxy.Send(session);
    }

    private void OnCalibrationSceneEnd(object sender, EventArgs e)
    {
        LoadLevelSelect();
    }

    private void OnSpySceneComplete(object sender, EventArgs e)
    {
        GMK.LevelManager.CompleteLevel(0, "");
        LoadLevelSelect();
    } 
    #endregion Event Handlers
}