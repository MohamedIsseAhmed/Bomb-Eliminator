using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class HealthSystem : MonoBehaviour,IDamagable
{
    [SerializeField] private float maxHealth=100;
    private float health;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
        health = maxHealth;
    }
   
    public  event EventHandler OnDead;
    public  event EventHandler OnTakeDamage;
    public void TakeDamage(float damage)
    {
        
        health -= damage;
       print("health"+health);
        animator.SetTrigger("TakeHit");
        OnTakeDamage?.Invoke(this, EventArgs.Empty);
        if (ÝsDead())
        {
            OnDead?.Invoke(this,EventArgs.Empty);
            animator.SetTrigger("Die");
        }
    }
    public bool ÝsDead()
    {
        return health == 0;
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
