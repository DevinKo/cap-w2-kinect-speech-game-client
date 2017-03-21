using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Toolbox
{
    public class GameEventHub
    {
        public SpySceneEvents SpyScene = new SpySceneEvents();
        public CalibrationSceneEvents CalibrationScene = new CalibrationSceneEvents();

        public class SpySceneEvents
        {
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
        }
    }
}
