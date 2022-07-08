using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

   public bool ÝsGameEnded { get; private set; }
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
        EnemyCounterOnScene.instance.OnAllEnemiesDied += Ýnstance_OnAllEnemiesDied;
    }
    private void OnDisable()
    {
        EnemyCounterOnScene.instance.OnAllEnemiesDied -= Ýnstance_OnAllEnemiesDied;
    }
    private void Ýnstance_OnAllEnemiesDied(object sender, System.EventArgs e)
    {
        CanShowBombVisual=true;
    }
}
