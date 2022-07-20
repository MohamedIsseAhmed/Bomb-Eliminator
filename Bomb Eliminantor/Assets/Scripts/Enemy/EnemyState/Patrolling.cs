using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Threading.Tasks;

public class Patrolling : EnemyState
{
    public Patrolling(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent,Transform currentGun,PatrollingRunner patrollingRunner) :
        base(_npc, _animator, _player, _navMeshAgent, currentGun, patrollingRunner)
    {
       // enemyType = EnemyType.Patroller;
        stateName = State.Patrolling;
    }
   
   private int current›ndex;
   private Vector3 currentWaypoint = Vector3.zero;
   private int pathCount = 0;
   private float dotvisibleAngle=15f;
   private HealthSystem healthSystem;
    private bool ShoulchangeStateToChasing;
   public override void Enter()
    {
        ShoulchangeStateToChasing = false;
        animator.SetTrigger("walk");
        current›ndex = 0;
        base.Enter();
        currentWaypoint = patrollingRunner.GetPathsList()[current›ndex].position;
        pathCount = patrollingRunner.GetPathsList().Count;
        healthSystem=npc.GetComponent<HealthSystem>();
        healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
    }

    private void HealthSystem_OnTakeDamage(object sender, float e)
    {
        ShoulchangeStateToChasing = true;
    }

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
            if(dot >1 && direction.magnitude< dotvisibleAngle)
            {
                nextState = new Chasing(npc, animator, player, navMeshAgent, currentGun);
                eventStages = EventStages.Exit;    
            }           
            if (navMeshAgent.remainingDistance < 1)
            {
                if (current›ndex >= pathCount - 1)
                {
                    current›ndex = 0;
                    currentWaypoint = patrollingRunner.GetPathsList()[current›ndex].position;
                }
                else
                {
                    current›ndex++;
                    currentWaypoint = patrollingRunner.GetPathsList()[current›ndex].position;
                }

                navMeshAgent.SetDestination(currentWaypoint);
              
            }
            
        }
        else if(enemyType == EnemyType.›dle)
        {
            nextState = new ›dle(npc, animator, player, navMeshAgent, currentGun,patrollingRunner);
            eventStages = EventStages.Exit;
        }
        if (ShoulchangeStateToChasing)
        {
            nextState = new Attacking(npc, animator, player, navMeshAgent, currentGun);
            eventStages = EventStages.Exit;
        }
    }

    public override void Exit()
    {
        animator.ResetTrigger("walk");
        healthSystem.OnTakeDamage -= HealthSystem_OnTakeDamage;
        base.Exit();
    }
   
}
