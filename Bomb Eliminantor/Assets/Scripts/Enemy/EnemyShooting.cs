using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour, IAimAndShoot
{
    [SerializeField] EnemyState.EnemyType enemyType;
    public enum EnemyShooterType
    {
        WithGun,
        WithSword
    }
    [SerializeField] private EnemyShooterType enemyShooterType;
    Enemy enemy;
    private Transform target;
    private float timer;
    private float timerMax;
    private void Awake()
    {
        timerMax = 1f;
        enemy = GetComponent<Enemy>();
    }
    void Start()
    {
        //Attacking attacking = new Attacking();
        //attacking.OnShootingStarted += Attacking_OnShootingStarted;
        enemy.OnShootingStarted += Enemy_OnShootingStarted1;
    }
    Attacking attacking;
    Transform targetPlayer;
    private void Enemy_OnShootingStarted1(object sender, EnemyState e)
    {
        attacking = e as Attacking;
        targetPlayer = attacking.GetPlayerTransform();
    }

    private void Update()
    {
        if (targetPlayer != null)
        {
            print("shoot the Player");
            Fire();
        }
    }

    private void Fire()
    {
        
        if (timer > timerMax)
        {
            timer = 0;
            Shoot();
            print(".................................shot");
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
        BulletPool.instance.GetBullet(GetComponent<GunController>().CurrentGun);
    }
    private void Attacking_OnShootingStarted(object sender, System.EventArgs e)
    {
        print("start firing Bullet");
        //Destroy(gameObject);
    }

    
    public void AimAndShoot(Transform target)
    {
        throw new System.NotImplementedException();
    }
}
