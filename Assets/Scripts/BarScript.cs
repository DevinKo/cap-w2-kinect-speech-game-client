using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    [SerializeField]
    private float fillAmount;

    [SerializeField]
    private Image content;

    [SerializeField]
    private Camera _mainCamera;

    private playerController _player;

    private float totalVolume;

	// Use this for initialization
	void Start ()
    {
        fillAmount = 0;
        _player = FindObjectOfType<playerController>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
	}

    private void HandleBar()
    {
        totalVolume += _player.getLastSound() * 1000;
        fillAmount = Map(totalVolume, 0, 2000);
        content.fillAmount = fillAmount;
    }

    private float Map(float value, float inMin, float inMax)
    {

        return (value - inMin) / (inMax - inMin);
    }
}
