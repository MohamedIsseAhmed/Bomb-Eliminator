using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
  
    //[SerializeField] private List<Transform> paths;
    private EnemyState currentState;
    private Transform player;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public GameObject sphere;
    public  EnemyState.EnemyType enemyType;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        currentState = new Patrolling(gameObject, animator, player, navMeshAgent);
       
    }

    void Update() 
    {
        currentState = currentState.StateProcessor();
        //Vector3 f = transform.forward;
        //Vector3 d=player.position-transform.position;
        //Debug.DrawLine(transform.position,f* 10, Color.green);
        //Debug.DrawLine(transform.position,d* 100, Color.red);
        //sphere.transform.position = d;
        //print(Vector3.Angle(player.position, f));
        //print("distance:" + Vector3.Distance(player.position, transform.position));
    }
  

}
