using Assets.Toolbox;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimerCamera : MonoBehaviour
{
    public GameObject CountdownTimer;
    public GameObject Background;

    private Camera clockCamera;
    private Toolbox _toolbox;
    
    private bool _cursorIsInZone = false;

    public void Start()
    {
        var mesh = Background.GetComponent<MeshRenderer>();
        mesh.material.color = Color.white;

        clockCamera = gameObject.GetComponent<Camera>();
        clockCamera.enabled = false;

        _toolbox = FindObjectOfType<Toolbox>();

        _toolbox.EventHub.SpyScene.ZoneActivated += OnPointZoneActivated;
        _toolbox.EventHub.SpyScene.ZoneComplete += OnZoneComplete;
        _toolbox.EventHub.SpyScene.ZoneEntered += OnZoneEntered;
        _toolbox.EventHub.SpyScene.ZoneExited += OnZoneExited;
    }

    private void OnDestroy()
    {
        _toolbox.EventHub.SpyScene.ZoneActivated -= OnPointZoneActivated;
        _toolbox.EventHub.SpyScene.ZoneComplete -= OnZoneComplete;
        _toolbox.EventHub.SpyScene.ZoneEntered -= OnZoneEntered;
        _toolbox.EventHub.SpyScene.ZoneExited -= OnZoneExited;
    }

    private IEnumerator RunClock(GameTimer timer)
    {
        while (true)
        {
            if (_cursorIsInZone)
            {
                timer.resumeTimer();
            }
            else
            {
                timer.pauseTimer();
            }
            yield return null;
        }
    }

    #region EventHandlers
    private void OnPointZoneActivated(object sender, EventArgs e)
    {
        clockCamera.enabled = true;
        _cursorIsInZone = true;

        var gameSettings = _toolbox.AppDataManager.GetGameSettings();
        var duration = gameSettings.PointingZoneDuration;

        var timer = CountdownTimer.GetComponent<GameTimer>();
        timer.startTimer(duration);

        if (timer != null)
            StartCoroutine("RunClock", timer);
    }

    private void OnZoneComplete(object sender, EventArgs e)
    {
        clockCamera.enabled = false;
        StopCoroutine("RunClock");
    }

    private void OnZoneEntered(object sender, EventArgs e)
    {
        _cursorIsInZone = true;
    }

    private void OnZoneExited(object sender, EventArgs e)
    {
        _cursorIsInZone = false;
    }
    #endregion EventHandlers
}
