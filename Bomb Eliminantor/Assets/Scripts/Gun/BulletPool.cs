using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform poolBulletParent;
    [SerializeField] private float timeAfterToActivatePooledObjects = 2;
    private List<Bullet> bullets;
    [SerializeField] private int bulletPoolCount;

    public static BulletPool instance { get; private set; }
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
       
        bullets = new List<Bullet>();
       
    }
    private void Start()
    {
        CreateBulletPool();
    }
    private void CreateBulletPool()
    {
        for (int i = 0; i < bulletPoolCount; i++)
        {
            Bullet bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform);
            bullet.gameObject.SetActive(false);
            bullets.Add(bullet);
        }
    }
    public Bullet GetBullet(GunScriptableObject currentGun )
    {
        if(currentGun == null)
        {
            print("is is null!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
        }
        for (int i = 0; i < bulletPoolCount; i++)
        {
            if (!bullets[i].gameObject.activeInHierarchy)
            {
                bullets[i].gameObject.SetActive(true);
                bullets[i].transform.position = currentGun.projectileSpawnPostion.position;
                print(currentGun.gunPrfab.name);
                bullets[i].transform.rotation = Quaternion.identity;
                bullets[i].SetDirection(currentGun.projectileSpawnPostion.forward);
                StartCoroutine(DeActivateProjectiles(bullets[i],currentGun));
                return bullets[i];
            }
        }
        return null;
    }
    IEnumerator DeActivateProjectiles(Bullet bullet,GunScriptableObject currentGun)
    {
        yield return new WaitForSeconds(timeAfterToActivatePooledObjects);
        bullet.transform.position = currentGun.projectileSpawnPostion.position;
        bullet.gameObject.SetActive(false);
    }
}
