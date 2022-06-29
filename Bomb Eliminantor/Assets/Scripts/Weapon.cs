using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] GunScriptableObject currentGun;
    public Transform prjectileSpawnPosition;
    private void Awake()
    {
        currentGun.projectileSpawnPostion = prjectileSpawnPosition;
    }
}
