using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject backGrýund;
    [SerializeField] private float tweenScaleTime;
    [SerializeField] private float timeToWaitToScale;
    private void Start()
    {
        ExplosionTimer.OnTimeOverEvent += ExplosionTimer_OnTimeOverEvent;
    }

    private void ExplosionTimer_OnTimeOverEvent(object sender, System.EventArgs e)
    {
        StartCoroutine(ScaleGameOverPanel());
    }
    IEnumerator ScaleGameOverPanel()
    {
        yield return new WaitForSeconds(timeToWaitToScale);
        backGrýund.SetActive(true);
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localScale = Vector3.zero;
        gameOverPanel.transform.DOScale(Vector3.one, tweenScaleTime);
    }
}
