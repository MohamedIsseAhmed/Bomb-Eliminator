using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionForce = 700;
    [SerializeField] private float explosionRadius = 20;
    [SerializeField] private float sphreRadius = 20;

    [SerializeField] private GameObject expolosionParticle;

    [SerializeField] private Transform expolosionPosition;

    private int civilLayer = 1 << 10;
    void Start()
    {
        ExplosionTimer.OnTimeOverEvent += ExplosionTimer_OnTimeOverEvent;
    }

    private void ExplosionTimer_OnTimeOverEvent(object sender, System.EventArgs e)
    {
        StartCoroutine(InstantiateExplosion());
    }

    private IEnumerator ExplosionCoroutine(Vector3 position)
    {
        Collider[] colliders = new Collider[6];
       
        int collidersNearExplosion = Physics.OverlapSphereNonAlloc(position, sphreRadius, colliders, civilLayer);
        print(collidersNearExplosion);
        for (int i = 0; i < collidersNearExplosion; i++)
        {
            colliders[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
               colliders[i].transform.position + new Vector3(Random.Range(-5, 2), 0, 0), explosionRadius, 5);
            yield return null;
        }
        
    }
    private IEnumerator InstantiateExplosion()
    {
        GameObject newParticle = Instantiate(expolosionParticle, expolosionPosition.position, Quaternion.identity);
        Vector3 explosionParticlePosition = newParticle.transform.position;
        StartCoroutine(ExplosionCoroutine(explosionParticlePosition));
        yield return new WaitForSeconds(0.20f);
        Destroy(newParticle.gameObject);
    }
}
