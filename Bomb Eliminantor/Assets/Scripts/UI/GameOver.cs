using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject backGround;

    [SerializeField] private float tweenScaleTime;
    [SerializeField] private float timeToWaitToScale;

    [SerializeField] private Button retryButton;
    private void Awake()
    {
        retryButton.onClick.AddListener(ReloadLevel);
    }
    private void Start()
    {
        ExplosionTimer.OnTimeOverEvent += ExplosionTimer_OnTimeOverEvent;
        Shooting.EventHandlerOnPlayerDied += Shooting_EventHandlerOnPlayerDied;
    }

    private void Shooting_EventHandlerOnPlayerDied(object sender, System.EventArgs e)
    {
        StartCoroutine(ScaleGameOverPanel());
    }

    private void ExplosionTimer_OnTimeOverEvent(object sender, System.EventArgs e)
    {
        StartCoroutine(ScaleGameOverPanel());
    }
    private IEnumerator ScaleGameOverPanel()
    {
        yield return new WaitForSeconds(timeToWaitToScale);
        backGround.SetActive(true);
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localScale = Vector3.zero;
        gameOverPanel.transform.DOScale(Vector3.one, tweenScaleTime);
    }
    private void ReloadLevel()
    {
        LevelManager.instance.ReloadCurrentLevel();
    }
    private void OnDisable()
    {
        ExplosionTimer.OnTimeOverEvent -= ExplosionTimer_OnTimeOverEvent;
        retryButton.onClick.RemoveAllListeners();
        Shooting.EventHandlerOnPlayerDied -= Shooting_EventHandlerOnPlayerDied;
    }
}
