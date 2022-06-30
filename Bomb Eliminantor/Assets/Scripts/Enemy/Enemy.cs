using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Enemy : MonoBehaviour
{

    //[SerializeField] private List<Transform> paths;
    [SerializeField] private GunScriptableObject gunScriptableObject;
    private EnemyState currentState;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public GameObject sphere;
    public  EnemyState.EnemyType enemyType;
    private HealthSystem healthSystem;
    private Transform weapon;

    public event EventHandler<EnemyState> OnShootingStarted;
    private void Awake()
    {
        healthSystem = GetComponent<HealthSystem>();
    }
    void Start()
    {
       // gunScriptableObject = GetComponent<GunController>().CurrentGun;
        weapon = GetComponent<GunController>().CurrentWeapon;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new Patrolling(gameObject, animator, player, navMeshAgent, gunScriptableObject);
        healthSystem.OnDead += HealthSystem_OnDead;
        healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
      
    }

    private void HealthSystem_OnTakeDamage(object sender, EventArgs e)
    {
       GeAttackStatete();
    }

    private void Attacking_OnShootingStarted(object sender, System.EventArgs e)
    {
        print("Enemey start"+gameObject.name);
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        
        enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<HealthSystem>().enabled = false;
       
 
    }
    Attacking attacking1;
    private void OnDisable()
    {
        healthSystem.OnDead -= HealthSystem_OnDead;
        //attacking1.OnShootingStarted -= Attacking_OnShootingStarted;
    }
    void Update() 
    {
        currentState = currentState.StateProcessor();
        
    }
    public Transform GetWeapon()
    {
        return weapon;
    }
    public EnemyState GeAttackStatete()
    {
        Attacking attacking=currentState.StateProcessor() as Attacking;
        attacking1 = attacking;
        attacking.OnShootingStarted += Attacking_OnShootingStarted;
        OnShootingStarted?.Invoke(this,attacking);
        return currentState;
    }
}
