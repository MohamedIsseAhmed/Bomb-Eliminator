using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Threading.Tasks;

public class Patrolling : EnemyState
{
    public Patrolling(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent,GunScriptableObject currentGun) :
        base(_npc, _animator, _player, _navMeshAgent, currentGun)
    {
        enemyType = EnemyType.Patroller;
        stateName = State.Patrolling;

    }
    Vector3[] position;
    int current›ndex;
    Vector3 currentWaypoint = Vector3.zero;
    int pathCount = 0;
    public override void Enter()
    {
        //GameObject[] paths = GameObject.FindGameObjectsWithTag("path");
        //position=new Vector3[paths.Length]; 
        //for (int i = 0; i < paths.Length; i++)
        //{
        //    position[i]=paths[i].transform.position;
        //}
        animator.SetTrigger("walk");
        current›ndex = 0;
        base.Enter();
        currentWaypoint = PatrollingRunner.instance.GetPayhs()[current›ndex].transform.position;
        pathCount = PatrollingRunner.instance.GetPayhs().Count;
    }
   
    
    int waypoint›ndex = 0;
    float speed = 10f;
    float distanceThreshHold = 1f;
    public override void Update()
    {
        if (enemyType == EnemyType.Patroller)
        {
            if (CanSeePlayer())
            {
                nextState = new Chasing(npc, animator, player, navMeshAgent, currentGun);
                eventStages = EventStages.Exit;
            }
            Vector3 direction=player.transform.position-npc.transform.position;
            float dot=Vector3.Dot(npc.transform.forward,direction);
            if(dot >1 && direction.magnitude< 15)
            {
                nextState = new Chasing(npc, animator, player, navMeshAgent, currentGun);
                eventStages = EventStages.Exit;
                
            }
            
            if (navMeshAgent.remainingDistance < 1)
            {


                if (current›ndex >= pathCount - 1)
                {
                    current›ndex = 0;
                    currentWaypoint = PatrollingRunner.instance.GetPayhs()[current›ndex].transform.position;
                }
                else
                {
                    current›ndex++;
                    currentWaypoint = PatrollingRunner.instance.GetPayhs()[current›ndex].transform.position;
                }

                navMeshAgent.SetDestination(currentWaypoint);
              
            }
        }

    }
    public override void Exit()
    {
        animator.ResetTrigger("walk");
        base.Exit();
    }
   
}
