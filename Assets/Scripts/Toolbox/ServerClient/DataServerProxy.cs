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
using Constants;

namespace Assets.Toolbox
{
    public class DataServerProxy : AbstractDataServerClient
    {
        private Toolbox _toolbox;
        private string url = "http://localhost:3000/api/v1/sessions";
        private float _timeOutTimer;

        public void Start()
        {
            _toolbox = FindObjectOfType<Toolbox>();
        }

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

        public override void Login(LoginRequest loginRequest)
        {
            if (string.IsNullOrEmpty(loginRequest.email) || string.IsNullOrEmpty(loginRequest.password))
            {
                _toolbox.EventHub.ServerEvents.RaiseLoginComplete(new LoginResponse()
                {
                    Response = ServerResponses.Unauthorized
                });
                return;
            }
            var json = JsonConvert.SerializeObject(loginRequest);
            var bytes = Encoding.UTF8.GetBytes(json);

            var postHeader = new Dictionary<string, string>();
            postHeader.Add("Content-Type", "application/json");

            var request = new WWW(url, bytes, postHeader);
            StartCoroutine("WaitLoginAsync", request);
            
        }

        public IEnumerator WaitAsync(WWW request)
        {
            yield return request;
        }

        public IEnumerator WaitLoginAsync(WWW request)
        {
            SetTimeout(30);
            while (!request.isDone)
            {
                if (CheckTimeout())
                {
                    break;
                }
                yield return null;
            }
            var response = new LoginResponse();

            var status = request.responseHeaders["STATUS"];

            if (status.Contains("401"))
            {
                response.Response = ServerResponses.Unauthorized;
            }
            _toolbox.EventHub.ServerEvents.RaiseLoginComplete(response);
            yield return request;
        }

        public void SetTimeout(float seconds)
        {
            _timeOutTimer = seconds;
        }

        public bool CheckTimeout()
        {
            _timeOutTimer -= Time.deltaTime;
            if (_timeOutTimer <= 0)
            {
                return true;
            }
            return false;
        }
    }

}
