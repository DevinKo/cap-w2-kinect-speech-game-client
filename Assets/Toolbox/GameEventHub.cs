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
                {
                    ZoneActivated(this, new EventArgs());
                }
            }
        }
    }
}
