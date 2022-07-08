using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class WinPanel : MonoBehaviour
{
    [SerializeField] private Image chance›ndictor;
    [SerializeField] private float endValue = -164f;
    [SerializeField] protected float tweenTime = 2f;
    [SerializeField] protected float waiteTime = 2f;
    [SerializeField] private Button doubleButton;
    [SerializeField] Ease ease;

    [SerializeField][Range(-164,-100)] private float X2Range;
    [SerializeField][Range(-100,-36)] private float X3Range;
    [SerializeField][Range(-36,31)] private float X4Range;
    [SerializeField][Range(31,105)] private float X5Range;
    [SerializeField][Range(105,181)] private float x3Range;
    [SerializeField] [Range(181,240)] private float x2Range;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject backGround;
    [SerializeField] private GameObject claimButton;
    private Tween tween;
    void Start()
    {
    

        tween = chance›ndictor.rectTransform.DOLocalMoveX(-164, tweenTime, false).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
        BombVisual.OnFilledEvent += BombVisual_OnFilledEvent;
    }

    private void BombVisual_OnFilledEvent(object sender, System.EventArgs e)
    {
        StartCoroutine(EnableWinPane());
       
        print("Enable Win");
    }
    IEnumerator EnableWinPane()
    {
        winPanel.SetActive(true);
        backGround.SetActive(true);
        claimButton.SetActive(false);
        yield return new WaitForSeconds(waiteTime);
        winPanel.GetComponent<RectTransform>().DOScale(Vector3.one, waiteTime);
        yield return new WaitForSeconds(waiteTime*3);
        claimButton.SetActive(true);
        claimButton.GetComponent<RectTransform>().DOScale(Vector3.one, waiteTime/2);
    }
    public void UserClickedDoubleButton()
    {
        tween.Kill();
        print(chance›ndictor.rectTransform.anchoredPosition.x);
        float x = chance›ndictor.rectTransform.anchoredPosition.x;
        if(ValidateRange( -164f, -100f, x))
        {
            print("X2");
        }
        else if(ValidateRange( -100f, -36f, x))
        {
            print("X3");
        }
        else if (ValidateRange(-31f, 36f, x))
        {
            print("X4");
        }
        else if (ValidateRange(31f, 105f, x))
        {
            print("X5");
        }
        else if (ValidateRange(105f, 190f, x))
        {
            print("X33");
        }
        else if (ValidateRange(190f, 244f, x))
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
