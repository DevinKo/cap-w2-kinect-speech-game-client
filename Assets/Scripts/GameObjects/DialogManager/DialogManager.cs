using Assets.Scripts.Constants;
using Assets.Toolbox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {
    private Toolbox _toolbox;

    public TextAsset textFile;
    public Text textObj;
    public string[] textLines;

    private int currentLine;

    public enum PROMPT
    {
        Where = 0,
        HowBig = 1
    }

    // Use this for initialization
    void Start () {
        _toolbox = FindObjectOfType<Toolbox>();

        currentLine = -1;

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
        
        _toolbox.EventHub.SpyScene.LoadComplete += OnSpySceneStart;
        _toolbox.EventHub.SpyScene.ZoneComplete += OnDescribeStart;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateDialogBox(string text)
    {
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
        updateDialogBox(DisplayStrings.Instruction_DescribeClue);
    }
}
