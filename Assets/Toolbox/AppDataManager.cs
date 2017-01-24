using UnityEngine;
using System.Collections.Generic;
using Assets.GameScripts;
using Assets.DataContracts;

namespace Assets.Toolbox
{
    public class AppDataManager : AbstractAppDataManager
    {
        private List<BodySnapshot> _bodySnapshots = new List<BodySnapshot>();
        private Toolbox toolbox;

        private void Start()
        {
            toolbox = gameObject.GetComponent<Toolbox>();
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var dataClient = toolbox.DataServerProxy;
                dataClient.SendAsFile(_bodySnapshots.ToArray());
                _bodySnapshots = new List<BodySnapshot>();
            }
        }

        public override void Save(BodySnapshot data)
        {
            _bodySnapshots.Add(data);

        }

    }

}