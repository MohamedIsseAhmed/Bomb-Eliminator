using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   public bool GameOver { get; private set; }
   public bool CanShowBombVisual { get; private set; }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);

        }
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        EnemyCounterOnScene.instance.OnAllEnemiesDied += OnAllEnemiesDied;
        ExplosionTimer.OnTimeOverEvent += ExplosionTimer_OnTimeOverEvent;
    }

    private void ExplosionTimer_OnTimeOverEvent(object sender, System.EventArgs e)
    {
        GameOver = true ;
    }

    private void OnDisable()
    {
        EnemyCounterOnScene.instance.OnAllEnemiesDied -= OnAllEnemiesDied;
        ExplosionTimer.OnTimeOverEvent -= ExplosionTimer_OnTimeOverEvent;
    }
    private void OnAllEnemiesDied(object sender, System.EventArgs e)
    {
        CanShowBombVisual=true;
    }
}
