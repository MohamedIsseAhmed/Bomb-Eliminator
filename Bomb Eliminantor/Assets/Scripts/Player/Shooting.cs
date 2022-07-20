using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Shooting : MonoBehaviour
{
   
    [SerializeField] private float burstCount;
    private GunScriptableObject currentGun;
    [SerializeField] private float sphereRadius;
    [SerializeField] private float shootingDistanceRange=10;
    [SerializeField] private LayerMask enemyLayerMaks;
    private float timer;
    private float timerMax;

    private Transform targetEnemy;
    private Animator animator;
    private PlayerController playerController;
    [SerializeField] private float timeBetWeenShots;
    private GunController gunController;

    private HealthSystem healthSystem;
    private int Obstaclelayer = 1 << 7;
    private int mayRayDistance = 10;
    private float timeBetweenShotsWithLaserGun = 0.15f;
    private float timeBetweenShotsWithNormalGun = 0.20f;
    public static event EventHandler EventHandlerOnPlayerDied;
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        gunController = GetComponent<GunController>();
        currentGun = gunController.CurrentGun;
        if (currentGun.weaponType== GunScriptableObject.WeaponType.NormalGun)
        {
            timeBetWeenShots = timeBetweenShotsWithNormalGun;
        }
        else if (currentGun.weaponType == GunScriptableObject.WeaponType.LaserGun)
        {
            timeBetWeenShots = timeBetweenShotsWithLaserGun;
        }
        timerMax = timeBetWeenShots;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
    }
 
    void Update()
    {
        AimAndShoot(targetEnemy);
    }
    private void OnEnable()
    {
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        targetEnemy = null;
        SoundManager.instance.PlaySound(SoundManager.Sound.GameOver);
        GetComponent<PlayerController>().enabled = false;
        enabled = false;
        GetComponent<GunController>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GameManager.instance.GameOver = true;
        EventHandlerOnPlayerDied?.Invoke(this, EventArgs.Empty);

    }

    private void Fire()
    {
        if (Time.time>timer)
        {

            timer = Time.time + timeBetWeenShots;
            GetTheBullet();
            SoundManager.instance.PlaySound(SoundManager.Sound.NormalBullet1);
           

        }
      
    }
   
    private  void GetTheBullet()
    {
       BulletPool.instance.GetBullet(gunController.ProjectileSpawnPosition);
    }
    public void AimAndShoot(Transform target)
    {

        if (target != null)
        {
            
            if (IsThereObstacle›nMyFront(target))
            {
                return;
            }
            else if (!playerController.›sDraging)
            {
                ShootTheEnemy(targetEnemy);
            }
            else if (targetEnemy == null || playerController.›sDraging)
            {
                animator.SetTrigger("RifleDown");
            }  
        }

    }
    private void ShootTheEnemy(Transform _targetEnemy)
    {
        targetEnemy = _targetEnemy;
        if (targetEnemy.GetComponent<HealthSystem>().›sDead())
        {
            animator.SetTrigger("RifleDown");
            return;
        }
        if (Vector3.Distance(transform.position, targetEnemy.position) < shootingDistanceRange)
        {
            Vector3 directionToEnemy = targetEnemy.transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(directionToEnemy);
            animator.SetTrigger("Shoot");

            Fire();

        }
    }
    private bool IsThereObstacle›nMyFront(Transform _targetEnemy)
    {
        targetEnemy=_targetEnemy;
        Ray ray = new Ray(transform.position + Vector3.up, (targetEnemy.position - transform.position));
        RaycastHit raycastHit;
        return  Physics.Raycast(ray, out raycastHit, mayRayDistance, Obstaclelayer);
    }
}
