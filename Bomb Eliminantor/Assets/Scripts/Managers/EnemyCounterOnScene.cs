using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class EnemyCounterOnScene : MonoBehaviour
{
    public static EnemyCounterOnScene instance;
    [SerializeField] private List<GameObject> enemiesOnScen;
    public event EventHandler OnAllEnemiesDied;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
      GameObject[] enemies =GameObject.FindGameObjectsWithTag("Enemy");

      foreach (GameObject enemy in enemies)
      {
            enemiesOnScen.Add(enemy);
      }
    }

   public void RemoveEnemy(GameObject enemyToRemove)
    {
        if (enemiesOnScen.Contains(enemyToRemove))
        {
            enemiesOnScen.Remove(enemyToRemove);
        }
        if (enemiesOnScen.Count == 0)
        {
            OnAllEnemiesDied?.Invoke(this,EventArgs.Empty);
        }
    }
}
