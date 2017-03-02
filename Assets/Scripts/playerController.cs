using Assets.Toolbox;
using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private Cursor _cursor;

    public CursorTypes CursorType;

    public float pointing_zone_timer;
    private Toolbox _toolbox;
    
    public bool task1IsComplete = false;
    public bool task2IsComplete = false;
    private float timeLeft;
    
    public OBJECTIVE state;

    // Use this for initialization
    void Start()
    {
        _cursor = CursorFactory.Create(CursorType);

        state = OBJECTIVE.NONE;

        _toolbox = FindObjectOfType<Toolbox>();
        timeLeft = pointing_zone_timer;

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (_cursor.IsTouching("zone_collider", out hit))
        {
            checkTouching();
            doTask1();
        }

        if (state == OBJECTIVE.DESCRIBE)
        {
            checkTouching2();
        }

        if (state == OBJECTIVE.NONE)
        {
            this.gameObject.SetActive(false);
        }

    }

    void checkTouching()
    {
        rightRay = Camera.main.ScreenPointToRay(_UiHandRight.CurrentPosition);
        leftRay = Camera.main.ScreenPointToRay(_UiHandLeft.CurrentPosition);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool leftTouch = false;
        bool rightTouch = false;
        bool mouseTouch = false;

        if (_useRightHand)
        {
            if (Physics.Raycast(rightRay, out hit))
            {
                if (hit.collider.tag == "zone_collider")
                {
                    print("Right Hand on Target");
                    rightTouch = true;
                }
            }
        }
        if (_useLeftHand)
        {
            if (Physics.Raycast(leftRay, out hit))
            {

                if (hit.collider.tag == "zone_collider")
                {
                    print("Left Hand on Target");
                    leftTouch = true;
                }
            }
        }
        if (_useCursor)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "zone_collider")
                {
                    print("Mouse on Target");
                    mouseTouch = true;
                }
            }
        }
        isTouching = ((leftTouch && _useLeftHand && !_useRightHand)
            || (rightTouch && _useRightHand && !_useLeftHand)
            || (mouseTouch && _useCursor))
            || (leftTouch && rightTouch && _useLeftHand && _useRightHand);

    }

    void checkTouching2()
    {
        rightRay = Camera.main.ScreenPointToRay(_UiHandRight.CurrentPosition);
        leftRay = Camera.main.ScreenPointToRay(_UiHandLeft.CurrentPosition);

        bool leftTouch = false;
        bool rightTouch = false;

        if (_useRightHand)
        {
            if (Physics.Raycast(rightRay, out hit))
            {
                if (hit.collider.tag == "DescribeObject")
                {
                    if (hit.collider.name == "RightDescribeHand")
                    {
                        print("Right Hand on Target");
                        rightTouch = true;
                    }
                }
            }
        }
        if (_useLeftHand)
        {
            if (Physics.Raycast(leftRay, out hit))
            {

                if (hit.collider.tag == "DescribeObject")
                {
                    if (hit.collider.name == "LeftDescribeHand")
                    {
                        print("Right Hand on Target");
                        leftTouch = true;
                    }
                }
            }
        }

        task2IsComplete = (leftTouch && rightTouch);
    }

    void doTask1()
    {
        if (task1IsComplete)
        {

        }
        else if (isTouching)
        {
            hit.collider.gameObject.GetComponent<zone_shader_modifier>().gotHit();

            print(pointing_zone_timer);
            pointing_zone_timer -= Time.deltaTime;

            if (pointing_zone_timer < 0)
            {
                print("COMPLETE");
                task1IsComplete = true;
            }
        }
        else
        {
            pointing_zone_timer = timeLeft;
            task1IsComplete = false;
        }
    }

    public float getLastSound()
    {
        return _toolbox.VolumeSourceManager.RawEnergy;
    }

    public void setState(OBJECTIVE newState)
    {
        state = newState;
    }
}
