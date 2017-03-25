using Assets.Toolbox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// Use this class to obtain a reference to the game manager in the editor.
/// Since the GameManager is DontDestroyOnLoad, the a reference to a GameManager,
/// in the sence may be destroyed when the scene is loaded if a GameManager already
/// exists.
/// </summary>
public class GameManagerWrapper : MonoBehaviour
{
    private GameManager _gameManager;

    private void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
    }

    public void CompleteAndSendSession()
    {
        _gameManager.CompleteAndSendSession();
    }
}