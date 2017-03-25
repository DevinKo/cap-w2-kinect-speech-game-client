using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Assets.Toolbox;
using Constants;
using System;

public class DescribeTarget : MonoBehaviour
{
    Transform leftHand;
    Transform rightHand;

    private Toolbox _toolbox;

    private RectTransform leftUi, rightUi, dleftUi, drightUi;

    private bool isDescribing = false;
    private bool setupDone = false;

    // Use this for initialization
    void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();

        // Subscribe to events
        _toolbox.EventHub.SpyScene.ClueMoved += SetupDescribe;

        leftHand = transform.FindChild("LeftDescribeHand");
        rightHand = transform.FindChild("RightDescribeHand");

        leftUi = GameObject.FindGameObjectWithTag("UIHandLeft").GetComponent<RectTransform>();
        rightUi = GameObject.FindGameObjectWithTag("UIHandRight").GetComponent<RectTransform>();

        foreach (var x in GameObject.FindGameObjectsWithTag("DescribeUI"))
        {
            if (x.GetComponent<DescribeUiController>().isLeft)
                dleftUi = x.GetComponent<RectTransform>();
            else
                drightUi = x.GetComponent<RectTransform>();
        }
    }

    private void OnDestroy()
    {
        // Subscribe to events
        _toolbox.EventHub.SpyScene.ClueMoved -= SetupDescribe;
    }

    // Update is called once per frame
    void Update()
    {
        if (setupDone)
        {
            if(leftUi.anchoredPosition.x < dleftUi.anchoredPosition.x && 
                rightUi.anchoredPosition.x > drightUi.anchoredPosition.x && 
                !isDescribing)
            {
                _toolbox.EventHub.SpyScene.RaiseDescribingSize(true);
                isDescribing = true;
            }
            else if(leftUi.anchoredPosition.x > dleftUi.anchoredPosition.x &&
                rightUi.anchoredPosition.x < drightUi.anchoredPosition.x &&
                isDescribing)
            {
                _toolbox.EventHub.SpyScene.RaiseDescribingSize(false);
                isDescribing = false;
            }
        }
    }

    public void SetupDescribe(object sender, EventArgs e)
    {
        var clueObject = BaseSceneManager.Instance.GetObjectWithName(GameObjectName.Clue);

        var renderer = clueObject.GetComponent<MeshRenderer>();
        var extent = renderer.bounds.extents.x;
        var position = new Vector3(clueObject.transform.position.x + extent,
            clueObject.transform.position.y, clueObject.transform.position.z);

        leftHand.position = new Vector3(clueObject.transform.position.x + extent,
            clueObject.transform.position.y, clueObject.transform.position.z);
        BaseSceneManager.Instance.AddGameObject(GameObjectName.DescribeHandLeft, gameObject);

        rightHand.position = new Vector3(clueObject.transform.position.x - extent,
            clueObject.transform.position.y, clueObject.transform.position.z);
        BaseSceneManager.Instance.AddGameObject(GameObjectName.DescribeHandRight, gameObject);

        setupDone = true;
    }

    public Vector3 getDHandPosition(bool isLeft)
    {
        if (isLeft)
            return leftHand.position;
        else
            return rightHand.position;
    }
}
