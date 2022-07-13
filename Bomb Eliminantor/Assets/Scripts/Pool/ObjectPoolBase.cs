using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPoolBase<T> : MonoBehaviour where T: Component
{
    protected private T prefabTo›nstantiate;
    protected private Transform poolBulletParent;
    protected private float timeAfterToActivatePooledObjects = 2;
    protected List<GameObject> poolList=new List<GameObject>();
    protected private int objectCount=100;
   
    protected virtual  void Start()
    {
        print("base start");
       
    }
    protected void CreateBulletPool(Vector3 position, Transform parent)
    {
        for (int i = 0; i < objectCount; i++)
        {
           
            Transform bullet =Instantiate(prefabTo›nstantiate.transform ,position, Quaternion.identity, parent) ;
            poolList.Add(bullet.gameObject);
           
            //GameObject bullet = (T)(object)Instantiate((GameObject)(object)sourcePool) as GameObject;
            bullet.gameObject.SetActive(false);
           
        }
    }
    public abstract T GetBullet(Transform projectileSpawnPosition);
   //protected IEnumerator DeActivateProjectiles(Transform bullet, Transform projectileSpawnPosition)
   // {
   //     yield return new WaitForSeconds(timeAfterToActivatePooledObjects);
   //     bullet.transform.position = projectileSpawnPosition.position;
   //     bullet.gameObject.SetActive(false);
   // }
}
