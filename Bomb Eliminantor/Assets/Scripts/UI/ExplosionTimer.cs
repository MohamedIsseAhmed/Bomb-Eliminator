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
    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] private string secondsColorHex;

    private float explosionTime;
    private bool timeFilled;
    public static event EventHandler OnTimeOverEvent;
    private bool isPlayerEliminatedBomb;
    private void Start()
    {
        StartCoroutine(ExplosionCoroutine());
        BombVisual.OnFilledEvent += BombVisual_OnFilledEvent;
    }

    

    private void BombVisual_OnFilledEvent(object sender, EventArgs e)
    {
        isPlayerEliminatedBomb = true;
    }

    IEnumerator ExplosionCoroutine()
    {
        while (!timeFilled && !isPlayerEliminatedBomb)
        {
            if (GameManager.instance.GameOver)
            {
                yield break;
            }
            explosionTime += Time.deltaTime;
            explosionFillÝmage.fillAmount = explosionTime / explosionTimerMax;
            secondsText.text = (explosionTimerMax- explosionTime).ToString("F0");
            expolosionText.text = "EXPOLOSÝON ÝN " + secondsText.text + " S" ;
            if (explosionTime >= explosionTimerMax)
            {
             
                timeFilled=true;
                OnTimeOverEvent?.Invoke(this, EventArgs.Empty);
                yield break;
            }
            yield return null;
        }
    }
    private void OnDisable()
    {
        BombVisual.OnFilledEvent -= BombVisual_OnFilledEvent;
    }
}
