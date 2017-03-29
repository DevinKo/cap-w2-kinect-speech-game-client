using Assets.Toolbox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelectSceneManager : MonoBehaviour
{
    public GameObject LogoutButton;
    public GameObject LoginButton;

    private Toolbox _toolbox;

    // Use this for initialization
    void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();

        if (_toolbox.AppAuth.IsLoggedIn)
        {
            LogoutButton.SetActive(true);
            LoginButton.SetActive(false);
        }
        else
        {
            LogoutButton.SetActive(false);
            LoginButton.SetActive(true);
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        _toolbox.EventHub.LevelSelect.RaiseLevelLoaded();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RaiseSceneEnd()
    {
        _toolbox.EventHub.LevelSelect.RaiseLevelEnd();
    }
}

