using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameUI;
[CreateAssetMenu(menuName = "GunScriptableObject/GunType")]
public class GunScriptableObject : ScriptableObject
{
    public enum WeaponType
    {
        LaserGun,
        NormalGun
    }
    public enum CharacterEquipedThiGun
    {
        Player,
        Enemy
    }
    public CharacterEquipedThiGun characterEquipedThiGun;
    public WeaponType weaponType;
    public Weapon gunPrfab;
    public Weapon lasergunPrfab;
    public enum FireMode { Auto,Burst}
    public FireMode fireMode;
    public int burstCount;

    public bool canEquipLaserGun;
   
    private void OnEnable()
    {
        LevelManager.UnEquipLaserGun += Ýnstance_UnEquipLaserGun;
        EquipGunTimer.OnChangeGunEvent += EquipGunTimer_OnChangeGunEvent;
    }

    private void Ýnstance_UnEquipLaserGun(object sender, System.EventArgs e)
    {
        if (characterEquipedThiGun == CharacterEquipedThiGun.Player)
        {
            canEquipLaserGun = false;
        }
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
        LevelManager.UnEquipLaserGun -= Ýnstance_UnEquipLaserGun;
        EquipGunTimer.OnChangeGunEvent -= EquipGunTimer_OnChangeGunEvent;
    }
}
