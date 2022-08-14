using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace GameUI
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject settingPanel;
        [SerializeField] private Slider musicVolumeSlider;
        [SerializeField] private Slider soundVolumeSlider;
        private Button goToSettingsBtn;
        private Button backButton;

        private bool isPanelActive;
        void Awake()
        {
            goToSettingsBtn = transform.Find("GoToSettingsBtn").GetComponent<Button>();
            backButton = transform.Find("backButton").GetComponent<Button>();

            goToSettingsBtn.onClick.AddListener(ActivateSettingsPanel);
            backButton.onClick.AddListener(DeActivateSettingsPanel);
            musicVolumeSlider.onValueChanged.AddListener(MusicVolumeSettings);
            soundVolumeSlider.onValueChanged.AddListener(SoundVolumeSettings);

        }
        private void Start()
        {
            musicVolumeSlider.value = MusicManager.instance.Volume;
            soundVolumeSlider.value = SoundManager.instance.Volume;
        }
        private void SoundVolumeSettings(float volume)
        {
            SoundManager.instance.IncreaOrDecreaseseVolume(volume);
        }
        private void MusicVolumeSettings(float volume)
        {
            MusicManager.instance.IncreaOrDecreaseseVolume(volume);
        }

        private void ActivateSettingsPanel()
        {
            isPanelActive = settingPanel.gameObject.activeInHierarchy;
            settingPanel.gameObject.SetActive(true);
            backButton.gameObject.SetActive(true);
            StartCoroutine(TimeScaleSetting());
        }
        private void DeActivateSettingsPanel()
        {
            settingPanel.gameObject.SetActive(false);
            backButton.gameObject.SetActive(false);
            StartCoroutine(DeActivateTimeScale());
        }
        IEnumerator TimeScaleSetting()
        {
            goToSettingsBtn.gameObject.SetActive(false);
            yield return new WaitForSeconds(1);
            Time.timeScale = 0;
            yield break;
        }
        IEnumerator DeActivateTimeScale()
        {
            Time.timeScale = 1;
            goToSettingsBtn.gameObject.SetActive(true);
            yield break;
        }
    }
}


 
