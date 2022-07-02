using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem : MonoBehaviour,IDamagable
{
    [SerializeField] private float maxHealth=100;
    [SerializeField] private float force=20;
    private float health;
    private Animator animator;
    private Rigidbody rb;
    private bool isAppliedForce=false;
    private float timeToStopForce=0.25f;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }
   
    public  event EventHandler OnDead;
    public  event EventHandler OnTakeDamage;
    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("TakeHit");
        OnTakeDamage?.Invoke(this, EventArgs.Empty);
        if (ÝsDead())
        {
            OnDead?.Invoke(this,EventArgs.Empty);
            animator.SetTrigger("Die");           
            enabled = false;
        }
    }
    public bool ÝsDead()
    {
        return health == 0;
    }
    //Animation Event
    private void ApplyForce()
    {
        StartCoroutine(ApplyForceOnDead());
    }
    private IEnumerator  ApplyForceOnDead()
    {
        if (!isAppliedForce)
        {
            Vector3 forceVector = -transform.forward * 2;
            rb.AddForceAtPosition(forceVector * force, transform.position, ForceMode.VelocityChange);
            isAppliedForce = true;
        }
        yield return new WaitForSeconds(timeToStopForce);
        rb.isKinematic = true;
       
    }

//    public class EventArgHealthAmount : EventArgs
//    {
//        private float currentHealth;
//        public EventArgHealthAmount(float _currentHealth)
//        {
//            currentHealth = _currentHealth;
//        }
//        public float GetHealth()
//        {
//            return currentHealth;
//        }

//    }
//}
}
