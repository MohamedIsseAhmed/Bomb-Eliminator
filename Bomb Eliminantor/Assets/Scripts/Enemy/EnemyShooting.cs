using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
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

    private GunController gunController;
    private void Awake()
    {
        gunController = GetComponent<GunController>();  
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
          
        }
        
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
