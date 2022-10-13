using System.Collections;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameUI;
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    private static int currentLevel=1;

    [SerializeField] private float waitTimeRToLoadNextScene =5f;
    public float WaitTimeRToLoadNextScene { get { return waitTimeRToLoadNextScene;} set { waitTimeRToLoadNextScene = value; } }
    private WaitForSeconds waitForSeconds;

    [SerializeField] private bool isFirstSceneLoaded=false;
    public bool ÝsFirstSceneLoaded { get { return isFirstSceneLoaded; } set { isFirstSceneLoaded = value; } }
    private int isFirstSceneLoadedForNumber;
    public static event EventHandler UnEquipLaserGun;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        
        currentLevel = PlayerPrefs.GetInt("currentLevel", currentLevel);
        isFirstSceneLoadedForNumber = PlayerPrefs.GetInt("isFirstSceneLoaded",0);
        if (isFirstSceneLoadedForNumber == 0)
        {
            isFirstSceneLoaded = false;
        }
        else if(isFirstSceneLoadedForNumber == 1)
        {
            isFirstSceneLoaded = true;
        }       
    }
    private void OnEnable()
    {
        if (isFirstSceneLoaded)
        {
            SceneManager.LoadSceneAsync(currentLevel);
        }
    }
    private void Start()
    {
        waitForSeconds = new WaitForSeconds(waitTimeRToLoadNextScene);
        RewardDimond.LoadLevelEvent += LoadLevelEvent;
    }
    private void LoadLevelEvent(object sender, System.EventArgs e)
    {
       StartCoroutine(LoadNextLevel());
    }
    private IEnumerator LoadNextLevel()
    {
        yield return waitForSeconds;
        if (currentLevel >= 9)
        {
            currentLevel = 1;
            PlayerPrefs.DeleteAll();
            UnEquipLaserGun?.Invoke(this, EventArgs.Empty);
            SceneManager.LoadSceneAsync(currentLevel);
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            yield break;
        }
        else
        {
            currentLevel++;
            SceneManager.LoadSceneAsync(currentLevel);
            PlayerPrefs.SetInt("currentLevel", currentLevel);
            
        }
      
    }
    public void ReloadCurrentLevel()
    {
        GameManager.instance.GameOver = false;
        SceneManager.LoadSceneAsync(currentLevel);
    }
    private void OnDisable()
    {
        RewardDimond.LoadLevelEvent -= LoadLevelEvent;
    }
}
