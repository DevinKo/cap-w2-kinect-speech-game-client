using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast_mouse_cursor : MonoBehaviour {

    public float pointing_zone_timer;

    Ray ray;
    RaycastHit hit;
    bool isComplete = false;
    float timeLeft;

    // Use this for initialization
    void Start()
    {

        timeLeft = pointing_zone_timer;

    }

    // Update is called once per frame
    void Update()
    {

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (isComplete) { }
        else if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "zone_collider")
            {
                print(hit.collider.name);
                pointing_zone_timer -= Time.deltaTime;
                print(pointing_zone_timer);

                if (pointing_zone_timer < 0)
                {
                    print("COMPLETE");
                    isComplete = true;
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
