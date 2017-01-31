using Assets.DataContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;

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

        public override void SendAsFile(BodySnapshot[] snapshots)
        {
            var file = new BodySnapshotJsonFile()
            {
                Snapshots = snapshots,
            };

            var json = JsonUtility.ToJson(file);
            var bytes = Encoding.UTF8.GetBytes(json);
            var form = new WWWForm();
            form.AddBinaryData("file", bytes, "fileName", "application/javascript");
            var request = new WWW(url, form);
            File.WriteAllBytes(@"C:\Users\barto\Documents\Snapshots.json", bytes);
            Debug.Log("Writing file");
            StartCoroutine("WaitAsync", request);

        }

        public IEnumerator WaitAsync(WWW request)
        {
            yield return request;
        }
    }

}
