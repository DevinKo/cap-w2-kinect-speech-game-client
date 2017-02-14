using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
	}

    private void HandleBar()
    {
        content.fillAmount = Map(0, 0, 100);
    }

    private float Map(float value, float inMin, float inMax)
    {

        return (value - inMin) / (inMin - inMax);
    }
}
