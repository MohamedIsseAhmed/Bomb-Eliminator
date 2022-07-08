using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class ExplosionTimer : MonoBehaviour
{
    [SerializeField] private Image explosionFillÝmage;
    [SerializeField] private float explosionTimerMax = 3;
    
   
    [SerializeField] private TextMeshProUGUI expolosionText;

    private float explosionTime;
    private bool timeFilled;
    public static event EventHandler OnTimeOverEvent;
    private void Start()
    {
        StartCoroutine(ExplosionCoroutine());
    }
  
    IEnumerator ExplosionCoroutine()
    {
        while (!timeFilled)
        {
            explosionTime += Time.deltaTime;
            explosionFillÝmage.fillAmount = explosionTime / explosionTimerMax;
            expolosionText.text = "Explosion in 10 S" + explosionTime.ToString("F1");
            if (explosionTime >= explosionTimerMax)
            {
             
                timeFilled=true;
                OnTimeOverEvent?.Invoke(this, EventArgs.Empty);
                yield break;
            }
            yield return null;
        }
    }
 
}
