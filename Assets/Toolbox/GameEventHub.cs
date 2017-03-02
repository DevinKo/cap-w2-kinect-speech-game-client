using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Toolbox
{
    public class GameEventHub
    {
        public SpySceneEvents SpyScene = new SpySceneEvents();

        public class SpySceneEvents
        {
            // Gets raised when zone over clue is activated.
            public delegate void ZoneActivatedEventHandler(object sender, EventArgs e);
            public event ZoneActivatedEventHandler ZoneActivated;
            public void OnZoneActivated()
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
        }
    }
}
