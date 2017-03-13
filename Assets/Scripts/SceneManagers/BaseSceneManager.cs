using Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BaseSceneManager : MonoBehaviour
{
    public static BaseSceneManager Instance { get { return _instance;  } }

    private static BaseSceneManager _instance;

    private Dictionary<GameObjectName, List<GameObject>> _gameObjects = new Dictionary<GameObjectName, List<GameObject>>();

    public void Awake()
    {
        _instance = this;
    }

    public IEnumerable<GameObject> GetObjectsWithName(GameObjectName name)
    {
        if (!_gameObjects.ContainsKey(name)) return new List<GameObject>();
        return _gameObjects[name];
    }

    public GameObject GetObjectWithName(GameObjectName name)
    {
        if (!_gameObjects.ContainsKey(name)) return null;
        return _gameObjects[name].FirstOrDefault();
    }

    public void AddGameObject(GameObjectName name, GameObject gameObject)
    {
        if (gameObject == null)
        {
            return;
        }

        if (_gameObjects.ContainsKey(name))
        {
            _gameObjects[name].Add(gameObject);
        }
        else
        {
            _gameObjects.Add(name, new List<GameObject>() { gameObject });
        }
    }
}
