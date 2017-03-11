using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

using System.Collections.Generic;       //Allows us to use Lists. 
using Assets.Toolbox;
using System;

public class GameManager : MonoBehaviour
{
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

        InitTestScene();
    }

    public void Start()
    {
        // create new session track game data
        var session = new GameSession("p.mcpatientface@email.com", "mFjDhCdzCw", Toolbox);
        Toolbox.AppDataManager.Save(session);
        Toolbox.EventHub.SpyScene.DescribeComplete += OnSessionComplete;
    }

    public void InitTestScene()
    {
        TestSceneManager.OnSceneEnd += LoadSpyScene;
    }

    public void LoadSpyScene()
    {
        SceneManager.LoadScene("template_Scene");
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
    #endregion Event Handlers
}