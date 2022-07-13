using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shooting : MonoBehaviour
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
    [SerializeField] private float timeBetWeenShots;
    private GunController gunController;

    private HealthSystem healthSystem;
    private int Obstaclelayer = 1 << 7;
    private int mayRayDistance = 10;
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
        gunController = GetComponent<GunController>();
        if (gunType == GunType.StandardGun)
        {
            //timeBetWeenShots = 0.2f;
        }
        else if(gunType == GunType.LazerGun)
        {
            //timeBetWeenShots = 0.3f;
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
        if (Time.time>timer)
        {

            timer = Time.time + timeBetWeenShots;
            if (GetComponent<GunController>().CurrentGun.fireMode == GunScriptableObject.FireMode.Auto)
            {
                GetTheBullet();
                SoundManager.instance.PlaySound(SoundManager.Sound.NormalBullet1);
            }
            else if (GetComponent<GunController>().CurrentGun.fireMode == GunScriptableObject.FireMode.Burst)
            {
                if (shootingDistanceRange == 0)
                {
                    burstCount = currentGun.burstCount;
                }
                SoundManager.instance.PlaySound(SoundManager.Sound.NormalBullet1);
                GetTheBullet();

                shootingDistanceRange--;
            }

        }
        //timer += Time.deltaTime;
    }
    private  void GetTheBullet()
    {
       BulletPool.instance.GetBullet(currentGun.projectileSpawnPostion);
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
