using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;
public class WinPanel : MonoBehaviour
{
    [SerializeField] private Image chance›ndictor;

    [SerializeField] private float endValue = -164f;
    [SerializeField] private float tweenTime = 2f;
    [SerializeField] private float waiteTime = 2f;
    [SerializeField] private float scaleTweenTime = 2f;

    [SerializeField] private Button doubleButton;

    [SerializeField] Ease ease;

 
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject claimButton;

    
   [SerializeField] private EquipGunTimer equipGunTimer;

    public event EventHandler ShowDmiondEvent;
    private Tween tween;

    
    [SerializeField] private float X2Min = -164f, X2Max = -100f;
    [SerializeField] private float X3Min = -100f, X3Max = -36f;
    [SerializeField] private float X4Min = -31f, X4Max = 36f;
    [SerializeField] private float X5Min = 31f, X5Max = 105f;
    [SerializeField] private float X32Min = 190f, X32Max = 240f;
    [SerializeField] private float X22Min = 105f, X22Max = 190f;

    void Start()
    {
        tween = chance›ndictor.rectTransform.DOLocalMoveX(-164, tweenTime, false).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        claimButton.GetComponent<Button>().onClick.AddListener(RewardWithDimond);
        equipGunTimer.OnShowWinPanelEvent += EquipGunTimer_ShowWinPanelEvent;
    }

    private void EquipGunTimer_ShowWinPanelEvent(object sender, EventArgs e)
    {
        StartCoroutine(EnableWinPane()); ;
    }

   
    IEnumerator EnableWinPane()
    {
     
        winPanel.SetActive(true);
        winPanel.transform.localScale = Vector3.zero;
        backGround.SetActive(true);
        claimButton.SetActive(false);
        yield return new WaitForSeconds(waiteTime);
        winPanel.GetComponent<RectTransform>().DOScale(Vector3.one, scaleTweenTime);
        yield return new WaitForSeconds(waiteTime * 3);
        claimButton.SetActive(true);
        claimButton.GetComponent<RectTransform>().DOScale(Vector3.one, waiteTime / 2);
    }
  
    public void RewardWithDimond()
    {
        ShowDmiondEvent?.Invoke(this, EventArgs.Empty);
    }

   
    public void UserClickedDoubleButton()
    {
        tween.Kill();
       
        float x = chance›ndictor.rectTransform.anchoredPosition.x;
        if(ValidateRange(X2Min, X2Max, x))
        {
            print("X2");
        }
        else if(ValidateRange( X3Min, X3Max, x))
        {
            print("X3");
        }
        else if (ValidateRange(X4Min,X4Max, x))
        {
            print("X4");
        }
        else if (ValidateRange(X5Min, X5Max, x))
        {
            print("X5");
        }
        else if (ValidateRange(X32Min, X32Max, x))
        {
            print("X33");
        }
        else if (ValidateRange(X22Min, X22Max, x))
        {
            print("X22");
        }
    }
    private bool ValidateRange(float min , float max, float value)
    {
        if (min < 0 || max < 0)
        {
            if (value > min && value < max)
            {
                return true;
            }
        }
        else if (value >min && value< max)
        {

            return true;
        }
        return false ;   
    }
}
