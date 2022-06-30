using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Attacking : EnemyState,IAimAndShoot
{
  
    private Vector3 weaponOrigingPosition;
    private Enemy enemy;
    public event EventHandler OnShootingStarted;
    private float timer;
    private float timerMax;
    private int shootingDistanceRange;
    public Attacking(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent,GunScriptableObject currentGun) :
        base(_npc, _animator, _player, _navMeshAgent, currentGun)
    {
        stateName = EnemyState.State.Attcking;
        timer = 0.25f;
    }
    public override void Enter()
    {
        weaponOrigingPosition = currentGun.gunPrfab.transform.localPosition;
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
      
        if(Vector3.Distance(player.transform.position, navMeshAgent.transform.position) > 7.54f)
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
            OnShootingStarted?.Invoke(this, EventArgs.Empty);
            GunPositionOnShooting();
            navMeshAgent.isStopped = true;
            AimAndShoot(player);
           
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
        currentGun.gunPrfab.transform.localPosition = new Vector3(0.294999987f, 0.947000027f, 0.147f);
        currentGun.gunPrfab.transform.localEulerAngles =new Vector3(300.602997f, 75.4687881f, 348.521545f);
    }
    private void BringBackWeaponToOrigingPosition()
    {
        currentGun.gunPrfab.transform.localPosition = weaponOrigingPosition;
    }

    public void AimAndShoot(Transform target)
    {
       
    }
    public Transform GetPlayerTransform()
    {
        return player;
    }
    private void Fire()
    {
        GunPositionOnShooting();
        if (timer > timerMax)
        {
            timer = 0;
            Shoot();
            Debug.Log(".................................shot");
            //if (currentGun.fireMode == GunScriptableObject.FireMode.Auto)
            //{
            //    Shoot();
            //}
            //else if (currentGun.fireMode == GunScriptableObject.FireMode.Burst)
            //{
            //    if (shootingDistanceRange == 0)
            //    {
            //        return;
            //    }
            //    Shoot();
            //    shootingDistanceRange--;
            //}

        }
        timer += Time.deltaTime;
    }
    private void Shoot()
    {
        BulletPool.instance.GetBullet(currentGun);
    }
}
