using UnityEngine;
using System.Collections;

public class TestSceneManager : MonoBehaviour
{
    public delegate void EndAction();
    public static event EndAction OnSceneEnd;
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if(OnSceneEnd != null)
            {
                OnSceneEnd();
            }
        }
    }
}
