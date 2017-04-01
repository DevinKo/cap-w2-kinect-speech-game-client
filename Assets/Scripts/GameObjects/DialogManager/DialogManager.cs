using Assets.Scripts.Constants;
using Assets.Toolbox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    private Toolbox _toolbox;
    public Text textObj;

    private bool dialogTimerStartTimeSet = false;
    private DateTime dialogTimerStartTime;

    // Use this for initialization
    void Start () {

        _toolbox = FindObjectOfType<Toolbox>();
        
        _toolbox.EventHub.SpyScene.LoadComplete += OnSpySceneStart;
        _toolbox.EventHub.SpyScene.ZoneComplete += OnDescribeStart;
        _toolbox.EventHub.SpyScene.ClueMoved += AfterClueMoved;
        _toolbox.EventHub.SpyScene.DescribeComplete += OnLevelComplete;
	}

    public void OnDestroy()
    {
        _toolbox.EventHub.SpyScene.LoadComplete -= OnSpySceneStart;
        _toolbox.EventHub.SpyScene.ZoneComplete -= OnDescribeStart;
        _toolbox.EventHub.SpyScene.ClueMoved -= AfterClueMoved;
        _toolbox.EventHub.SpyScene.DescribeComplete -= OnLevelComplete;
    }

    // Update is called once per frame
    void Update () {
        dialogTimer();
	}

    public void dialogTimer()
    {
        if (textObj.text == DisplayStrings.Instruction_DescribeClue && !dialogTimerStartTimeSet)
        {
            dialogTimerStartTime = DateTime.Now;
            dialogTimerStartTimeSet = true;
        }

        if (dialogTimerStartTimeSet && (DateTime.Now - dialogTimerStartTime).Seconds > 10)
        {
            updateDialogBox(DisplayStrings.Instruction2_DescribeClue);
        }

        if (dialogTimerStartTimeSet && (DateTime.Now - dialogTimerStartTime).Seconds > 20)
        {
            updateDialogBox(DisplayStrings.Instruction3_DescribeClue);
        }
    }

    public void updateDialogBox(string text)
    {
        if (textObj == null) { return; }
        textObj.text = text;
    }

    public void clearDialogBox()
    {
        textObj.text = "";
    }

    private void OnSpySceneStart(object sender, EventArgs e)
    {
        updateDialogBox(DisplayStrings.Instruction_FindClue);
    }

    private void OnDescribeStart(object sender, EventArgs e)
    {
        updateDialogBox("");
    }

    private void AfterClueMoved(object sender, EventArgs e)
    {
        updateDialogBox(DisplayStrings.Instruction_DescribeClue);
    }

    private void OnLevelComplete(object sender, EventArgs e)
    {
        updateDialogBox(DisplayStrings.Completion_Text);
    }
}
