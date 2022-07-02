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
    [SerializeField] private float burstCount;
    [SerializeField] GunScriptableObject currentGun;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float shootingDistanceRange=10;
    [SerializeField] private LayerMask enemyLayerMaks;
    private float timer;
    private float timerMax;

    private Transform targetEnemy;
    private Animator animator;
    private PlayerController playerController;

    int shotsRemainingInBurst;
    private bool hasTarget;
    private float timeBetWeenShots;
    private GunController gunController;

    private HealthSystem healthSystem;
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        gunController = GetComponent<GunController>();
        if (gunType == GunType.StandardGun)
        {
            timeBetWeenShots = 0.2f;
        }
        else if(gunType == GunType.LazerGun)
        {
            timeBetWeenShots = 0.3f;
        }
        timerMax = timeBetWeenShots;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();

        //shootingDistanceRange = GetComponent<GunController>().CurrentGun.burstCount;
        burstCount = currentGun.burstCount;
    }
 
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if(burstCount!=currentGun.burstCount)
               burstCount=currentGun.burstCount;
        }
        AimAndShoot(targetEnemy);
 
    }
    private void OnEnable()
    {
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        SoundManager.instance.PlaySound(SoundManager.Sound.GameOver);
    }

    private void Fire()
    {
        if (timer > timerMax)
        {
          
            timer = 0;
            if (GetComponent<GunController>().CurrentGun.fireMode == GunScriptableObject.FireMode.Auto)
            {
                Shoot();
                SoundManager.instance.PlaySound(SoundManager.Sound.NormalBullet1);
            }
            else if (GetComponent<GunController>().CurrentGun.fireMode == GunScriptableObject.FireMode.Burst)
            {
                if (shootingDistanceRange == 0)
                {
                    burstCount = currentGun.burstCount;
                }
                SoundManager.instance.PlaySound(SoundManager.Sound.NormalBullet1);
                Shoot();

                shootingDistanceRange--;
            }

        }
        timer += Time.deltaTime;
    }
    private  void Shoot()
    {
       BulletPool.instance.GetBullet(gunController.ProjectileSpawnPosition);
    }
    public void AimAndShoot(Transform target)
    {
       
        if (target != null && !playerController.ÝsDraging)
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
