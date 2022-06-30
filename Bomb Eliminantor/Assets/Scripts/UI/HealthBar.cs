using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class HealthBar : MonoBehaviour
{
    private Image fillÝmage;
    [SerializeField] HealthSystem healthSystem; 
    private void Awake()
    {
        fillÝmage=transform.Find("Parent").Find("fillÝmage").GetComponent<Image>();
    }
    private void Start()
    {
        healthSystem.OnTakeDamage+= HealthSystem_OnTakeDamage;
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
       Destroy(gameObject);
    }

    private void HealthSystem_OnTakeDamage(object sender, EventArgs e)
    {
        
        float cutAmount = 0.1f;
        fillÝmage.fillAmount -= cutAmount;
        print(cutAmount);
    }
    private void OnDisable()
    {
        healthSystem.OnTakeDamage -= HealthSystem_OnTakeDamage;
        healthSystem.OnDead -= HealthSystem_OnDead;
    }


}
