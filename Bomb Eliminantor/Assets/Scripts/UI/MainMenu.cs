using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Image slidder›mage;
    [SerializeField] private float slidderSpeed=0.25f;
    [SerializeField] private float addAmount = 0.05f;
    private IEnumerator  Start()
    {
        if (LevelManager.instance.›sFirstSceneLoaded)
        {
         
            yield break;
        }
        var asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        while (slidder›mage.fillAmount < 1.3f)
        {
            slidder›mage.fillAmount += addAmount;
            if (slidder›mage.fillAmount > 0.95f)
            {
                LevelManager.instance.›sFirstSceneLoaded = true;
                asyncOperation.allowSceneActivation = true;
                PlayerPrefs.SetInt("isFirstSceneLoaded",1);
            }
            yield return new WaitForSeconds(slidderSpeed);
        }

    }
}
