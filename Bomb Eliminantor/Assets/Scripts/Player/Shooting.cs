using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shooting : MonoBehaviour, IAimAndShoot
{
    public enum GunType
    {
        StandardGun,
        LazerGun
    }
    [SerializeField] private GunType gunType;
    [SerializeField] private float shootingDistanceRange = 10;
    [SerializeField] GunScriptableObject currentGun;
    private float timer;
    private float timerMax;

    private Transform targetEnemy;
    private Animator animator;
    private PlayerController playerController;

    int shotsRemainingInBurst;
    private bool hasTarget;
    private float timeBetWeenShots;
    private void Awake()
    {
        if (gunType == GunType.StandardGun)
        {
            timeBetWeenShots = 0.25f;
        }
        else if(gunType == GunType.LazerGun)
        {
            timeBetWeenShots = 0.15f;
        }
        timerMax = timeBetWeenShots;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
       
        shootingDistanceRange = currentGun.burstCount;
    }

    void Update()
    {
        
        AimAndShoot(targetEnemy);
        if (Input.GetMouseButtonUp(0))
        {
            shootingDistanceRange = currentGun.burstCount;
        }
    }
    private void Fire()
    {
        if (timer > timerMax)
        {
            timer = 0;
            if (currentGun.fireMode == GunScriptableObject.FireMode.Auto)
            {
                Shoot();
            }
            else if (currentGun.fireMode == GunScriptableObject.FireMode.Burst)
            {
                if (shootingDistanceRange == 0)
                {
                    return;
                }
                Shoot();
                shootingDistanceRange--;
            }

        }
        timer += Time.deltaTime;
    }
    private  void Shoot()
    {
       BulletPool.instance.GetBullet(currentGun);
    }
    public void AimAndShoot(Transform target)
    {
      
        if(target != null && !playerController.ÝsDraging)
        {
            targetEnemy= target;
            if (targetEnemy.GetComponent<HealthSystem>().ÝsDead())
            {
                animator.SetTrigger("RifleDown");
                return;
            }
            if (Vector3.Distance(transform.position, target.position) < shootingDistanceRange)
            {
                Vector3 directionToEnemy = target.transform.position - transform.position;
                transform.rotation = Quaternion.LookRotation(directionToEnemy);
                animator.SetTrigger("Shoot");
                Fire();
            }
        }
        else if(targetEnemy == null || playerController.ÝsDraging)
        {
            animator.SetTrigger("RifleDown");
        }
    }
   
  
}
