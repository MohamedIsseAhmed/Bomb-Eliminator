using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Shooting : MonoBehaviour, IAimAndShoot
{
    //[SerializeField] private Transform projectileSwanPosition;
    //[SerializeField] private Transform bulletOriganlPosition;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float shootingDistanceRange = 10;
    [SerializeField] private float timeAfterToActivatePooledObjects = 2f;
    [SerializeField] GunScriptableObject currentGun;
    private float timer;
    private float timerMax;

    private Transform targetEnemy;
    private Animator animator;
    private PlayerController playerController;

    [SerializeField] private Transform poolBulletParent;
    private List<Bullet> bullets;
    [SerializeField] private int bulletPoolCount;

    int shotsRemainingInBurst;
    private bool hasTarget;
    private void Awake()
    {
        timerMax = 0.25f;
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        bullets = new List<Bullet>();
        shootingDistanceRange = currentGun.burstCount;
    }
    private void Start()
    {
       CreateBulletPool();
    }
    private void CreateBulletPool()
    {
        for (int i = 0; i < bulletPoolCount; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, currentGun.projectileSpawnPostion.localPosition, Quaternion.identity, poolBulletParent);
            bullet.gameObject.SetActive(false);
            bullets.Add(bullet);
        }
    }
    void Update()
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
        AimAndShoot(targetEnemy);
        if (Input.GetMouseButtonUp(0))
        {
            shootingDistanceRange = currentGun.burstCount;
        }
    }
    private  void Shoot()
    {
        GetBullet();
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
            }
        }
        else
        {
             animator.SetTrigger("RifleDown");
        }
    }
    public Bullet GetBullet()
    {
        for (int i = 0; i < bulletPoolCount; i++)
        {
            if (!bullets[i].gameObject.activeInHierarchy)
            {
                bullets[i].gameObject.SetActive(true);
                bullets[i].transform.position = currentGun.projectileSpawnPostion.position;
                bullets[i].transform.rotation = Quaternion.identity;    
                bullets[i].SetDirection(currentGun.projectileSpawnPostion.forward);
                StartCoroutine(DeActivateProjectiles(bullets[i]));
                return bullets[i];
            }
        }
        return null;
    }
    IEnumerator DeActivateProjectiles(Bullet bullet)
    {
        yield return new WaitForSeconds(timeAfterToActivatePooledObjects);
        bullet.transform.position =currentGun.projectileSpawnPostion.position;
        bullet.gameObject.SetActive(false); 
    }
}
