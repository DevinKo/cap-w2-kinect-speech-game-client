using Assets.DataContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Toolbox
{
    public class DataServerProxy : AbstractDataServerClient
    {
        private string url = "http://localhost:81";

        public override bool Send(BodySnapshot[] data)
        {
            var json = JsonUtility.ToJson(new BodySnapshotsMessage()
            {
                Snapshots = data
            });
            var dataBytes = Encoding.UTF8.GetBytes(json);
            var request = new WWW(url, dataBytes);
            StartCoroutine("WaitAsync", request);
            Debug.Log(url + " " + json);
            return true;
        }


        public IEnumerator WaitAsync(WWW request)
        {
            yield return request;
        }
    }

}
