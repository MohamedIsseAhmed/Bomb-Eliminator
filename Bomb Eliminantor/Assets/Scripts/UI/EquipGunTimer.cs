using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
namespace GameUI
{
    public class EquipGunTimer : MonoBehaviour
    {
        [SerializeField] private GameObject gunEquipTimer;
        [SerializeField] private TextMeshProUGUI equipTimePercentText;
        [SerializeField] private float percentPerLevel = 0f;
        [SerializeField] private float equipTimePercentTextİncreaseSpeed = 0.5f;
        [SerializeField] private float scaleTweenTime = 0.2f;
        [SerializeField] private Image gunDiscoverİmage;

        private float smoothVelocity;
        private bool isEquiped;
        private float timeWaitingModifier = 0.65f;

        public event EventHandler OnShowWinPanelEvent;
        public static event EventHandler OnChangeGunEvent;
        private Tween gunEquipTween;
        private void Awake()
        {

            percentPerLevel = PlayerPrefs.GetFloat("percentPerLevel", 0);
            gunDiscoverİmage.fillAmount = PlayerPrefs.GetFloat("gunDiscoverİmage.fillAmount", gunDiscoverİmage.fillAmount);
            equipTimePercentText.text = "%" + percentPerLevel.ToString();
            isEquiped = gunDiscoverİmage.fillAmount == 0;

        }
        private void Start()
        {
            BombVisual.OnFilledEvent += BombVisual_OnFilledEvent;
        }

        private void BombVisual_OnFilledEvent(object sender, System.EventArgs e)
        {
            if (!isEquiped)
            {
                StartCoroutine(ShowEquipWeaponTimerPanel());
            }
            else
            {
                OnShowWinPanelEvent?.Invoke(this, EventArgs.Empty);
            }
        }

        private IEnumerator ShowEquipWeaponTimerPanel()
        {
            gunEquipTimer.SetActive(true);
            gunEquipTimer.transform.localScale = Vector3.zero;
            gunEquipTween = gunEquipTimer.GetComponent<RectTransform>().DOScale(Vector3.one, scaleTweenTime);

            percentPerLevel += 0.25f;
            percentPerLevel = Mathf.Clamp01(percentPerLevel);
            equipTimePercentText.text = "%" + percentPerLevel.ToString();

            PlayerPrefs.SetFloat("percentPerLevel", percentPerLevel);


            yield return StartCoroutine(FillDiscoverİmage());
            yield return new WaitForSeconds(2);
            gunEquipTimer.SetActive(false);
            OnShowWinPanelEvent?.Invoke(this, EventArgs.Empty);
        }

        private void OnDisable()
        {
            BombVisual.OnFilledEvent -= BombVisual_OnFilledEvent;
            gunEquipTween.Kill();
        }
        private IEnumerator FillDiscoverİmage()
        {
            float value = gunDiscoverİmage.fillAmount - 0.25f;

            while (value < gunDiscoverİmage.fillAmount)
            {

                if (gunDiscoverİmage.fillAmount == value)
                {

                    equipTimePercentText.text = "%" + percentPerLevel.ToString();

                    PlayerPrefs.SetFloat("gunDiscoverİmage.fillAmount", gunDiscoverİmage.fillAmount);

                    yield break;
                }
                gunDiscoverİmage.fillAmount = Mathf.SmoothDamp(gunDiscoverİmage.fillAmount, value, ref smoothVelocity, equipTimePercentTextİncreaseSpeed * Time.deltaTime);
                gunDiscoverİmage.fillAmount = Mathf.Clamp01(gunDiscoverİmage.fillAmount);
                if (gunDiscoverİmage.fillAmount < 0.010f)
                {
                    equipTimePercentText.text = "%" + percentPerLevel.ToString() + "00";
                    OnChangeGunEvent?.Invoke(this, EventArgs.Empty);
                }
                yield return new WaitForSeconds(equipTimePercentTextİncreaseSpeed * timeWaitingModifier);
            }

            PlayerPrefs.SetFloat("gunDiscoverİmage.fillAmount", gunDiscoverİmage.fillAmount);
        }
    }
}

