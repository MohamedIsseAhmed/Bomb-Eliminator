using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GunScriptableObject currentGun;
    public GunScriptableObject CurrentGun { get { return currentGun; } }
    [SerializeField] private Transform gunParent;

  
    private Transform currentWeapon;
    public Transform CurrentWeapon { get { return currentWeapon; } set { currentWeapon = value; } }
    [SerializeField] private Transform projectileSpawnPosition;
    public Transform ProjectileSpawnPosition { get { return projectileSpawnPosition; }
        set { projectileSpawnPosition = value; }}
    private Vector3 weaponOriginPosition;
    private Vector3 weaponOriginRotation;
    private Vector3 gunSpawnPosition= new Vector3(0.215000004f, 0.832000017f, 0.414999992f);
    private Vector3 gunrotaion;

    private Weapon weapon;
    void Awake()
    {
        if (currentGun.canEquipLaserGun)
        {
            gunrotaion = new Vector3(319.814056f, 6.36982012f, 81.3262711f);
            currentWeapon = Instantiate(currentGun.lasergunPrfab.transform, gunSpawnPosition, Quaternion.Euler(gunrotaion), gunParent);
            weapon = currentWeapon.GetComponent<Weapon>();
            projectileSpawnPosition = weapon.prjectileSpawnPosition;
            weaponOriginPosition = gunSpawnPosition;
            weaponOriginRotation = gunrotaion;
        }
        else
        {
            gunrotaion = new Vector3(319.814056f, 6.36982012f, 81.3262711f);
            currentWeapon = Instantiate(currentGun.gunPrfab.transform, gunSpawnPosition, Quaternion.Euler(gunrotaion), gunParent);
            weapon = currentWeapon.GetComponent<Weapon>();
            projectileSpawnPosition = weapon.prjectileSpawnPosition;
            weaponOriginPosition = gunSpawnPosition;
            weaponOriginRotation = gunrotaion;
        }
       
        
    }
    //AnimationEvent
    public void BringBackWeaponOriginPosition()
    {
        currentWeapon.transform.localPosition = weaponOriginPosition;
        currentWeapon.transform.localEulerAngles = weaponOriginRotation;

    }
    //AnimationEvent
    public void RepositionWeaponWhenRunning()
    {
        currentWeapon.transform.localPosition = new Vector3(0.116065338f, 0.924593449f, 0.141138822f);
        currentWeapon.transform.localEulerAngles = new Vector3(273.997253f, 279.588867f, 144.62648f);
    }
    //AnimationEvent 
    public void WeaponPositionAndRotationOnShootingPlayer()
    {
        currentWeapon.transform.localPosition = new Vector3(0.305999994f, 0.90200001f, 0.0689999983f);
        currentWeapon.transform.localEulerAngles = new Vector3(302.237488f, 61.6823273f, 348.534119f);
    }
    public void WeaponPositionAndRotationOnShootingEnemy()
    {
        currentGun.gunPrfab.transform.localPosition = new Vector3(0.294999987f, 0.947000027f, 0.147f);
        currentGun.gunPrfab.transform.localEulerAngles = new Vector3(300.602997f, 75.4687881f, 348.521545f);
    }
}
