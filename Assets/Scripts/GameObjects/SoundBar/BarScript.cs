using Assets.Toolbox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BarScript : MonoBehaviour {

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Camera _mainCamera;

    private Toolbox _toolbox;

    private float totalVolume;

    private bool soundBarOn = false;

    Image mask;

    Image SoundBarImage;

    Color contentColor;
    Color maskColor;
    Color grayColor = new Color(119, 136, 153);

	// Use this for initialization
	void Start ()
    {
        fillAmount = 0;
        _toolbox = FindObjectOfType<Toolbox>();

        // Subscribe to events
        _toolbox.EventHub.SpyScene.ClueMoved += SetupDescribeSoundBar;
        _toolbox.EventHub.SpyScene.DescribingSize += describingSizeEventHandler;

        SoundBarImage = GetComponent<Image>();
        mask = transform.FindChild("Mask").GetComponent<Image>();

        maskColor = mask.color;
        contentColor = content.color;

        SoundBarImage.enabled = false;
        mask.enabled = false;
        content.enabled = false;

        soundBarOn = false;
        SoundBarImage.color = grayColor;
        content.color = grayColor;
        mask.color = grayColor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(soundBarOn)
            HandleBar();
	}

    private void HandleBar()
    {
        totalVolume += _toolbox.VolumeSourceManager.Decibel;
        fillAmount = Map(totalVolume, 0, 2000);
        content.fillAmount = fillAmount;
    }

    private float Map(float value, float inMin, float inMax)
    {
        return (value - inMin) / (inMax - inMin);
    }

    public void SetupDescribeSoundBar(object sender, EventArgs e)
    {
        SoundBarImage.enabled = true;
        mask.enabled = true;
        content.enabled = true;
    }

    public void describingSizeEventHandler(object sender, EventArgs e, bool isDescribing)
    {
        if (isDescribing)
        {
            soundBarOn = true;
            content.color = contentColor;
            mask.color = maskColor;
        }
        else
        {
            soundBarOn = false;
            content.color = grayColor;
            mask.color = grayColor;
        }
    }
}
