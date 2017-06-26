using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixClock : MonoBehaviour {

    public Camera cam;
    public float range = 2f;

	
	void Update () {
        

        if(Input.GetButtonDown("Fire1"))
        {
            Cast();
        }
	}
    void Cast()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            ClockAnimator target = hit.transform.GetComponent<ClockAnimator>();
            if (target != null)
            {
                target.resetTime();
            }
        }
    }
    
}
