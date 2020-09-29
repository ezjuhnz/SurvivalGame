//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnermyState
{
    PATROL,
    CHASE,
    ATTACK
}
public class EnermyController : MonoBehaviour {

    private EnermyAnimator enermy_Animator;
    private NavMeshAgent navmesh_Agent;
    private EnermyState enermy_State;
    public float walk_Speed = 0.5f;
    public float chase_Speed = 4f;
    public float chase_Distance = 7f;
    private float current_Chase_Distance;

    public float attack_Distance = 1.8f;
    public float chase_After_Attack_Distance = 2f;
    public float patrol_Radius_Min = 20f, patrol_Radius_Max = 60f;
    public float patrol_For_This_Time = 15f;
    private float patrol_Timer;

    public float wait_Before_Attack = 2f;
    private float attack_Timer;
    private Transform target;

    public GameObject attack_Point;
    private float viewAngle = 60f;

    private EnermyAudio enermyAudio;
    private HealthScript healthScript;


    public EnermyState Enermy_State
    {
        get
        {
            return enermy_State;
        }

        set
        {
            enermy_State = value;
        }
    }

    void Awake()
    {
        enermy_Animator = GetComponent<EnermyAnimator>();
        navmesh_Agent = GetComponent<NavMeshAgent>();
        target = GameObject.FindWithTag(Tags.PLAYER_TAG).transform;

        enermyAudio = GetComponentInChildren<EnermyAudio>();
        
    }
	void Start () {
        Enermy_State = EnermyState.PATROL;
        patrol_Timer = patrol_For_This_Time;
        //attack the enermy first gets to the player
        attack_Timer = wait_Before_Attack;
        current_Chase_Distance = chase_Distance;
        healthScript = this.GetComponent<HealthScript>();
    }
	
	// Update is called once per frame
	void Update () {
        if(healthScript.health <= 0)
        {
            return;
        }
		if(Enermy_State == EnermyState.PATROL)
        {
            Patrol();
        }
        else if (Enermy_State == EnermyState.CHASE)
        {
            Chase();
        }
        else if (Enermy_State == EnermyState.ATTACK)
        {
            Attack();
        }
        
    }


    void Patrol()
    {
        // tell nav agent that he can move
        navmesh_Agent.isStopped = false;
        navmesh_Agent.speed = walk_Speed;

        // add to the patrol timer
        patrol_Timer += Time.deltaTime;

        if (patrol_Timer > patrol_For_This_Time)
        {

            SetNewRandomDestination();

            patrol_Timer = 0f;

        }

        if (navmesh_Agent.velocity.sqrMagnitude > 0)
        {
            enermy_Animator.Walk(true);
        }
        else
        {
            enermy_Animator.Walk(false);

        }

        // test the distance between the player and the enemy
        if (Vector3.Distance(transform.position, target.position) <= chase_Distance)
        {

            enermy_Animator.Walk(false);

            Enermy_State = EnermyState.CHASE;

            // play spotted audio
            enermyAudio.Play_ScreamSound();

        }


    } // patrol

    void Chase()
    {
        // enable the agent to move again
        navmesh_Agent.isStopped = false;
        navmesh_Agent.speed = chase_Speed;

        // set the player's position as the destination
        // because we are chasing(running towards) the player
        navmesh_Agent.SetDestination(target.position);

        if (navmesh_Agent.velocity.sqrMagnitude > 0)
        {
            enermy_Animator.Run(true);
        }
        else
        {
            enermy_Animator.Run(false);
        }

        // if the distance between enemy and player is less than attack distance
        if (Vector3.Distance(transform.position, target.position) <= attack_Distance)
        {

            // stop the animations
            enermy_Animator.Run(false);
            enermy_Animator.Walk(false);
            Enermy_State = EnermyState.ATTACK;
            attack_Timer = wait_Before_Attack;
            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }

        }
        else if (Vector3.Distance(transform.position, target.position) > chase_Distance)
        {
            // player run away from enemy

            // stop running
            enermy_Animator.Run(false);

            Enermy_State = EnermyState.PATROL;

            // reset the patrol timer so that the function
            // can calculate the new patrol destination right away
            patrol_Timer = patrol_For_This_Time;

            // reset the chase distance to previous
            if (chase_Distance != current_Chase_Distance)
            {
                chase_Distance = current_Chase_Distance;
            }


        } // else

    } // chase

    void Attack()
    {
        navmesh_Agent.velocity = Vector3.zero;
        navmesh_Agent.isStopped = true;

        attack_Timer += Time.deltaTime;

        if (attack_Timer > wait_Before_Attack)
        {
            //转向玩家
            Vector3 relativePos = target.position - transform.position;
            float angle = Vector3.Angle(relativePos, transform.forward);
            if(angle > viewAngle/2)
            {
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
                transform.rotation = rotation;
            }
            
            enermy_Animator.Attack();

            attack_Timer = 0f;

            // play attack sound
            enermyAudio.Play_AttackSound();

        }

        if (Vector3.Distance(transform.position, target.position) >
           attack_Distance + chase_After_Attack_Distance)
        {

            Enermy_State = EnermyState.CHASE;
        }


    } // attack

    void SetNewRandomDestination()
    {

        float rand_Radius = Random.Range(patrol_Radius_Min, patrol_Radius_Max);

        Vector3 randDir = Random.insideUnitSphere * rand_Radius;
        randDir += transform.position;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDir, out navHit, rand_Radius, -1);

        navmesh_Agent.SetDestination(navHit.position);

    }

    public void TurnOn_AttackPoint()
    {
        attack_Point.SetActive(true);
    }

    public void TurnOff_AttackPoint()
    {
        if (attack_Point.activeInHierarchy)
        {
            attack_Point.SetActive(true);
        }

    }

    

}
