using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession {

    public string Email { get; set; }
    public string Password { get; set; }
    public System.DateTime StartTime;
    public System.DateTime EndTime;

    public GameCalibrationData CalibrationData = new GameCalibrationData();

    public List<GameTrial> Trials = new List<GameTrial>();

    public GameSession(string email, string password)
    {
        Email = email;
        Password = password;
        StartTime = System.DateTime.Now;
    }

}
