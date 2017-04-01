using Assets.Toolbox;
using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AppAuth : MonoBehaviour
{
    private Toolbox _toolbox;

    public void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();

        _toolbox.EventHub.ServerEvents.LoginComplete += OnLoginComplete;
    }

    private void OnDestroy()
    {
        _toolbox.EventHub.ServerEvents.LoginComplete -= OnLoginComplete;
    }

    public string Password = "";
    public string Email = "";
    public bool IsLoggedIn = false;
    public bool IsGuest = true;
    public bool IsScaryMode = false;

    public void PlayAsGuest()
    {
        IsGuest = true;
        IsLoggedIn = false;
        IsScaryMode = false;

        Email = "p.mcpatientface@email.com";
        Password = "mFjDhCdzCw";
    }

    public void Logout()
    {
        IsLoggedIn = false;
        IsScaryMode = false;
        IsGuest = true;

        Email = "";
        Password = "";
        
        _toolbox.EventHub.ServerEvents.RaiseLogoutComplete();
    }

    public void Login(string username, string password)
    {
        Email = username;
        Password = password;

        var request = new LoginRequest(username, password);
        if(username.Equals("Count Dracula") && password.Equals("Spooky"))
        {
            IsLoggedIn = false;
            IsGuest = true;
            IsScaryMode = true;
            _toolbox.EventHub.ServerEvents.RaiseLoginComplete(new LoginResponse()
            {
                Response = ServerResponses.Unauthorized,
            });
        }
        _toolbox.DataServerProxy.Login(request);
    }

    public void OnLoginComplete(object sender, EventArgs e, LoginResponse response)
    {
        if (response.Response != ServerResponses.Unauthorized)
        {
            IsLoggedIn = true;
        }
    }
}
