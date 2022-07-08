using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipGun : MonoBehaviour
{
    [SerializeField] private GunScriptableObject defaultGun;
  
    [SerializeField] private Transform gunParent;
    private Button button;
    [SerializeField] private GunController controller;
    private bool isEquiped;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ChangeWeapon);
    }
    private void ChangeWeapon()
    {
        if (!isEquiped)
        {
            Transform equipedGun = Instantiate(defaultGun.gunPrfab.transform, gunParent);
            controller.CurrentWeapon.gameObject.SetActive(false);
            controller.ProjectileSpawnPosition = equipedGun.GetComponent<Weapon>().prjectileSpawnPosition;
            controller.CurrentWeapon = equipedGun;
            isEquiped = true;
        }
    }
}
