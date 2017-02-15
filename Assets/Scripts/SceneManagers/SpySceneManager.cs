using UnityEngine;
using System.Collections;

public class SpySceneManager : MonoBehaviour
{
    // Event triggers and listeners
    public delegate void EndAction();
    public static event EndAction OnSceneEnd;
}