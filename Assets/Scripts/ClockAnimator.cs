using UnityEngine;
using System;
//Original author: Jasper Flick - https://unity3d.com/learn/tutorials/topics/scripting/simple-clock
//Updated by and downloaded from https://comphonia.com/unity-clock-animator
//Modified by Māris Kaufelds
public class ClockAnimator : MonoBehaviour
{

    private const float
        hoursToDegrees = -360f / 12f, // definē cik grādi ir 1h
        minutesToDegrees = -360f / 60f, // definē cik grādi ir 1min
        secondsToDegrees = -360f / 60f; // definē cik grādi ir 1sec

    public Transform hours, minutes, seconds; 
    public int gmt = 0; //Mainīgais laika zonu mainīšanai
    public int troll = 0; //Mainīgāis nepareizā laika uzstādīšanai
    public GameObject clockName; // pulksteņa nosaukums
    
    void Update()
    {
        DateTime time = DateTime.Now; // atrod patreizējo laiku
        hours.localRotation = Quaternion.Euler(0f, 270, (time.Hour-2+troll+gmt-6) * -hoursToDegrees); // uzstāda studas rādītāja rotāciju...  Tam atņem 2, jo Latvijai ir +2 UTC... Pieskaita laika zonu un troll mainīgo... Un atņem 6 lai kompensētu rotāciju, citādāk tas rāda nepareizu laiku
        minutes.localRotation = Quaternion.Euler(0f, 270, (time.Minute-30) * -minutesToDegrees); // uzstāda minūšu rādītāja rotāciju
        seconds.localRotation = Quaternion.Euler(0f, 270, (time.Second-30) * -secondsToDegrees); // uzstāda sekunžu rādītāja rotāciju
        if (troll > 0)
        {
            TextMesh tMesh = clockName.GetComponent<TextMesh>(); // atrod pulksteņa nosaukuma (textMesh) elementu 
            tMesh.color = Color.red;    //Nomaina texta krāsu uz sarkanu
        }
    }
    public void setRandomTroll() // metode kas uzstāda par cik sagrozīt laiku
    {
        troll = UnityEngine.Random.Range(1, 11); // atrod random skaitli no 1-10
    }
    public void resetTime()// metode laika resetošanai
    {
        TextMesh tMesh = clockName.GetComponent<TextMesh>();// atrod pulksteņa nosaukuma (textMesh) elementu 
        tMesh.color = Color.white;//Nomaina texta krāsu uz baltu
        troll = 0;
    }
}