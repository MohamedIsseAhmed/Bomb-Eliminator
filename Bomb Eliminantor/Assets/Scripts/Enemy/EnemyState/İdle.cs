using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class İdle : EnemyState
{
    public İdle(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent, Transform currentGun, PatrollingRunner patrollingRunner) :
          base(_npc, _animator, _player, _navMeshAgent, currentGun, patrollingRunner)
    {
        stateName = State.İdle;
        enemyType = EnemyType.İdle;
        navMeshAgent.isStopped = true;
    }
   
    public override void Enter()
    {
        
        if (enemyType == EnemyType.İdle)
        {
            animator.SetTrigger("Idle");
        }
        animator.SetTrigger("Idle");
        base.Enter();
    }
    public override void Update()
    {
        if (enemyType == EnemyType.İdle)
        {
            if (CanSeePlayer())
            {
                nextState = new Chasing(npc, animator, player, navMeshAgent, currentGun);
                eventStages = EventStages.Exit;
            }
            Vector3 direction = player.transform.position - npc.transform.position;
            float dot = Vector3.Dot(npc.transform.forward, direction);
            if (dot > 1 && direction.magnitude < visibleAngle)
            {
                nextState = new Chasing(npc, animator, player, navMeshAgent, currentGun);
                eventStages = EventStages.Exit;
            }
        }
    }
    public override void Exit()
    {
        animator.ResetTrigger("Idle");
        base.Exit();
    }
}
