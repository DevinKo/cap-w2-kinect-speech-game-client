using UnityEngine;
using System.Collections;
using Windows.Kinect;

public class BodySourceManager : MonoBehaviour
{
    private KinectSensor _Sensor;
    private BodyFrameReader _Reader;
    private Body[] _Data = null;

    public Body[] GetData()
    {
        return _Data;
    }


    void Start()
    {
        _Sensor = KinectSensor.GetDefault();

        if (_Sensor != null)
        {
            _Reader = _Sensor.BodyFrameSource.OpenReader();

            if (!_Sensor.IsOpen)
            {
                _Sensor.Open();
            }
        }
    }

    void Update()
    {
        if (_Reader != null)
        {
            var frame = _Reader.AcquireLatestFrame();
            if (frame != null)
            {
                if (_Data == null)
                {
                    _Data = new Body[_Sensor.BodyFrameSource.BodyCount];
                }

                frame.GetAndRefreshBodyData(_Data);

                frame.Dispose();
                frame = null;
            }
        }
    }

    void OnApplicationQuit()
    {
        if (_Reader != null)
        {
            _Reader.Dispose();
            _Reader = null;
        }

        if (_Sensor != null)
        {
            if (_Sensor.IsOpen)
            {
                _Sensor.Close();
            }

            _Sensor = null;
        }
    }

    public Body GetFirstTrackedBody()
    {
        /*if (_bodySourceManager == null)
        {
            return null;
        }*/


        //_bodyManager = _bodySourceManager.GetComponent<BodySourceManager>();
        /*if (bodyManager == null)
        {
            return null;
        }*/

        Body[] data = GetData();
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

    /// <summary>
    /// Returns the position of joint relative to originJoint
    /// </summary>
    /// <param name="originJoint"></param>
    /// <param name="joint"></param>
    /// <returns></returns>
    public Vector3 GetRelativeJointPosition(JointType originJointType, JointType jointType)
    {
        var body = GetFirstTrackedBody();
        if(body == null)
        {
            return new Vector3();
        }
        var originJoint = body.Joints[originJointType];
        var joint = body.Joints[jointType];
        var vectorOrigin = new Vector3(originJoint.Position.X, originJoint.Position.Y, originJoint.Position.Z);
        var vector = new Vector3(joint.Position.X, joint.Position.Y, joint.Position.Z);
        return vector - vectorOrigin;
    }
}
