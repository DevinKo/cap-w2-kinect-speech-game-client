using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;
using Constants;
/// <summary>
/// Abstract UI component class for hand cursor objects
/// </summary>
[RequireComponent(typeof(CanvasGroup), typeof(Image))]
public abstract class AbstractKinectUICursor : MonoBehaviour {

    [SerializeField]
    protected JointType _handType;
    protected Image _image;

    // A value for scaling kinect position to screen position
    public Vector3 _reachScalar;

    // holds the current tracking state for the hand
    public TrackingState TrackingState = TrackingState.NotTracked;

    public virtual void Awake()
    {

    }

    public virtual void Start()
    {
        Setup();
    }

    protected void Setup()
    {
        // Make sure we dont block raycasts
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<CanvasGroup>().interactable = false;
        // image component
        _image = GetComponent<Image>();

        // Add object to scene manager
        BaseSceneManager.Instance.AddGameObject(_handType == JointType.HandLeft ?
            GameObjectName.HandLeft : GameObjectName.HandRight, gameObject);
    }

    public virtual void Update()
    {
        ProcessData();
    }

    public abstract void ProcessData();
}
