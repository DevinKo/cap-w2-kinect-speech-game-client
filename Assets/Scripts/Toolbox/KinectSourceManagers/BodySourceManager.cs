using UnityEngine;
using System.Collections;
using Windows.Kinect;
using Assets.Models;
using System;

public class BodySourceManager : BaseBodySourceManager, IBodySourceManager
{
    private KinectSensor _Sensor;
    private BodyFrameReader _Reader;
    private Windows.Kinect.Body[] _Data = null;

    public Windows.Kinect.Body[] GetData()
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
                    _Data = new Windows.Kinect.Body[_Sensor.BodyFrameSource.BodyCount];
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

    public override Assets.Models.Body GetFirstTrackedBody()
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

        Windows.Kinect.Body[] data = GetData();
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

    public override Assets.Models.Joint GetJoint(JointType jointType)
    {
        return GetFirstTrackedBody().Joints[jointType];
    }
}
