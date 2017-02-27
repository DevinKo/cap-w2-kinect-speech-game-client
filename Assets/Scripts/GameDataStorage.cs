﻿using System.Collections;
using System.Collections.Generic;
using Assets.DataContracts;
using Assets.Toolbox;
using UnityEngine;
using System;

public class GameDataStorage : MonoBehaviour {

    public enum OBJECTIVE
    {
        LOCATE,
        DESCRIBE,
        NONE
    }

    public class Session
    {
        public Session() { }

        private float identifyObjectTime;
        private float indicateSizeTime;

        public int currentTask;

        // setters, getters
        public void setIdentifyObjectTime(float time)
        {
            identifyObjectTime = time;
        }
        public float getIdentifyObjectTime()
        {
            return identifyObjectTime;
        }

        public void setIndicateSizeTime(float time)
        {
            indicateSizeTime = time;
        }
        public float getIndicateSizeTime()
        {
            return indicateSizeTime;
        }
    }

    // --------------------------- NEW DATA STRUCTURE ----------------------------------
    
    public class PlayerSession
    {
        public string Email;
        public string Password;
        public System.DateTime StartTime;
        public System.DateTime EndTime;

        public List<Trial> Trials = new List<Trial>();

        private int currentTrial;
        private OBJECTIVE currentObjective;

        // CONSTRUCTOR
        public PlayerSession(string email, string password)
        {
            Email = email;
            Password = password;
            StartTime = System.DateTime.Now;
        }

        // SETTERS/GETTERS
        public int GetCurrentTrial()
        {
            return currentTrial;
        }

        public void SetCurrentObjective(OBJECTIVE NewObjective)
        {
            currentObjective = NewObjective;
        }

        public OBJECTIVE GetCurrentObjective()
        {
            return currentObjective;
        }

        public void SetEndTime()
        {
            EndTime = System.DateTime.Now;
        }

        public List<BodySnapshot> GetCurrentObjectiveBodySnapshotList()
        {
            return Trials[currentTrial].Objectives[(int)currentObjective].BodySnapshots;
        }

        public List<AudioSnapshot> GetCurrentObjectiveAudioSnapshotList()
        {
            return Trials[currentTrial].Objectives[(int)currentObjective].AudioSnapshots;
        }

        public void SetZoneActiviationTime()
        {
            LocateObjective tempRef = Trials[currentTrial].Objectives[(int)OBJECTIVE.LOCATE] as LocateObjective;
            if (tempRef != null)
                tempRef.ActivationTime = System.DateTime.Now;
            else
                print("failed LocateObjective Casting in SetZoneActivationTime()");
        }

        // ADDITIONAL METHODS
        public void NewTrial()
        {
            Trials.Add(new Trial(System.DateTime.Now));
            currentTrial = Trials.Count - 1;
            currentObjective = OBJECTIVE.NONE;
        }

        public void StartObjective(OBJECTIVE ObjectiveType)
        {
            currentObjective = ObjectiveType;
            Trials[currentTrial].Objectives[(int)ObjectiveType].StartTime = System.DateTime.Now;
        }

        public void CollectDistanceSnapshot(float distance)
        {
            LocateObjective tempRef = Trials[currentTrial].Objectives[(int)OBJECTIVE.LOCATE] as LocateObjective;

            DistanceSnapshot distanceSnapshot = new DistanceSnapshot();
            distanceSnapshot.setSnapshot(distance);

            tempRef.DistanceSnapshots.Add(distanceSnapshot);
        }

        public void CollectDistance2Snapshot(float dist1, float dist2)
        {
            DescribeObjective tempRef = Trials[currentTrial].Objectives[(int)OBJECTIVE.DESCRIBE] as DescribeObjective;

            Distance2Snapshot distance2Snapshot = new Distance2Snapshot();
            distance2Snapshot.setSnapshot(dist1, dist2);

            tempRef.Distance2Snapshot.Add(distance2Snapshot);
        }

    }

    public class Trial
    {
        public System.DateTime StartTime;
        public System.DateTime EndTime;
        public int Difficulty;
        public Objectives[] Objectives = new Objectives[] { new LocateObjective(), new DescribeObjective() };

        public Trial(System.DateTime currentTime)
        {
            this.StartTime = currentTime;
        }

    }

    public class Objectives
    {
        public System.DateTime StartTime;
        public System.DateTime EndTime;
        public List<BodySnapshot> BodySnapshots = new List<BodySnapshot>();
        public List<AudioSnapshot> AudioSnapshots = new List<AudioSnapshot>();
        public virtual Assets.DataContracts.Objectives ToDataContract() { return null; }
    }

    public class LocateObjective : Objectives
    {
        public string kind = "LocateObjective";

        // time at which the pointing zone was activated
        public System.DateTime ActivationTime;

        // distance from the hands mid-point to the object to identify
        public List<DistanceSnapshot> DistanceSnapshots = new List<DistanceSnapshot>();

        public override Assets.DataContracts.Objectives ToDataContract()
        {
            var objectiveContract = new Assets.DataContracts.LocateObjective();
            objectiveContract.StartTime = StartTime.ToString("s");
            objectiveContract.EndTime = EndTime.ToString("s");
            objectiveContract.AudioSnapshots = AudioSnapshots.ToArray();
            objectiveContract.BodySnapshots = BodySnapshots.ToArray();
            objectiveContract.kind = kind;
            objectiveContract.DistanceSnapshots = DistanceSnapshots.ToArray();
            objectiveContract.ActivationTime = ActivationTime.ToString("s");
            return objectiveContract;
        }        
    }

    public class DescribeObjective : Objectives
    {
        public string kind = "DescribeObjective";

        public List<Distance2Snapshot> Distance2Snapshot = new List<Distance2Snapshot>();

        public override Assets.DataContracts.Objectives ToDataContract()
        {
            var objectiveContract = new Assets.DataContracts.DescribeObjective();
            objectiveContract.StartTime = StartTime.ToString("s");
            objectiveContract.EndTime = EndTime.ToString("s");
            objectiveContract.AudioSnapshots = AudioSnapshots.ToArray();
            objectiveContract.BodySnapshots = BodySnapshots.ToArray();
            objectiveContract.kind = kind;
            objectiveContract.Distance2Snapshot = Distance2Snapshot.ToArray();
            return objectiveContract;
        }
    }

    public static IEnumerator CollectAudioSnapshots(Toolbox toolBox, List<AudioSnapshot> AudioSnapshotList)
    {
        while (true)
        {
            if (!toolBox.VolumeCollector)
            {
                yield return null;
                continue;
            }
            var audioSnapshot = new AudioSnapshot()
            {
                Intensity = toolBox.VolumeCollector.Decibel,
                Time = System.DateTime.Now.ToString("s"),
            };
            AudioSnapshotList.Add(audioSnapshot);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public static IEnumerator CollectBodySnapshot(Toolbox toolBox, List<BodySnapshot> BodySnapshotList)
    {
        while (true)
        {
            ////////// Test
            //var s = Assets.MockDataContracts.MockSnapshots.BodySnapshot1;
            //s.Time = Time.time;
            //_toolbox.AppDataManager.Save(s);
            //yield return new WaitForSeconds(3);
            ////////////// Test
            if (toolBox.BodySourceManager == null)
            {
                yield return null;
                continue;
            }
            var body = toolBox.BodySourceManager.GetFirstTrackedBody();
            if (body == null)
            {
                yield return null;
                continue;
            }

            var joints = new List<Assets.DataContracts.Joint>();

            foreach (var joint in body.Joints)
            {
                var pos = joint.Value.Position;
                joints.Add(new Assets.DataContracts.Joint()
                {
                    JointType = joint.Key.ToString(),
                    X = pos.X,
                    Y = pos.Y,
                    Z = pos.Z
                });
            }

            var snapshot = new BodySnapshot()
            {
                Joints = joints.ToArray(),
                Time = System.DateTime.Now.ToString("s"),
            };

            BodySnapshotList.Add(snapshot);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public static Assets.DataContracts.Session ConvertToDataContract(PlayerSession session)
    {
        var sessionContract = new Assets.DataContracts.Session();
        sessionContract.StartTime = session.StartTime.ToString("s");
        sessionContract.EndTime = session.EndTime.ToString("s");
        sessionContract.Email = session.Email;
        sessionContract.Password = session.Password;
        var trialContractList = new List<Assets.DataContracts.Trial>();
        foreach (var trial in session.Trials)
        {
            var trialContract = new Assets.DataContracts.Trial();
            trialContract.StartTime = trial.StartTime.ToString("s");
            trialContract.EndTime = trial.EndTime.ToString("s");
            trialContract.Difficulty = trial.Difficulty;
            var objectiveContractList = new List<Assets.DataContracts.Objectives>();
            foreach (var objective in trial.Objectives)
            {
                var objectiveContract = objective.ToDataContract();
                
                objectiveContractList.Add(objectiveContract);
            }
            trialContract.Objectives = objectiveContractList.ToArray();
            trialContractList.Add(trialContract);
        }
        sessionContract.Trials = trialContractList.ToArray();

        return sessionContract;
    } 

}
