using Assets.Toolbox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{

    public float pointing_zone_timer;
    private Toolbox _toolbox;

    Ray ray;
    RaycastHit hit;

    Ray rightRay;
    RaycastHit rightHit;

    Ray leftRay;
    RaycastHit leftHit;

    bool isComplete = false;
    float timeLeft;
    bool isTouching = false;
    
    protected KinectInputData _rightData;
    private KinectInputData _leftData;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        timeLeft = pointing_zone_timer;

        _rightData = KinectInputModule.instance.GetHandData(KinectUIHandType.Right);
        _leftData = KinectInputModule.instance.GetHandData(KinectUIHandType.Left);

        offset = new Vector3(681.5f, 296.5f);
    }

    // Update is called once per frame
    void Update()
    {
        var audio = _toolbox.VolumeCollector.Decibel;
        //ray = Camera.main.ScreenPointToRay(new Vector3(_data.GetHandScreenPosition().x, (2 * offset.y) - _data.GetHandScreenPosition().y, _data.GetHandScreenPosition().z));

        checkTouching();

        if (isComplete) { }
        else if (isTouching)
        {
            print(pointing_zone_timer);
            pointing_zone_timer -= Time.deltaTime;

            if (pointing_zone_timer < 0)
            {
                print("COMPLETE");
                isComplete = true;
            }
        }
        else
        {
            pointing_zone_timer = timeLeft;
            isComplete = false;
        }

    }

    void checkTouching()
    {
        rightRay = Camera.main.ScreenPointToRay(new Vector3(_rightData.GetHandScreenPosition().x, (2 * offset.y) - _rightData.GetHandScreenPosition().y, _rightData.GetHandScreenPosition().z));
        leftRay = Camera.main.ScreenPointToRay(new Vector3(_leftData.GetHandScreenPosition().x, (2 * offset.y) - _leftData.GetHandScreenPosition().y, _leftData.GetHandScreenPosition().z));
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool leftTouch = false;
        bool rightTouch = false;
        bool mouseTouch = false;

        if (Physics.Raycast(rightRay, out rightHit))
        {
            if (rightHit.collider.tag == "zone_collider")
            {
                print("Right Hand on Target");
                rightTouch = true;
            }
        }
        if (Physics.Raycast(leftRay, out leftHit))
        {

            if (leftHit.collider.tag == "zone_collider")
            {
                print("Left Hand on Target");
                leftTouch = true;
            }
        }
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "zone_collider")
            {
                print("Mouse on Target");
                mouseTouch = true;
            }
        }

        isTouching = (leftTouch || rightTouch || mouseTouch);

    }

    public float getLastSound()
    {
        return _toolbox.VolumeCollector.RawEnergy;
    }
}
