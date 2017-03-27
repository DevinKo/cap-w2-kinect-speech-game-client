using Assets.Toolbox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectSceneManager : MonoBehaviour
{
    private Toolbox _toolbox;

    // Use this for initialization
    void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();
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

