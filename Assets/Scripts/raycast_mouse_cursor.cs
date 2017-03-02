using Assets.Toolbox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast_mouse_cursor : MonoBehaviour {

    private Toolbox _toolbox;

    public float pointing_zone_timer;

    Ray ray;
    RaycastHit hit;
    public bool isComplete = false;
    public bool isFound = false;
    float timeLeft;

    // Use this for initialization
    void Start()
    {
        _toolbox = FindObjectOfType<Toolbox>();
        timeLeft = pointing_zone_timer;

    }

    // Update is called once per frame
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (isComplete) {
            hit.collider.gameObject.GetComponent<zone_shader_modifier>().moveParent();
        }
        else if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "zone_collider")
            {
                if (!isFound)
                {
                    hit.collider.gameObject.GetComponent<zone_shader_modifier>().gotHit();
                    isFound = true;
                    _toolbox.EventHub.SpyScene.OnZoneActivated();
                }
                
                pointing_zone_timer -= Time.deltaTime;

                if (pointing_zone_timer < 0)
                {
                    print("COMPLETE");
                    isComplete = true;
                    _toolbox.EventHub.SpyScene.OnZoneComplete();
                }
            }
        }
        else
        {
            pointing_zone_timer = timeLeft;
            isComplete = false;
        }

    }
}
