using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class MainMenu : MonoBehaviour
{
    private Button playButton;
    private Button goToSettingsBtn;
    [SerializeField] private GameObject settingPanel;
    private void Awake()
    {
        playButton = transform.Find("PlayButton").GetComponent<Button>();
        playButton.onClick.AddListener(LoadNextScene);
       
        goToSettingsBtn.onClick.AddListener(()=>settingPanel.gameObject.SetActive(!settingPanel.gameObject.activeSelf));
    }
 
    private  void LoadNextScene()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
