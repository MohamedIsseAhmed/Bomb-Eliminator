using System.Collections;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   public bool GameOver { get; set; }
   public bool CanShowBombVisual { get; private set; }
   public static event EventHandler OnGameOver;

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
    
    }
    private void OnDisable()
    {
        EnemyCounterOnScene.instance.OnAllEnemiesDied -= OnAllEnemiesDied;
       
    }
    private void OnAllEnemiesDied(object sender, System.EventArgs e)
    {
        CanShowBombVisual=true;
    }
}
