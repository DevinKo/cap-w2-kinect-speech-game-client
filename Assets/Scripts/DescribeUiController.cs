using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Assets.Toolbox;

public class DescribeUiController : MonoBehaviour {
    [SerializeField]
    private DescribeTarget dt;
    private bool isLeft;

	// Use this for initialization
	void Start ()
    {
        // Make sure we dont block raycasts
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<CanvasGroup>().interactable = false;

        dt = GameObject.FindGameObjectWithTag("DescribeController").GetComponent<DescribeTarget>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = dt.getDHandPosition(isLeft);
	}
}
