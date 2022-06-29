using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attacking : EnemyState
{
  
    private Vector3 weaponOrigingPosition;
    private Enemy enemy;
    public Attacking(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent,Transform weapon) : base(_npc, _animator, _player, _navMeshAgent,weapon)
    {
        stateName = EnemyState.State.Attcking;
      
    }
    public override void Enter()
    {
        weaponOrigingPosition = weapon.localPosition;
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
            BringBackWeaponToOrigingPosition();
        }
        else
        {
            animator.ResetTrigger(state);
            state="Fire";
            GunPositionOnShooting();
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
    private void GunPositionOnShooting()
    {
        weapon.localPosition = new Vector3(0.294999987f, 0.947000027f, 0.147f);
        weapon.localEulerAngles =new Vector3(300.602997f, 75.4687881f, 348.521545f);
    }
    private void BringBackWeaponToOrigingPosition()
    {
        weapon.localPosition = weaponOrigingPosition;
    }
   
}
