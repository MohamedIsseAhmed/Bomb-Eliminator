using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;
namespace GameUI
{
    public class WinPanel : MonoBehaviour
    {
        [SerializeField] private Image chance›ndictor;

        [SerializeField] private float endValue = -164f;
        [SerializeField] private float tweenTime = 2f;
        [SerializeField] private float waiteTime = 2f;
        [SerializeField] private float scaleTweenTime = 2f;
        [SerializeField] private float tweenXTarget = -308f;
        [SerializeField] private Button doubleButton;

        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameObject backGround;
        [SerializeField] private GameObject claimButton;

        [SerializeField] private EquipGunTimer equipGunTimer;

        public event EventHandler<DimondCountNumberEventArgs> ShowDmiondEvent;

        [SerializeField] Ease ease;
        private Tween chancetween;
     
        [SerializeField] private float X2Min = -164f, X2Max = -100f;
        [SerializeField] private float X3Min = -100f, X3Max = -36f;
        [SerializeField] private float X4Min = -31f, X4Max = 36f;
        [SerializeField] private float X5Min = 31f, X5Max = 105f;
        [SerializeField] private float X32Min = 190f, X32Max = 240f;
        [SerializeField] private float X22Min = 105f, X22Max = 190f;

        [SerializeField] private TextMeshProUGUI dimondMultiplyText;
        [SerializeField] private TextMeshProUGUI levelText;
        void Start()
        {
            chancetween = chance›ndictor.rectTransform.DOLocalMoveX(tweenXTarget, tweenTime, false).SetEase(ease).SetLoops(-1, LoopType.Yoyo);
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
            winPanel.GetComponent<RectTransform>().DOScale(new Vector3(0.504670024f, 0.504670024f, 0), scaleTweenTime);
            levelText.text = "LEVEL " + PlayerPrefs.GetInt("currentLevel").ToString();
            yield return new WaitForSeconds(waiteTime * 3);
            claimButton.SetActive(true);
            claimButton.GetComponent<RectTransform>().DOScale(Vector3.one, waiteTime / 2);
            yield return new WaitForSeconds(waiteTime);

        }

        public void RewardWithDimond()
        {
            ShowDmiondEvent?.Invoke(this, new DimondCountNumberEventArgs(1));
            doubleButton.GetComponent<Button>().interactable = false;
            claimButton.GetComponent<Button>().interactable = false;
            chancetween.Kill();
            LevelManager.instance.WaitTimeRToLoadNextScene = 3;
        }


        public void UserClickedDoubleButton()
        {
            chancetween.Kill();
            //winPaneltween.Kill();
            //claimButtonScaleTween.Kill();

            float x = chance›ndictor.rectTransform.anchoredPosition.x;
            if (ValidateRange(X2Min, X2Max, x))
            {

                dimondMultiplyText.text = "X2";
                ShowDmiondEvent?.Invoke(this, new DimondCountNumberEventArgs(2));
                claimButton.GetComponent<Button>().interactable = false;


            }
            else if (ValidateRange(X3Min, X3Max, x))
            {
                dimondMultiplyText.text = "X3";
                ShowDmiondEvent?.Invoke(this, new DimondCountNumberEventArgs(3));
                claimButton.GetComponent<Button>().interactable = false;
            }
            else if (ValidateRange(X4Min, X4Max, x))
            {
                dimondMultiplyText.text = "X4";
                ShowDmiondEvent?.Invoke(this, new DimondCountNumberEventArgs(4));
                claimButton.GetComponent<Button>().interactable = false;
            }
            else if (ValidateRange(X5Min, X5Max, x))
            {
                dimondMultiplyText.text = "X5";
                ShowDmiondEvent?.Invoke(this, new DimondCountNumberEventArgs(5));
                claimButton.GetComponent<Button>().interactable = false;
            }
            else if (ValidateRange(X32Min, X32Max, x))
            {
                dimondMultiplyText.text = "X3";
                ShowDmiondEvent?.Invoke(this, new DimondCountNumberEventArgs(3));
                claimButton.GetComponent<Button>().interactable = false;
            }
            else if (ValidateRange(X22Min, X22Max, x))
            {
                dimondMultiplyText.text = "X2";
                ShowDmiondEvent?.Invoke(this, new DimondCountNumberEventArgs(2));
                claimButton.GetComponent<Button>().interactable = false;
            }
        }
        private void OnDisable()
        {
            chancetween.Kill();
            equipGunTimer.OnShowWinPanelEvent -= EquipGunTimer_ShowWinPanelEvent;
        }
        private bool ValidateRange(float min, float max, float value)
        {
            if (min < 0 || max < 0)
            {
                if (value > min && value < max)
                {
                    return true;
                }
            }
            else if (value > min && value < max)
            {
                return true;
            }
            return false;
        }
        public class DimondCountNumberEventArgs : EventArgs
        {
            public int numberOfDimondsToReward;
            public DimondCountNumberEventArgs(int _numberOfDimondsToReward)
            {
                this.numberOfDimondsToReward = _numberOfDimondsToReward;
            }
        }
    }
}

