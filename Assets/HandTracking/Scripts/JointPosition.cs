using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Windows.Kinect;
using Assets.DataContracts;
using Assets.Toolbox;

public class JointPosition : MonoBehaviour
{
    public Windows.Kinect.JointType _jointType;
    public GameObject _bodySourceManager;
    private BodySourceManager _bodyManager;
    // A vector to be used to scale joint position vector.
    private Vector3 _scalar = new Vector3(10, 10, 1);

    private Toolbox toolbox;

    // Use this for initialization
    void Start()
    {
        toolbox = FindObjectOfType<Toolbox>();
        StartCoroutine("RecordSnapshot");
    }

    // Update is called once per frame
    void Update()
    {
        var body = GetFirstTrackedBody();
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

    public Body GetFirstTrackedBody()
    {
        if (_bodySourceManager == null)
        {
            return null;
        }

        _bodyManager = _bodySourceManager.GetComponent<BodySourceManager>();
        if (_bodyManager == null)
        {
            return null;
        }

        Body[] data = _bodyManager.GetData();
        if (data == null)
        {
            return null;
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
                return body;
            }
        }
        return null;
    }

    public IEnumerator RecordSnapshot()
    {
        while (true)
        {
            var body = GetFirstTrackedBody();
            if (body == null)
            {
                yield return null;
                continue;
            }
            var pos = body.Joints[_jointType].Position;
            var joints = new List<Assets.DataContracts.Joint>();
            joints.Add(new Assets.DataContracts.Joint()
            {
                JointType = _jointType.ToString(),
                X = pos.X,
                Y = pos.Y,
                Z = pos.Z
            });

            var snapshot = new BodySnapshot()
            {
                Joints = joints.ToArray(),
                Time = Time.time
            };

            toolbox.AppDataManager.Save(snapshot);
            yield return new WaitForSeconds(3);
        }
    }
}
