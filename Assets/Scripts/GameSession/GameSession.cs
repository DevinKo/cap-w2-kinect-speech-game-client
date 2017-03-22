using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using Assets.Toolbox;
using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

[JsonObject(MemberSerialization.OptIn)]
public class GameSession {

    private int currentTrial = -1;
    private Toolbox _toolbox;

    [JsonProperty]
    public string email { get; set; }
    [JsonProperty]
    public string password { get; set; }
    [JsonProperty]
    public System.DateTime StartTime;
    [JsonProperty]
    public System.DateTime EndTime;

    [JsonProperty]
    public GameCalibrationData CalibrationData;

    [JsonProperty]
    public List<GameTrial> Trials = new List<GameTrial>();

    public GameSession(string email, string password, Toolbox toolbox)
    {
        _toolbox = toolbox;
        this.email = email;
        this.password = password;
        StartTime = System.DateTime.Now;

        CalibrationData = new GameCalibrationData(_toolbox);

        _toolbox.EventHub.GameManager.SessionComplete += OnSessionComplete;
        _toolbox.EventHub.GameManager.StartTrial += OnStartTrial;
    }

    public int GetCurrentTrial()
    {
        return currentTrial;
    }

    public GameTrial AddTrial()
    {
        var trial = new GameTrial(_toolbox);
        currentTrial++;
        Trials.Add(trial);
        return Trials[currentTrial];
    }

    #region Event Handlers
    private void OnSessionComplete(object sender, EventArgs e)
    {
        EndTime = DateTime.Now;
    }

    private void OnStartTrial(object sender, EventArgs e)
    {
        // add a new trial to current session
        AddTrial();
    }
    #endregion Event Handlers
}
