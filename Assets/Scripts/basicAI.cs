using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class basicAI : MonoBehaviour {

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State //izveido enumu ar stāvokļiem
        {
            WONDERING,// AI staigā apkārt starp waypointiem
            LOOKING, // AI izvēlas nākamo waypointu
            TROLLING // AI pārbīda stundu rādītāja laiku un izvēlas nākamo waypointu
        }

        public State state;
        private bool alive; // šis nosaka vai objekts eksistē... tas varētu būt arī nederīgs

        //variables Wondering
        
        public GameObject[] waypoints; //Satur waypointus kas ir pie pulksteņiem
        public GameObject[] clocks; //Satur visus pulksteņus
        int random = 4; // Skaitlis kas nosaka uz kuru waypoint iet
        public float patrolSpeed = 0.5f; // AI kustēšanās ātrums
        
        

      


        public
        void Start () {
            agent = GetComponent<NavMeshAgent>(); //izveido aģentu (NavMesh)
            character = GetComponent<ThirdPersonCharacter>(); //atrod AI ķermeni

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = basicAI.State.WONDERING;

            alive = true;

            StartCoroutine(FSM()); // uzstartē korotīnu
	    }
        IEnumerator FSM() // izveido korotīnu
        {
            while (alive) //Kamēr ir dzīvs izpilda šo
            {
                switch (state) 
                {
                    case State.WONDERING: 
                        Wonder();  //izsauc Wonder() funkciju
                        break;
                    case State.LOOKING:                   
                        Look(); //izsauc Look() funkciju
                        break;
                    case State.TROLLING:
                        Troll();    //izsauc Troll() funkciju
                        break;
                }
                yield return null;
            }
        }

        void Wonder()
        {
            agent.speed = patrolSpeed; //Uzstāda ātrumu kādā pārvietosies AI
            
            if (Vector3.Distance(this.transform.position, waypoints[random].transform.position) >= 1f) //novērtē vai distance starp AI un waypoiuntu ir >= 1
            {
                agent.SetDestination(waypoints[random].transform.position); //Uzstāda aģentam mērķi
                character.Move(agent.desiredVelocity, false, false); // Notiek AI kustēšanās uz mērķi
            }
            else
            {
               //Ja ir sasniedzis mērķi
                character.Move(Vector3.zero, false, false); //Aptur AI kustību
                int num = UnityEngine.Random.Range(1, 3); //nosaka num vērtību izmantojot random(50% iespēja)
                if (num == 1)
                {
                    state = basicAI.State.TROLLING; // maina state uz trolling
                }
                else state = basicAI.State.LOOKING; // maina state uz looking
                
            }
            
        }
        void Troll()
        {
            ClockAnimator clock = clocks[random].GetComponent<ClockAnimator>(); //Sameklē pūlksteni pie kura stāv izmantojot waypoint indexu
            clock.setRandomTroll(); // izsauc ClockAnimator metodi, kas uzstāda random ciparu, ko izmanto lai pārmainītu laiku uz nepareizu
            
            random = UnityEngine.Random.Range(0,waypoints.Length); //izvēlas jaunu waypoint
            state = basicAI.State.WONDERING; //maina state uz wondering
        }
        void Look()
            
        {
            
            random = UnityEngine.Random.Range(0,waypoints.Length);//izvēlas jaunu waypoint
            state = basicAI.State.WONDERING;//maina state uz wondering
        }
        
	
	
    }
}

