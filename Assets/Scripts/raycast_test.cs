using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast_test : MonoBehaviour {

    public float pointing_zone_timer;

    Ray ray;
    RaycastHit hit;
    bool isComplete=false;
    float timeLeft;

    [SerializeField]
    protected KinectUIHandType _handType;
    protected KinectInputData _data;

    private Vector3 offset;

    // Use this for initialization
    void Start () {

         timeLeft = pointing_zone_timer;

        _data = KinectInputModule.instance.GetHandData(_handType);
        offset = new Vector3(681.5f, 296.5f);
    }
	
	// Update is called once per frame
	void Update () {
        ray = Camera.main.ScreenPointToRay(new Vector3(_data.GetHandScreenPosition().x, (2 * offset.y) - _data.GetHandScreenPosition().y, _data.GetHandScreenPosition().z));
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
