using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    [SerializeField] private GunScriptableObject currentGun;
    public GunScriptableObject CurrentGun { get { return currentGun; } }
    [SerializeField] private Transform gunParent;

    private Vector3 weaponOriginPosition;
    private Vector3 weaponOriginRotation;
    private Transform currentWeapon;
    public Transform CurrentWeapon { get { return currentWeapon; } }
    private Vector3 gunSpawnPosition= new Vector3(0.215000004f, 0.832000017f, 0.414999992f);
    
    private Vector3 gunrotaion;
    void Awake()
    {
        
        gunrotaion = new Vector3(319.814056f, 6.36982012f, 81.3262711f);
        currentWeapon = Instantiate(currentGun.gunPrfab.transform, gunSpawnPosition, Quaternion.Euler(gunrotaion), gunParent);
        weaponOriginPosition = gunSpawnPosition;
        weaponOriginRotation = gunrotaion;
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
    public void WeaponPositionAndRotationOnShooting()
    {
        currentWeapon.transform.localPosition = new Vector3(0.305999994f, 0.90200001f, 0.0689999983f);
        currentWeapon.transform.localEulerAngles = new Vector3(302.237488f, 61.6823273f, 348.534119f);
    }
}
