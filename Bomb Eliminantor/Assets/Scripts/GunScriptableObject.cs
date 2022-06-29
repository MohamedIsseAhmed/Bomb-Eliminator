using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunScriptableObject/GunType")]
public class GunScriptableObject : ScriptableObject
{
    public Weapon gunPrfab;
    public enum FireMode { Auto,Burst}
    public FireMode fireMode;
    public int burstCount;
    public Transform projectileSpawnPostion;
}
