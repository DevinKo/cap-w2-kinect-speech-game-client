using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast_test : MonoBehaviour
{

    public float pointing_zone_timer;

    Ray ray;
    RaycastHit hit;

    Ray rightRay;
    RaycastHit rightHit;

    Ray leftRay;
    RaycastHit leftHit;

    bool isComplete = false;
    float timeLeft;
    bool isTouching = false;

    private KinectUIHandType _handType = KinectUIHandType.Right;
    protected KinectInputData _rightData;
    private KinectInputData _leftData;

    private Vector3 offset;

    // Use this for initialization
    void Start()
    {

        timeLeft = pointing_zone_timer;

        _rightData = KinectInputModule.instance.GetHandData(KinectUIHandType.Right);
        _leftData = KinectInputModule.instance.GetHandData(KinectUIHandType.Left);

        offset = new Vector3(681.5f, 296.5f);
    }

    // Update is called once per frame
    void Update()
    {
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

        bool leftTouch = false;
        bool rightTouch = false;

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

        isTouching = (leftTouch || rightTouch);

    }
}
