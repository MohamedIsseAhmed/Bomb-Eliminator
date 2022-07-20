using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
public class Bullet : MonoBehaviour
{
    [SerializeField] private float bulletSpeed = 10f;
    private Vector3 direction;

    private Rigidbody rigidbody; 
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
    void Update()
    {
       transform.position += direction * bulletSpeed * Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();
        if (damagable != null)
        {
            damagable.TakeDamage(10);
           
        }
    }
}
