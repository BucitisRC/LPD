using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixClock : MonoBehaviour {

    public Camera cam; //definē spēlētāja kameru
    public float range = 2f; //nosaka attālumu kādā var salabot pulksteņa laiku

	
	void Update () {
        

        if(Input.GetButtonDown("Fire1")) // nostrādā kad nospiesta peles kreisā poga
        {
            Cast();
        }
	}
    void Cast()
    {
        RaycastHit hit; // Izveido mainīgo kas satur informāciju par objektu, kuram trāpija Raycast
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))// ja Raycast trāpa  range attālumā
        {
            ClockAnimator target = hit.transform.GetComponent<ClockAnimator>(); //izveido ClockAnimator, kuram piesaista trāpītā objekta parametrus
            if (target != null)//ja ir target
            {
                target.resetTime();// izsauc ClockAnimator metodi resetTime(), kas izlabo laiku
            }
        }
    }
    
}
