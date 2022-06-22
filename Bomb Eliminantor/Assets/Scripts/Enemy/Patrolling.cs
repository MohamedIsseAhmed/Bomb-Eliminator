using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Threading.Tasks;

public class Patrolling : EnemyState
{
    public Patrolling(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent) : base(_npc, _animator, _player, _navMeshAgent)
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
                nextState = new Chasing(npc, animator, player, navMeshAgent);
                eventStages = EventStages.Exit;
            }
            Vector3 direction=player.transform.position-npc.transform.position;
            float dot=Vector3.Dot(npc.transform.forward,direction);
            if(dot >1 && direction.magnitude< 15)
            {
                nextState = new Chasing(npc, animator, player, navMeshAgent);
                eventStages = EventStages.Exit;
                
            }
            print(dot+","+direction.magnitude);
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

                /* TransformPoint(PatrollingRunner.instance.GetPayhs()[current›ndex].transform.position*/

                //if (Vector3.Distance(navMeshAgent.transform.position, currentWaypoint) < distanceThreshHold)
                //{
                //    waypoint›ndex = (waypoint›ndex + 1) % pathCount;
                //    currentWaypoint = PatrollingRunner.instance.GetPayhs()[current›ndex].transform.position;

                //}
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
