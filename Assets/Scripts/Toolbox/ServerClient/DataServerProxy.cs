using Assets.DataContracts;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace Assets.Toolbox
{
    public class DataServerProxy : AbstractDataServerClient
    {
        private string url = "http://localhost:3000/api/v1/sessions";

        public override void Send(GameSession session)
        {
            var json = JsonConvert.SerializeObject(session);
            var bytes = Encoding.UTF8.GetBytes(json);

            var postHeader = new Dictionary<string, string>();
            postHeader.Add("Content-Type", "application/json");

            var request = new WWW(url, bytes, postHeader);
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
