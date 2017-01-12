using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class JointPosition : MonoBehaviour 
{
    public Windows.Kinect.JointType _jointType;
    public GameObject _bodySourceManager;
    private BodySourceManager _bodyManager;
    // A vector to be used to scale joint position vector.
    private Vector3 _scalar = new Vector3(10, 10, 1);
	
    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (_bodySourceManager == null)
        {
            return;
        }

        _bodyManager = _bodySourceManager.GetComponent<BodySourceManager>();
        if (_bodyManager == null)
        {
            return;
        }

        Body[] data = _bodyManager.GetData();
        if (data == null)
        {
            return;
        }

        // get the first tracked body...
        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
               //this.gameObject.transform.position = new Vector3
               // this.gameObject.transform.localPosition =  body.Joints[_jointType].Position;
                var pos = body.Joints[_jointType].Position;
                var z = gameObject.transform.position.z;
               this.gameObject.transform.position = Vector3.Scale(new Vector3(pos.X, pos.Y, z), _scalar);
                break;
            }
        }
	}
}
