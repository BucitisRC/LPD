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

        public enum State
        {
            WONDERING,
            LOOKING,
            TROLLING
        }

        public State state;
        private bool alive;

        //variables Wondering
        
        public GameObject[] waypoints;
        public GameObject[] clocks;
        int random = 4;
        public float patrolSpeed = 0.5f;
        //variables Looking
        private int time;
        public GameObject target;

        //variables Trolling


        public
        void Start () {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            state = basicAI.State.WONDERING;

            alive = true;

            StartCoroutine(FSM());
	    }
        IEnumerator FSM()
        {
            while (alive)
            {
                switch (state)
                {
                    case State.WONDERING:    
                        Wonder();
                        break;
                    case State.LOOKING:                   
                        Look();
                        break;
                    case State.TROLLING:
                        Troll();
                        break;
                }
                yield return null;
            }
        }

        void Wonder()
        {
            agent.speed = patrolSpeed;
            
            if (Vector3.Distance(this.transform.position, waypoints[random].transform.position) >= 1f)
            {
                agent.SetDestination(waypoints[random].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else
            {
               
                character.Move(Vector3.zero, false, false);
                int num = UnityEngine.Random.Range(1, 3);
                if (num == 1)
                {
                    state = basicAI.State.TROLLING;
                }
                else state = basicAI.State.LOOKING;
                
            }
            
        }
        void Troll()
        {
            ClockAnimator clock = clocks[random].GetComponent<ClockAnimator>();
            clock.setRandomTroll();
            
            random = UnityEngine.Random.Range(0,waypoints.Length);
            state = basicAI.State.WONDERING;
        }
        void Look()
            
        {
            
            random = UnityEngine.Random.Range(0,waypoints.Length);
            state = basicAI.State.WONDERING;
        }
        
	
	
    }
}

