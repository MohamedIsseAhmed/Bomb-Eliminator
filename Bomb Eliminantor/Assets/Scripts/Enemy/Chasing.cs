using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chasing : EnemyState
{
    public Chasing(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent) : base(_npc, _animator, _player, _navMeshAgent)
    {
        stateName = State.Chasing;
        enemyType = EnemyType.Patroller;
    }
    public override void Enter()
    {
        animator.SetTrigger("Run");
        base.Enter();
    }
    public override void Update()
    {
        if (navMeshAgent.hasPath)
        {
            navMeshAgent.SetDestination(player.transform.position);
            if (CanShoot())
            {
                nextState = new Attacking(npc, animator, player, navMeshAgent);
                eventStages = EventStages.Exit;
            }
        }
        navMeshAgent.SetDestination(player.position);
    }
    public override void Exit()
    {
        //navMeshAgent.isStopped = true;
        animator.ResetTrigger("walk");
        base.Exit();
    }
}
