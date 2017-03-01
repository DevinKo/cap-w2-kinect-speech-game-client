using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Constants;
using Assets.Toolbox;

public class GameSession {

    private int currentTrial = -1;
    private Toolbox _toolbox;

    public string Email { get; set; }
    public string Password { get; set; }
    public System.DateTime StartTime;
    public System.DateTime EndTime;

    public GameCalibrationData CalibrationData = new GameCalibrationData();

    public List<GameTrial> Trials = new List<GameTrial>();

    public GameSession(string email, string password, Toolbox toolbox)
    {
        _toolbox = toolbox;
        Email = email;
        Password = password;
        StartTime = System.DateTime.Now;
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

    //public static Assets.DataContracts.Session ConvertToDataContract(GameSession session)
    //{
    //    var sessionContract = new Assets.DataContracts.Session();
    //    sessionContract.StartTime = session.StartTime.ToString("s");
    //    sessionContract.EndTime = session.EndTime.ToString("s");
    //    sessionContract.email = session.Email;
    //    sessionContract.password = session.Password;

    //    var trialContractList = new List<Assets.DataContracts.Trial>();
    //    foreach (var trial in session.Trials)
    //    {
    //        var trialContract = new Assets.DataContracts.Trial();
    //        trialContract.StartTime = trial.StartTime.ToString("s");
    //        trialContract.EndTime = trial.EndTime.ToString("s");
    //        trialContract.Difficulty = trial.Difficulty;
    //        var objectiveContractList = new List<Assets.DataContracts.Objectives>();
    //        foreach (var objective in trial.Objectives)
    //        {
    //            var objectiveContract = objective.ToDataContract();

    //            objectiveContractList.Add(objectiveContract);
    //        }
    //        trialContract.Objectives = objectiveContractList.ToArray();
    //        trialContractList.Add(trialContract);
    //    }
    //    sessionContract.Trials = trialContractList.ToArray();

    //    var calibrationContract = new Assets.DataContracts.CalibrationData();
    //    sessionContract.CalibrationData = calibrationContract;

    //    return sessionContract;
    //}

}
