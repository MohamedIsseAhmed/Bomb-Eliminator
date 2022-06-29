using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyState:MonoBehaviour
{
    protected enum State
    {
        Ýdle,
        Patrolling,
        Chasing,
        Attcking
    }
    public enum EventStages
    {
        Enter,
        Update,
        Exit
    }
    protected State stateName;
    protected EventStages eventStages;

    protected GameObject npc;
    protected Animator animator;
    protected Transform player;
    protected EnemyState nextState;
    protected NavMeshAgent navMeshAgent;
    protected Transform weapon;
    public enum EnemyType
    {
        Ýdle,
        Patroller
    }
    public EnemyType enemyType;

    protected float vissibleDistance=10;
    protected float visibleAngle = 30;
    protected float shootDistance = 10;

    public EnemyState(GameObject _npc, Animator _animator, Transform _player, NavMeshAgent _navMeshAgent, Transform weapon)
    {
        Debug.Log("state created");
        this.npc= _npc;
        this.animator= _animator;
        this.player= _player;
        this.navMeshAgent= _navMeshAgent;
        this.weapon= weapon;
    }
    public virtual void Enter()
    {
       eventStages=EventStages.Update;
    }
    public virtual void Update()
    {
        eventStages=EventStages.Update;
    }
    public virtual void Exit()
    {
        eventStages= EventStages.Exit;
    }
    protected  bool CanSeePlayer()
    {
        Vector3 direction=player.transform.position-npc.transform.position;
        float angle = Vector3.Angle(direction, npc.transform.forward); 
        if (direction.sqrMagnitude<vissibleDistance*vissibleDistance)
        {
            Debug.Log("enemy canSee player now");
            return true;
         
           
        }
        return false;
    }
    protected bool CanShoot()
    {
        Vector3 direction = player.transform.position - npc.transform.position;
        Quaternion lookDirection=Quaternion.LookRotation(direction);
        npc.transform.rotation = Quaternion.Slerp(npc.transform.rotation, lookDirection, 15 * Time.deltaTime);
        if (direction.sqrMagnitude < shootDistance * shootDistance)
        {
            return true;
        }
        return false;
    }
    public EnemyState StateProcessor()
    {
        if (eventStages == EventStages.Enter)
        {
            Enter();
        }
        if (eventStages == EventStages.Update)
        {
            Update();
        }
        if(eventStages == EventStages.Exit)
        {
            Exit();
            return nextState;
        }
       
        return this;
    }
}
