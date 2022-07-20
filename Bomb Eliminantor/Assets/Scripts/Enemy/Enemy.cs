using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class Enemy : MonoBehaviour
{

   
    [SerializeField] private CrossHair crossHair;
    [SerializeField] private LayerMask obstacle;
    [SerializeField] private GunScriptableObject gunScriptableObject;
    private EnemyState currentState;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public GameObject sphere;
    public  EnemyState.EnemyType enemyType;
    private HealthSystem healthSystem;
    private Transform weapon;
    private Transform currentGun;
  
    protected PatrollingRunner patrollingRunner;

    public event EventHandler<EnemyState> OnShootingStarted;
    private void Awake()
    {
        patrollingRunner =GetComponent<PatrollingRunner>();
        healthSystem = GetComponent<HealthSystem>();
    }
    void Start()
    {
        gunScriptableObject = GetComponent<GunController>().CurrentGun;
        currentGun = GetComponent<GunController>().CurrentWeapon;
        weapon = GetComponent<GunController>().CurrentWeapon;
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new Patrolling(gameObject, animator, player, navMeshAgent, currentGun, patrollingRunner);
        currentState.enemyType = enemyType;
        healthSystem.OnDead += HealthSystem_OnDead;

    }
    
    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<HealthSystem>().enabled = false;
    }
   
    private void OnDisable()
    {
        healthSystem.OnDead -= HealthSystem_OnDead;
    }
    void Update()
    {
        currentState = currentState.StateProcessor();
    }
    
    public void ActivateCrossHair(bool is›nShhotingRange)
    {
        crossHair.ShowCrossHair(is›nShhotingRange);
    }
}
