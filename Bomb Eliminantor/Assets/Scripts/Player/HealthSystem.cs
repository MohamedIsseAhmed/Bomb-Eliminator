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
    private float timeToStopForce=0.15f;

    public event EventHandler OnDead;
    public event EventHandler<float> OnTakeDamage;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        health = maxHealth;
    }
   
   
    public void TakeDamage(float damage)
    {
        health -= damage;
        animator.SetTrigger("TakeHit");
        OnTakeDamage?.Invoke(this, damage);
        if (ÝsDead())
        {
            OnDead?.Invoke(this,EventArgs.Empty);
            EnemyCounterOnScene.instance.RemoveEnemy(gameObject);
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
            rb.isKinematic = false;
            rb.constraints = RigidbodyConstraints.None;
            Vector3 forceVector = -transform.forward * 2;
            rb.AddForceAtPosition(forceVector * force, transform.position, ForceMode.VelocityChange);
            isAppliedForce = true;
        }
        yield return new WaitForSeconds(timeToStopForce);
        rb.isKinematic = true;
       
    }
    public float GetMaxHealth()
    {
        return maxHealth;
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
