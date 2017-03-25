using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Toolbox;
using System;

public class DescribeUiController : MonoBehaviour {
    public bool isLeft;

    private Toolbox _toolbox;

    private DescribeTarget dt;
    private bool setupComplete = false;
    private Vector2 DescribeScreenPosition = new Vector2(0, 0);
    private Image DescribeImage;

    // Use this for initialization
    void Start ()
    {
        // Make sure we dont block raycasts
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<CanvasGroup>().interactable = false;

        _toolbox = FindObjectOfType<Toolbox>();

        // Subscribe to events
        _toolbox.EventHub.SpyScene.ClueMoved += SetupDescribeUI;

        dt = GameObject.FindGameObjectWithTag("DescribeController").GetComponent<DescribeTarget>();

        DescribeImage = GetComponent<Image>();
        DescribeImage.enabled = false;
    }

    private void OnDestroy()
    {
        // Subscribe to events
        _toolbox.EventHub.SpyScene.ClueMoved -= SetupDescribeUI;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetHandPosition = dt.getDHandPosition(isLeft);

        RectTransform CanvasRect = transform.parent.gameObject.GetComponent<RectTransform>();

        Camera Cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        Vector2 ViewportPosition = Cam.WorldToViewportPoint(targetHandPosition);
        Vector2 DescribeScreenPosition = new Vector2(
        ((ViewportPosition.x * CanvasRect.sizeDelta.x) - (CanvasRect.sizeDelta.x * 0.5f)),
        ((ViewportPosition.y * CanvasRect.sizeDelta.y) - (CanvasRect.sizeDelta.y * 0.5f)));

        this.GetComponent<RectTransform>().anchoredPosition = DescribeScreenPosition;
    }

    public void SetupDescribeUI(object sender, EventArgs e)
    {
        DescribeImage.enabled = true;
    }
}
