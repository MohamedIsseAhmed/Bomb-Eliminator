using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Vector3 direction;
    IObjectPool<Bullet> objectPool;
    public float timeToDisableBullet;
    private float bulletTimer;
    private bool canGoFowrad=true;
    public bool CanGoFowrad { get { return canGoFowrad; }  set { canGoFowrad = value; } }

    private Vector3 originPosition;
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
      
        timeToDisableBullet = 2f;
    }
    void Update()
    {
        transform.position += direction * bulletSpeed * Time.deltaTime;
        if (canGoFowrad)
        {
            
        }
        bulletTimer += Time.deltaTime;
        if (bulletTimer > timeToDisableBullet)
        {
            //DisableBullet();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(10);
            DisableBullet();
        }
        DisableBullet();


    }
    private void OnCollisionEnter(Collision collision)
    {
        DisableBullet();
    }
    public void SetPoolObject(IObjectPool<Bullet> _objectPool)
    {
        objectPool = _objectPool;
    }
    public void DisableBullet()
    {
        
        
       // gameObject.SetActive(false);
        
    }
}