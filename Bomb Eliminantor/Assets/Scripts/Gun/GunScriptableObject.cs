using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GunScriptableObject/GunType")]
public class GunScriptableObject : ScriptableObject
{
    public enum CharacterEquipedThiGun
    {
        Player,
        Enemy
    }
    public CharacterEquipedThiGun characterEquipedThiGun;
    public Weapon gunPrfab;
    public Weapon LasergunPrfab;
    public enum FireMode { Auto,Burst}
    public FireMode fireMode;
    public int burstCount;
    public Transform projectileSpawnPostion;
    public bool canEquipLaserGun;
    private void Awake()
    {
        projectileSpawnPostion = gunPrfab.prjectileSpawnPosition;
    }
    private void OnEnable()
    {
        EquipGunTimer.OnChangeGunEvent += EquipGunTimer_OnChangeGunEvent;
    }

    private void EquipGunTimer_OnChangeGunEvent(object sender, System.EventArgs e)
    {
        if (characterEquipedThiGun == CharacterEquipedThiGun.Player)
        {
            canEquipLaserGun = true;
        }
    }
    private void OnDisable()
    {
        EquipGunTimer.OnChangeGunEvent -= EquipGunTimer_OnChangeGunEvent;
    }
}
