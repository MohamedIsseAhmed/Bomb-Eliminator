using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameUI
{
    public class DisableJoystick : MonoBehaviour
    {
        [SerializeField] private GameObject joystick;
        private EquipGunTimer equipGun;
        private void Awake()
        {
            equipGun = GetComponent<EquipGunTimer>();
        }
        private void OnEnable()
        {
            ExplosionTimer.OnTimeOverEvent += DisableJoystickObject;
            equipGun.OnShowWinPanelEvent += DisableJoystickObject;
        }

        private void DisableJoystickObject(object sender, System.EventArgs e)
        {
            joystick.SetActive(false);
        }
 
        private void OnDisable()
        {
            ExplosionTimer.OnTimeOverEvent -= DisableJoystickObject;
            equipGun.OnShowWinPanelEvent -= DisableJoystickObject;
        }
    }
}

