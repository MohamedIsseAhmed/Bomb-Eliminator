using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;
public class EquipGunTimer : MonoBehaviour
{
    [SerializeField] private GameObject gunEquipTimer;
    [SerializeField] private TextMeshProUGUI equipTimePercentText;
    [SerializeField] private float percentPerLevel = 0f;
    [SerializeField] private float equipTimePercentText›ncreaseSpeed = 0.5f;
    [SerializeField] private float scaleTweenTime = 0.2f;
    [SerializeField] private Image gunDiscover›mage;

    private float smoothVelocity;
    private bool isEquiped;
    private float timeWaitingModifier = 0.65f;

    public event EventHandler OnShowWinPanelEvent;
    public static event  EventHandler OnChangeGunEvent;
    private void Awake()
    {
      
        percentPerLevel = PlayerPrefs.GetFloat("percentPerLevel", 0);
        gunDiscover›mage.fillAmount = PlayerPrefs.GetFloat("gunDiscover›mage.fillAmount", gunDiscover›mage.fillAmount);
        equipTimePercentText.text = "%" + percentPerLevel.ToString();
        isEquiped = gunDiscover›mage.fillAmount==0;
  
    }
    private void Start()
    {
        BombVisual.OnFilledEvent += BombVisual_OnFilledEvent; ;
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
        gunEquipTimer.GetComponent<RectTransform>().DOScale(Vector3.one, scaleTweenTime);

        percentPerLevel += 0.25f;
        percentPerLevel = Mathf.Clamp01(percentPerLevel);
        equipTimePercentText.text = "%" + percentPerLevel.ToString();

        PlayerPrefs.SetFloat("percentPerLevel", percentPerLevel);
        print(PlayerPrefs.GetFloat("percentPerLevel"));

        yield return StartCoroutine(FillDiscover›mage());
        yield return new WaitForSeconds(2);
        gunEquipTimer.SetActive(false);
        OnShowWinPanelEvent?.Invoke(this, EventArgs.Empty);
    }
    private IEnumerator FillDiscover›mage()
    {
        float value = gunDiscover›mage.fillAmount - 0.25f;
        print(value);
        while (value < gunDiscover›mage.fillAmount)
        {

            if (gunDiscover›mage.fillAmount == value)
            {

                equipTimePercentText.text = "%" + percentPerLevel.ToString();

                PlayerPrefs.SetFloat("gunDiscover›mage.fillAmount", gunDiscover›mage.fillAmount);

                yield break;
            }
            gunDiscover›mage.fillAmount = Mathf.SmoothDamp(gunDiscover›mage.fillAmount, value, ref smoothVelocity, equipTimePercentText›ncreaseSpeed * Time.deltaTime);
            gunDiscover›mage.fillAmount = Mathf.Clamp01(gunDiscover›mage.fillAmount);
            if (gunDiscover›mage.fillAmount < 0.010f)
            {
                equipTimePercentText.text = "%" + percentPerLevel.ToString() + "00";
                OnChangeGunEvent?.Invoke(this, EventArgs.Empty);
            }
            yield return new WaitForSeconds(equipTimePercentText›ncreaseSpeed * timeWaitingModifier);
        }
       
        PlayerPrefs.SetFloat("gunDiscover›mage.fillAmount", gunDiscover›mage.fillAmount);
    }
}
