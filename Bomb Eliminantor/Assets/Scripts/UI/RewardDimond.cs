using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
public class RewardDimond : ObjectPoolBase<Image>
{
    [Header("Dimond instantaition")]
    [SerializeField] private Image dimond›con;
    [SerializeField] private List<GameObject> dimondsTo›nstantaite;
    [SerializeField] private int dimondCountsToInstantaite = 350;
    [SerializeField] private float dimondsRewardedPerLevel = 0;
    [SerializeField] private int CurrentdimondCounts =0;
    [SerializeField] private Transform posiontToSpawn;
    [SerializeField] private Transform destinationOfDimonds;
    [SerializeField] private Transform parent;

    [SerializeField] private int dimondsToActivateOnEnd = 20;

    [Header("About Dimond Text increasing")]
    [SerializeField] private float timeToWaite = 0.10f;
    //[SerializeField] private float lerpSpeed = 0.10f;
    [SerializeField] private float tweenTime = 0.10f;
    [SerializeField] private WinPanel winPanel;
    [SerializeField] private TextMeshProUGUI dimondCounterText;
    [SerializeField] private float textLerpSpeed = 0.5f;
    private bool canAnimateText;
    protected override void  Start()
    {
        prefabTo›nstantiate = dimond›con;
        objectCount = dimondCountsToInstantaite;
        CreateBulletPool(posiontToSpawn.position,parent);
        winPanel.ShowDmiondEvent += WinPanel_ShowDmiondEvent;

        CurrentdimondCounts = PlayerPrefs.GetInt("CurrentdimondCounts", 0);
        dimondCounterText.text = CurrentdimondCounts.ToString("F0");
        dimondsRewardedPerLevel += CurrentdimondCounts;
    }

    private void WinPanel_ShowDmiondEvent(object sender, System.EventArgs e)
    {
        canAnimateText = true;
        StartCoroutine(MoveDimondsToTargetOnEnd());
    }
    private void Update()
    {
        if (!canAnimateText) { return; }
        if (canAnimateText)
        {
            StartCoroutine(›ncreaseDimondCAount());
        }
    }
    IEnumerator ›ncreaseDimondCAount()
    {

        while (CurrentdimondCounts<dimondsRewardedPerLevel)
        {
            CurrentdimondCounts++;
            dimondCounterText.text = CurrentdimondCounts.ToString("F0");
            yield return new WaitForSeconds(textLerpSpeed);
        }
        PlayerPrefs.SetInt("CurrentdimondCounts", CurrentdimondCounts);
        print(PlayerPrefs.GetInt("CurrentdimondCounts"));
        canAnimateText=false;
    }
    public override Image GetBullet(Transform projectileSpawnPosition)
    {
        for (int i = 0; i < objectCount; i++)
        {
            if (!poolList[i].gameObject.activeInHierarchy)
            {
                poolList[i].gameObject.SetActive(true);
                poolList[i].transform.position = projectileSpawnPosition.position;
                poolList[i].transform.rotation = Quaternion.identity;
                return poolList[i].GetComponent<Image>();
            }
        }
        return null;
    }
   private IEnumerator MoveDimondsToTargetOnEnd()
    {
        for (int i = 0; i < dimondsToActivateOnEnd; i++)
        {
            if (!poolList[i].gameObject.activeInHierarchy)
            {
                poolList[i].gameObject.SetActive(true);
                poolList[i].transform.rotation = Quaternion.identity;
                poolList[i].transform.DOMove(destinationOfDimonds.position, tweenTime); /*= Vector3.MoveTowards(poolList[i].transform.position, destinationOfDimonds, lerpSpeed * Time.deltaTime);*/
                StartCoroutine(DeActivateDimond(poolList[i].transform));
            }
            yield return new WaitForSeconds(timeToWaite);

        }
    }
    IEnumerator DeActivateDimond(Transform item)
    {
        yield return new WaitForSeconds(timeToWaite);
        item.gameObject.SetActive(false);
    }
}
