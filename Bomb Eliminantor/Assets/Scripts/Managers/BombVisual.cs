using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombVisual : MonoBehaviour
{
    [SerializeField] private Image fill›mage;
    [SerializeField] private float timer;
    [SerializeField] private float timerMax;
    public float Timer { get { return timer; } set { timer = value; } }
    [SerializeField] private bool isFilled = false;
    public bool IsFilled { get { return isFilled; } set { isFilled = value; } }

    [SerializeField] private GameObject winParticlePrefab;
    [SerializeField] private Transform winParticleSpawnPosition;
    [SerializeField] private float waitTimeToDestroyParticle=2;
    public static event EventHandler OnFilledEvent;
    void Update()
    {
        
        if (isFilled) return;
        timer += Time.deltaTime;
        if (timer > timerMax )
        {
            timer = timerMax;
           isFilled = true;
            OnFilledEvent?.Invoke(this, EventArgs.Empty);
            StartCoroutine(CreateWinParticle());
        }
        
        fill›mage.fillAmount = timer / timerMax;

    }
    private IEnumerator CreateWinParticle()
    {
        Transform newParticle = Instantiate(winParticlePrefab.transform, winParticleSpawnPosition.position, Quaternion.identity);
        
        yield return new WaitForSeconds(waitTimeToDestroyParticle);
       
       // Destroy(newParticle.gameObject);
    }
}
