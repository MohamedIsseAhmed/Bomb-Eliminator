using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class HealthBar : MonoBehaviour
{
    private Image fill›mage;
    [SerializeField] HealthSystem healthSystem; 
    private void Awake()
    {
        fill›mage=transform.Find("Parent").Find("fill›mage").GetComponent<Image>();
    }
    private void Start()
    {
        healthSystem.OnTakeDamage+= HealthSystem_OnTakeDamage;
    }

    private void HealthSystem_OnTakeDamage(object sender, EventArgs e)
    {
        
        float cutAmount = 0.1f;
        fill›mage.fillAmount -= cutAmount;
        print(cutAmount);
    }

   
    
}
