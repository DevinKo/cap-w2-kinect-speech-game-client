using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast_mouse_cursor : MonoBehaviour {

    public float pointing_zone_timer;

    Ray ray;
    RaycastHit hit;
    public bool isComplete = false;
    public bool isFound = false;
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
                if (!isFound)
                {
                    var listOfZones = GameObject.FindGameObjectsWithTag("zone_collider");
                    for (int i = 0; i < listOfZones.Length; i++)
                    {
                        listOfZones[i].GetComponent<MeshRenderer>().enabled = true;
                    }
                    isFound = true;
                }
                
                pointing_zone_timer -= Time.deltaTime;

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
