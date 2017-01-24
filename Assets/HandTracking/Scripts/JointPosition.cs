using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Assets.DataContracts;
using Assets.Toolbox;

public class JointPosition : MonoBehaviour
{
    public Windows.Kinect.JointType _jointType;
    //public GameObject _bodySourceManager;
    private BodySourceManager _bodyManager;
    // A vector to be used to scale joint position vector.
    private Vector3 _scalar = new Vector3(10, 10, 1);

    private Toolbox _toolbox;

    // Use this for initialization
    void Start()
    {
        //toolbox = FindObjectOfType<Toolbox>();
        _toolbox = FindObjectOfType<Toolbox>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_toolbox.BodySourceManager == null)
        {
            return;
        }
        var body = _toolbox.BodySourceManager.GetFirstTrackedBody();
        if (body == null)
        {
            return;
        }
        //this.gameObject.transform.position = new Vector3
        // this.gameObject.transform.localPosition =  body.Joints[_jointType].Position;
        var pos = body.Joints[_jointType].Position;
        var z = gameObject.transform.position.z;
        this.gameObject.transform.position = Vector3.Scale(new Vector3(pos.X, pos.Y, z), _scalar);
        
    }
    
}
