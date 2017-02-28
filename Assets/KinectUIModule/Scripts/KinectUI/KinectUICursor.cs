using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Windows.Kinect;
using Assets.Toolbox;

public class KinectUICursor : AbstractKinectUICursor
{
    public Color normalColor = new Color(1f, 1f, 1f, 0.5f);
    public Color hoverColor = new Color(1f, 1f, 1f, 1f);
    public Color clickColor = new Color(1f, 1f, 1f, 1f);
    public Vector3 clickScale = new Vector3(.8f, .8f, .8f);

    private Vector3 _initScale;
    private Vector3 offset;
    private Toolbox _toolbox;
    private JointType _handJoint;
    private Canvas _UiCanvas;
    private RectTransform _UiCanvasRectTransform;
    // These values will be set by the calibration stage. Distance in 
    // meters from hand to shoulder spine joint.
    private float _maxReachX = 0.6f;
    private float _maxReachY = 0.5f;
    // A value for scaling kinect position to screen position
    private Vector3 _reachScalar;

    private Vector3 _currentPosition;
    public Vector3 CurrentPosition
    {
        get
        {
            return _currentPosition;
        }
    }

    public override void Start()
    {
        base.Start();
        _initScale = transform.localScale;
        //_image.color = new Color(1f, 1f, 1f, 0f);
        offset = new Vector3(681.5f, 296.5f);
        //offset = new Vector3(10, 10);
        _toolbox = FindObjectOfType<Toolbox>();
        _handJoint = _handType == KinectUIHandType.Right ? JointType.HandRight : JointType.HandLeft;

		var maxReach = _toolbox.AppDataManager.GetMaxReach(JointType.HandLeft);
		if (maxReach != null)
		{
			_maxReachX = maxReach.X;
			_maxReachY = maxReach.Y;

		}

        _UiCanvas = FindObjectOfType<Canvas>();
        _UiCanvasRectTransform = _UiCanvas.GetComponent<RectTransform>();
        _reachScalar = new Vector3(
            (_UiCanvasRectTransform.rect.width/2)/_maxReachX,
            (_UiCanvasRectTransform.rect.height/2)/_maxReachY,
            0);
    }

    public override void ProcessData()
    {
        if (_toolbox.BodySourceManager == null) return;

        // Get joints
        var body = _toolbox.BodySourceManager.GetFirstTrackedBody();
        if (body == null) return;
        var hand = body.Joints[_handJoint];
        var centerJoint = body.Joints[JointType.SpineShoulder];

        // calculate hand position relative to shoulder spine joint
        var distanceX = hand.Position.X - centerJoint.Position.X;
        var distanceY = hand.Position.Y - centerJoint.Position.Y;

        //Debug.Log(distanceX + " " + distanceY);

        // update pos
        _currentPosition = new Vector3(
            _UiCanvasRectTransform.rect.width / 2 + distanceX * _reachScalar.x,
            _UiCanvasRectTransform.rect.height / 2 + distanceY * _reachScalar.y,
            hand.Position.Z);
        transform.position = _currentPosition;
        /*
        if (_data.IsPressing)
        {
            _image.color = clickColor;
            _image.transform.localScale = clickScale;
            return;
        }
        if (_data.IsHovering)
        {
            _image.color = hoverColor;
        }
        else
        {
            _image.color = normalColor;
        }
        _image.transform.localScale = _initScale;*/
    }
}
