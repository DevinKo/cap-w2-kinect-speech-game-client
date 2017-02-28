using Assets.Toolbox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public bool _useLeftHand = false;
    public bool _useRightHand = false;
    public bool _useCursor = false;

    public float pointing_zone_timer;
    private Toolbox _toolbox;

    Ray ray;
    RaycastHit hit;

    Ray rightRay;
    RaycastHit rightHit;

    Ray leftRay;
    RaycastHit leftHit;

    public bool task1IsComplete = false;
    public bool task2IsComplete = false;
    float timeLeft;
    bool isTouching = false;
    
    protected KinectUICursor _UiHandLeft;
    protected KinectUICursor _UiHandRight;

    private Vector3 offset;

    public GameDataStorage.OBJECTIVE state;

    // Use this for initialization
    void Start()
    {
        state = GameDataStorage.OBJECTIVE.NONE;

        _toolbox = FindObjectOfType<Toolbox>();
        timeLeft = pointing_zone_timer;

        var handLeft = GameObject.FindGameObjectWithTag("UIHandLeft");
        _UiHandLeft = handLeft.GetComponent<KinectUICursor>();
         
        var handRight = GameObject.FindGameObjectWithTag("UIHandRight");
        _UiHandRight = handRight.GetComponent<KinectUICursor>();


        offset = new Vector3(681.5f, 296.5f);
    }

    // Update is called once per frame
    void Update()
    {
        var audio = _toolbox.VolumeSourceManager.Decibel;
        //ray = Camera.main.ScreenPointToRay(new Vector3(_data.GetHandScreenPosition().x, (2 * offset.y) - _data.GetHandScreenPosition().y, _data.GetHandScreenPosition().z));

        if (state == GameDataStorage.OBJECTIVE.LOCATE)
        {
            checkTouching();
            doTask1();
        }

        if (state == GameDataStorage.OBJECTIVE.DESCRIBE)
        {
            checkTouching2();
        }
        
        if (state == GameDataStorage.OBJECTIVE.NONE)
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
            // hit.collider.gameObject.GetComponent<zone_shader_modifier>().gotHit();

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

    public void setState(GameDataStorage.OBJECTIVE newState)
    {
        state = newState;
    }
}
