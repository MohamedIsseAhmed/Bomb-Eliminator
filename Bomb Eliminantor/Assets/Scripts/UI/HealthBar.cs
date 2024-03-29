using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
namespace GameUI
{
    public class HealthBar : MonoBehaviour
    {
        private Image fillÝmage;
        [SerializeField] private Image yellowImage;
        [SerializeField] HealthSystem healthSystem;
        [SerializeField] private TextMeshPro damageText;
        [SerializeField] private float yellowImageFillTime;
        private WaitForSeconds yellowImageFillWaitTime;
        private void Awake()
        {
            yellowImageFillWaitTime = new WaitForSeconds(yellowImageFillTime);
            fillÝmage = transform.Find("Parent").Find("fillÝmage").GetComponent<Image>();
            damageText.gameObject.SetActive(false);
        }
        private void Start()
        {
            healthSystem.OnTakeDamage += HealthSystem_OnTakeDamage;
            healthSystem.OnDead += HealthSystem_OnDead;
        }

        private void HealthSystem_OnDead(object sender, EventArgs e)
        {
            Destroy(gameObject);
        }

        private void HealthSystem_OnTakeDamage(object sender, float damnage)
        {
            damageText.gameObject.SetActive(true);
            float cutAmount = damnage / healthSystem.GetMaxHealth();
            fillÝmage.fillAmount -= cutAmount;
            StartCoroutine(YellowImageDecrease());
        }
        private void OnDisable()
        {
            healthSystem.OnTakeDamage -= HealthSystem_OnTakeDamage;
            healthSystem.OnDead -= HealthSystem_OnDead;
        }
        IEnumerator YellowImageDecrease()
        {

            yield return yellowImageFillWaitTime;
            yellowImage.fillAmount = Mathf.Lerp(fillÝmage.fillAmount, yellowImage.fillAmount, yellowImageFillTime * Time.deltaTime);
            damageText.gameObject.SetActive(false);
        }

    }
}

