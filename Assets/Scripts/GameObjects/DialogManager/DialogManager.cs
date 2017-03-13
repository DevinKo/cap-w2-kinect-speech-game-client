using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour {

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

        currentLine = -1;

        if (textFile != null)
        {
            textLines = (textFile.text.Split('\n'));
        }
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void updateDialogBox(int promptNum)
    {
        if (currentLine != promptNum)
        {
            //textObj.text = textLines[promptNum];
            currentLine = promptNum;
        }
    }

    public void clearDialogBox()
    {
        //textObj.text = "";
        currentLine = -1;
    }


}
