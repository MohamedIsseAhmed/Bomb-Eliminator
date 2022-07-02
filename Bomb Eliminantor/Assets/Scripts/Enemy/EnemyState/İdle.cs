using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class İdle : EnemyState
{
    public İdle(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent,Transform currentGun) : 
        base(_npc, _animator, _player, _navMeshAgent,currentGun)
    {
        stateName = State.İdle;
        enemyType = EnemyType.İdle;
    }
    public override void Enter()
    {
        animator.SetTrigger("Idle");
        base.Enter();
    }
    public override void Update()
    {
        //if (enemyType == EnemyType.İdle)
        //{
        //    if (CanSeePlayer())
        //    {
        //        Chasing chasing = new Chasing(npc, animator, player, navMeshAgent);
        //        nextState = chasing;
        //        eventStages = EventStages.Exit;


        //    }
        //}
        if (Random.Range(0,100) < 0.10f)
        {
            new Patrolling(npc, animator, player, navMeshAgent, currentGun);
        }
    
    }
}
