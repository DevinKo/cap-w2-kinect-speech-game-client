using Assets.Toolbox;
using Constants;
using GameMenuKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class GameMenuSceneManager : MonoBehaviour
{
    public InputField username;
    public InputField password;
    public Text LoginErrorText;
    public Button LogoutButton;

    private PanelsManager _panelManger;
    private GMKManager _gmkManager;

    private Toolbox _toolbox;

    // Use this for initialization
    void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        _gmkManager = FindObjectOfType<GMKManager>();
        _panelManger = FindObjectOfType<PanelsManager>();

        _toolbox.EventHub.ServerEvents.LogoutComplete += OnLogOutComplete;
        _toolbox.EventHub.ServerEvents.LoginComplete += OnLoginComplete;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        _toolbox.EventHub.ServerEvents.LogoutComplete -= OnLogOutComplete;
        _toolbox.EventHub.ServerEvents.LoginComplete -= OnLoginComplete;
    }

    public void PressStart()
    {
        if (_toolbox.AppAuth.IsLoggedIn)
        {
            _gmkManager.LoadScene("Calibration");
        }
        else
        {
            _panelManger.OpenPanelByName("Login");
        }
    }

    public void Logout()
    {
        _toolbox.AppAuth.Logout();
    }

    public void PlayAsGuest()
    {
        _toolbox.AppAuth.PlayAsGuest();

        _gmkManager.LoadScene("Calibration");
    }

    public void Login()
    {
        _toolbox.AppAuth.Login(username.text, password.text);
    }

    public void OnLoginComplete(object sender, EventArgs e, LoginResponse response)
    {
        if (response.Response != ServerResponses.Unauthorized)
        {
            LogoutButton.enabled = true;
            LoginErrorText.enabled = false;
            _gmkManager.LoadScene("Calibration");
        }
        else if(_toolbox.AppAuth.IsGuest && _toolbox.AppAuth.IsScaryMode)
        {
            _gmkManager.LoadScene("SchoolAbandoned");
        }
        else
        {
            // show error
            LoginErrorText.enabled = true;
        }
    }

    private void OnLogOutComplete(object sender, EventArgs e)
    {
        LogoutButton.enabled = false;
    }
}
