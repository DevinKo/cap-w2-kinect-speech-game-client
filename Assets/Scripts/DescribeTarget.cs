﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Toolbox;

public class DescribeTarget : MonoBehaviour
{
    Ray rightRay;
    Ray leftRay;

    RaycastHit hit;

    Transform leftHand;
    Transform rightHand;

    bool setup;
    bool isComplete;
    bool describeActive;

    public float shiftAmount = .01f;

    // Use this for initialization
    void Start()
    {
        leftHand = transform.FindChild("LeftDescribeHand");
        rightHand = transform.FindChild("RightDescribeHand");
        setup = false;
        isComplete = false;
        describeActive = false;
        rightRay = Camera.main.ScreenPointToRay(rightHand.position);
        leftRay = Camera.main.ScreenPointToRay(leftHand.position);
    }

    // Update is called once per frame
    void Update()
    {
        rightRay = Camera.main.ScreenPointToRay(rightHand.position);
        leftRay = Camera.main.ScreenPointToRay(leftHand.position);
        if (setup)
        {
            if (isComplete)
            {
                setup = false;
            }
            else
            {
                bool leftTouch = false;
                bool rightTouch = false;
                if (Physics.Raycast(rightRay, out hit))
                {
                    if (hit.collider.tag == "pointing_object")
                    {
                        rightTouch = true;
                    }
                }

                if (Physics.Raycast(leftRay, out hit))
                {
                    if (hit.collider.tag == "pointing_object")
                    {
                        leftTouch = true;
                    }
                }

                if (rightTouch)
                {
                    rightHand.position = new Vector3(rightHand.position.x - shiftAmount, rightHand.position.y, rightHand.position.z);
                }

                if (leftTouch)
                {
                    leftHand.position = new Vector3(leftHand.position.x + shiftAmount, leftHand.position.y, leftHand.position.z);
                }

                if (!rightTouch && !leftTouch)
                {
                    isComplete = true;
                    describeActive = true;
                }
            }
        }
    }

    public void setupDescribe()
    {
        setup = true;
    }

    public Vector3 getDHandPosition(bool isLeft)
    {
        if (isLeft)
            return leftHand.position;
        else
            return rightHand.position;
    }
}
