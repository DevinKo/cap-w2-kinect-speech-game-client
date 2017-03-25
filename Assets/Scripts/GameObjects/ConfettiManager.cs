using Assets.Toolbox;
using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ConfettiManager : MonoBehaviour
{
    public GameObject ConfettiSource;

    private Toolbox _toolbox;

    private void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();

        // Subscribe to events
        _toolbox.EventHub.SpyScene.DescribeComplete += OnDescribeComplete;
        
    }

    private void Update()
    {
        var clue = BaseSceneManager.Instance.GetObjectWithName(GameObjectName.Clue);
        gameObject.transform.position = clue.transform.position;
    }

    #region Event Handlers
    private void OnDescribeComplete(object sender, EventArgs e)
    {
        ConfettiSource.SetActive(true);
        for (int i = 0; i < 5; i++)
        {
            var confettiSource = Instantiate(ConfettiSource, gameObject.transform);
            confettiSource.SetActive(true);
        }
    }
    #endregion Event Handlers
}
