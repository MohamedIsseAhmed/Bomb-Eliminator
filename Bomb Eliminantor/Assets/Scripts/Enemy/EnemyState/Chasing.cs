using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Chasing : EnemyState
{
    public Chasing(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent,GunScriptableObject currentGun) : 
        base(_npc, _animator, _player, _navMeshAgent, currentGun)
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
            WeaponPositionOnRunning();
            if (CanShoot())
            {
                nextState = new Attacking(npc, animator, player, navMeshAgent,currentGun);
                eventStages = EventStages.Exit;
            }
        }
        navMeshAgent.SetDestination(player.position);
    }
    public override void Exit()
    {
        animator.ResetTrigger("walk");
        base.Exit();
    }
    private void WeaponPositionOnRunning()
    {
        currentGun.gunPrfab.transform.localPosition = new Vector3(0.298000008f, 0.782000005f, 0.257999986f);
        currentGun.gunPrfab.transform.localEulerAngles = new Vector3(322.085358f, 70.1041412f, 352.622131f);
    }
}
