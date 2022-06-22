using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : EnemyState
{
    public Attacking(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent) : base(_npc, _animator, _player, _navMeshAgent)
    {
        stateName = EnemyState.State.Attcking;
    }
    public override void Enter()
    {
        animator.SetTrigger("Run");
        navMeshAgent.isStopped = true;
        base.Enter();
    }
    string state;
    public override void Update()
    {
        Vector3 direction = player.transform.position - npc.transform.position;
        Quaternion lookDirection = Quaternion.LookRotation(direction);
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, lookDirection, 15 * Time.deltaTime);
      
        if(Vector3.Distance(player.transform.position, navMeshAgent.transform.position) > 5)
        {
            animator.ResetTrigger(state);
            state ="stop";
            navMeshAgent.SetDestination(player.transform.position);
            navMeshAgent.isStopped = false;
        }
        else
        {
            animator.ResetTrigger(state);
            state="Fire";
            navMeshAgent.isStopped = true;
        }
        animator.SetTrigger(state);
    }
    public override void Exit()
    {
        animator.ResetTrigger("Run");
        navMeshAgent.isStopped=false;
        base.Exit();
    }
}
