using UnityEngine;
using System;
//Original author: Jasper Flick - https://unity3d.com/learn/tutorials/topics/scripting/simple-clock
//Updated by and downloaded from https://comphonia.com/unity-clock-animator
//Modified by Māris Kaufelds
public class ClockAnimator : MonoBehaviour
{

    private const float
        hoursToDegrees = -360f / 12f,
        minutesToDegrees = -360f / 60f,
        secondsToDegrees = -360f / 60f;

    public Transform hours, minutes, seconds;
    public int gmt = 0;
    public int troll = 0;
    public GameObject clockName;
    
    void Update()
    {
        DateTime time = DateTime.Now;
        hours.localRotation = Quaternion.Euler(0f, 270, (time.Hour-2+troll+gmt-6) * -hoursToDegrees);
        minutes.localRotation = Quaternion.Euler(0f, 270, (time.Minute-30) * -minutesToDegrees);
        seconds.localRotation = Quaternion.Euler(0f, 270, (time.Second-30) * -secondsToDegrees);
        if (troll > 0)
        {
            TextMesh tMesh = clockName.GetComponent<TextMesh>();
            tMesh.color = Color.red;
        }
    }
    public void setRandomTroll()
    {
        troll = UnityEngine.Random.Range(1, 11);
    }
    public void resetTime()
    {
        TextMesh tMesh = clockName.GetComponent<TextMesh>();
        tMesh.color = Color.white;
        troll = 0;
    }
}