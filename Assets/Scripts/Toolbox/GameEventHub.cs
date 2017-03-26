using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Toolbox
{
    public class GameEventHub
    {
        public SpySceneEvents SpyScene = new SpySceneEvents();
        public CalibrationSceneEvents CalibrationScene = new CalibrationSceneEvents();
        public GameManagerEvents GameManager = new GameManagerEvents();

        public class GameManagerEvents
        {
            // Gets raised when a new trial is started. Should be called before loading a scene.
            public delegate void StartTrialEventHandler(object sender, EventArgs e);
            public event StartTrialEventHandler StartTrial;
            public void RaiseStartTrial()
            {
                if (StartTrial != null)
                    StartTrial(this, new EventArgs());
            }

            // Gets raised when session is complete.
            public delegate void SessionCompleteEventHandler(object sender, EventArgs e);
            public event SessionCompleteEventHandler SessionComplete;
            public void RaiseSessionComplete()
            {
                if (SessionComplete != null)
                    SessionComplete(this, new EventArgs());
            }
        }

        #region SpyScene
        public class SpySceneEvents
        {
            // Gets raised when cursor leaves zone. Can be raised many times in one scene
            public delegate void ZoneExitedEventHandler(object sender, EventArgs e);
            public event ZoneExitedEventHandler ZoneExited;
            public void RaiseZoneExited()
            {
                if (ZoneExited != null)
                    ZoneExited(this, new EventArgs());
            }

            // Gets raised when cursor enters zone. Can be raised many times in one scene
            public delegate void ZoneEnteredEventHandler(object sender, EventArgs e);
            public event ZoneEnteredEventHandler ZoneEntered;
            public void RaiseZoneEntered()
            {
                if (ZoneEntered != null)
                    ZoneEntered(this, new EventArgs());
            }

            // Gets raised when zone over clue is activated.
            public delegate void ZoneActivatedEventHandler(object sender, EventArgs e);
            public event ZoneActivatedEventHandler ZoneActivated;
            public void RaiseZoneActivated()
            {
                if (ZoneActivated != null)
                    ZoneActivated(this, new EventArgs());
            }

            // Gets raised when zone countdown is complete
            public delegate void ZoneCompleteEventHandler(object sender, EventArgs e);
            public event ZoneCompleteEventHandler ZoneComplete;
            public void OnZoneComplete()
            {
                if (ZoneComplete != null)
                    ZoneComplete(this, new EventArgs());
            }

            // Gets raised when Spy scene is loaded
            public delegate void LoadCompleteEventHandler(object sender, EventArgs e);
            public event LoadCompleteEventHandler LoadComplete;
            public void RaiseLoadComplete()
            {
                if (LoadComplete != null)
                    LoadComplete(this, new EventArgs());
            }

            // Gets raised when scene is complete.
            public delegate void CompletedEventHandler(object sender, EventArgs e);
            public event CompletedEventHandler Completed;
            public void RaiseCompleted()
            {
                if (Completed != null)
                    Completed(this, new EventArgs());
            }

            // Gets raised when Describe Scene is completed scene is loaded
            public delegate void DescribeCompleteEventHandler(object sender, EventArgs e);
            public event DescribeCompleteEventHandler DescribeComplete;
            public void RaiseDescribeComplete()
            {
                if (DescribeComplete != null)
                    DescribeComplete(this, new EventArgs());
            }

            // Gets raised when Clue is moved to infront of the Camera
            public delegate void ClueMovedEventHandler(object sender, EventArgs e);
            public event ClueMovedEventHandler ClueMoved;
            public void RaiseClueMoved()
            {
                if (ClueMoved != null)
                    ClueMoved(this, new EventArgs());
            }

            // Gets raised when Describe Scene is Loaded
            public delegate void DescribeLoadedEventHandler(object sender, EventArgs e);
            public event DescribeCompleteEventHandler DescribeLoaded;
            public void RaiseDescribeLoaded()
            {
                if (DescribeLoaded != null)
                    DescribeLoaded(this, new EventArgs());
            }

            // Gets raised when Describe Scene is Loaded
            public delegate void DescribingSizeEventHandler(object sender, EventArgs e, bool isDescribing);
            public event DescribingSizeEventHandler DescribingSize;
            public void RaiseDescribingSize(bool isDescribing)
            {
                if (DescribingSize != null)
                    DescribingSize(this, new EventArgs(), isDescribing);
            }
        }
        #endregion SpyScene

        #region Calibration
        public class CalibrationSceneEvents
        {
            // Gets raised  when Calibration scene ends
            public delegate void SceneEndEventHandler(object sender, EventArgs e);
            public event SceneEndEventHandler SceneEnd;
            public void RaiseSceneEnd()
            {
                if (SceneEnd != null)
                    SceneEnd(this, new EventArgs());
            }

            // Gets raised  when Calibration scene ends
            public delegate void LoadCompleteEventHandler(object sender, EventArgs e);
            public event LoadCompleteEventHandler LoadComplete;
            public void RaiseLoadComplete()
            {
                if (LoadComplete != null)
                    LoadComplete(this, new EventArgs());
            }

            // Gets raised  when radius is captured
            public delegate void RadiusCapturedEventHandler(object sender, EventArgs e, float radius);
            public event RadiusCapturedEventHandler RadiusCaptured;
            public void RaiseRadiusCaptured(float radius)
            {
                if (RadiusCaptured != null)
                    RadiusCaptured(this, new EventArgs(), radius);
            }

            // Gets raised  when radius is captured
            public delegate void AudioThresholdCapturedEventHandler(object sender, EventArgs e, float auiodThreshold);
            public event AudioThresholdCapturedEventHandler AudioThresholdCaptured;
            public void RaiseAudioThresholdCaptured(float audio)
            {
                if (AudioThresholdCaptured != null)
                    AudioThresholdCaptured(this, new EventArgs(), audio);
            }

            // Gets raised  when pointer zone duration is captured
            public delegate void PointerZoneDurationCapturedEventHandler(object sender, EventArgs e, float duration);
            public event PointerZoneDurationCapturedEventHandler PointerZoneDurationCaptured;
            public void RaisePointerZoneDurationCaptured(float duration)
            {
                if (PointerZoneDurationCaptured != null)
                    PointerZoneDurationCaptured(this, new EventArgs(), duration);
            }

            // Gets raised  when max reach is captured
            public delegate void MaxReachCapturedEventHandler(object sender, EventArgs e, Vector3 leftReach, Vector3 rightReach);
            public event MaxReachCapturedEventHandler MaxReachCaptured;
            public void RaiseMaxReachCaptured(Vector3 leftReach, Vector3 rightReach)
            {
                if (MaxReachCaptured != null)
                    MaxReachCaptured(this, new EventArgs(), leftReach, rightReach);
            }
        }
        #endregion Calibration
    }
}
