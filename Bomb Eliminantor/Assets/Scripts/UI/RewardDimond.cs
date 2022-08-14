using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System;

namespace GameUI
{
    public class RewardDimond : MonoBehaviour
    {
        [Header("Dimond instantaition")]
        [SerializeField] private Transform dimond›con;
        [SerializeField] private int dimondCountsToInstantaite = 350;
        [SerializeField] private float dimondsRewardedPerLevel = 0;
        [SerializeField] private int CurrentdimondCounts = 0;
        [SerializeField] private Transform posiontToSpawn;
        [SerializeField] private Transform destinationOfDimonds;
        [SerializeField] private Transform parent;

        [SerializeField] private int dimondsToActivateOnEnd = 20;

        [Header("About Dimond Text increasing")]
        [SerializeField] private float timeToWaite = 0.10f;
        [SerializeField] private float tweenTime = 0.10f;
        [SerializeField] private WinPanel winPanel;
        [SerializeField] private TextMeshProUGUI dimondCounterText;
        [SerializeField] private TextMeshProUGUI normalDimondCountText;
        [SerializeField] private float textLerpSpeed = 0.5f;
        private bool canAnimateText;

        [SerializeField] private Transform poolBulletParent;
        [SerializeField] private float timeAfterToActivatePooledObjects = 2;
        private List<Transform> dimonds;
        [SerializeField] private int dimondCount = 120;

        public static event EventHandler LoadLevelEvent;

        private Tween dimondMoveTween;

        void Awake()
        {
            dimonds = new List<Transform>();
        }
        private void Start()
        {
            winPanel.ShowDmiondEvent += WinPanel_ShowDmiondEvent;
            CurrentdimondCounts = PlayerPrefs.GetInt("CurrentdimondCounts", 0);
            dimondCounterText.text = CurrentdimondCounts.ToString("F0");
            dimondsRewardedPerLevel += CurrentdimondCounts;
            normalDimondCountText.text = (dimondsRewardedPerLevel - CurrentdimondCounts).ToString("F0");
            CreateDimondPool();
        }
        private void CreateDimondPool()
        {
            for (int i = 0; i < dimondCount; i++)
            {
                Transform bullet = Instantiate(dimond›con, transform.position, Quaternion.identity, parent);
                bullet.gameObject.SetActive(false);
                dimonds.Add(bullet);
            }
        }


        private void WinPanel_ShowDmiondEvent(object sender, WinPanel.DimondCountNumberEventArgs e)
        {
            dimondsRewardedPerLevel += e.numberOfDimondsToReward * 100;

            canAnimateText = true;
            int dimondsToActivateOnEndModifier = 1;
            if (e.numberOfDimondsToReward > 1)
            {
                dimondsToActivateOnEndModifier = e.numberOfDimondsToReward / 2;
            }

            StartCoroutine(MoveDimondsToTargetOnEnd(dimondsToActivateOnEndModifier));
        }
        private void Update()
        {
            if (!canAnimateText) { return; }
            if (canAnimateText)
            {
                StartCoroutine(›ncreaseDimondCAount());
            }
        }
        private IEnumerator ›ncreaseDimondCAount()
        {
            while (CurrentdimondCounts < dimondsRewardedPerLevel)
            {
                CurrentdimondCounts++;
                dimondCounterText.text = CurrentdimondCounts.ToString("F0");
                yield return new WaitForSeconds(textLerpSpeed);
            }
            PlayerPrefs.SetInt("CurrentdimondCounts", CurrentdimondCounts);
            canAnimateText = false;

        }

        public Transform GetBullet(Transform projectileSpawnPosition)
        {
            for (int i = 0; i < dimondCountsToInstantaite; i++)
            {
                if (!dimonds[i].gameObject.activeInHierarchy)
                {
                    dimonds[i].gameObject.SetActive(true);
                    dimonds[i].transform.position = projectileSpawnPosition.position;
                    dimonds[i].transform.rotation = Quaternion.identity;
                    return dimonds[i];
                }
            }
            return null;
        }
        private IEnumerator MoveDimondsToTargetOnEnd(int _dimondsToActivateOnEndModifier = 1)
        {
            dimondsToActivateOnEnd *= _dimondsToActivateOnEndModifier;
            for (int i = 0; i < dimondsToActivateOnEnd; i++)
            {
                if (!dimonds[i].gameObject.activeInHierarchy)
                {
                    dimonds[i].gameObject.SetActive(true);
                    dimonds[i].transform.rotation = Quaternion.identity;
                    dimondMoveTween = dimonds[i].transform.DOMove(destinationOfDimonds.position, tweenTime);
                    StartCoroutine(DeActivateDimond(dimonds[i].transform));
                }
                yield return new WaitForSeconds(timeToWaite);

            }
            LoadLevelEvent?.Invoke(this, EventArgs.Empty);
            dimondMoveTween.Kill();
        }

        IEnumerator DeActivateDimond(Transform item)
        {
            yield return new WaitForSeconds(timeToWaite);
            item.gameObject.SetActive(false);
        }
        private void OnDisable()
        {
            dimondMoveTween.Kill();
        }
        private void OnDestroy()
        {
            dimondMoveTween.Kill();
        }
    }
}

