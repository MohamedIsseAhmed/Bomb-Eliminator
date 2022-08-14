using System.Collections;
using UnityEngine;
using GameUI;
public class Explosion : MonoBehaviour
{
    [SerializeField] private float explosionForce = 700;
    [SerializeField] private float explosionRadius = 20;
    [SerializeField] private float sphreRadius = 20;
    [SerializeField] private float upWardModifier = 7;

    [SerializeField] private GameObject expolosionParticle;

    [SerializeField] private Transform expolosionPosition;

    private float timeToWaitToDestroyExplosionParticle = 0.2f;

    private float xAxisrandomMin = -5;
    private float xAxisrandomMax = 2;

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
        
        for (int i = 0; i < collidersNearExplosion; i++)
        {
            colliders[i].GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            colliders[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,
               colliders[i].transform.position + GetRandomPosition(), explosionRadius, upWardModifier);
            yield return null;
        }

    }
    private void OnDisable()
    {
        ExplosionTimer.OnTimeOverEvent -= ExplosionTimer_OnTimeOverEvent;
    }
    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(xAxisrandomMin, xAxisrandomMax), 0, 0);
    }

    private IEnumerator InstantiateExplosion()
    {
        GameObject newParticle = Instantiate(expolosionParticle, expolosionPosition.position, Quaternion.identity);
        SoundManager.instance.PlaySound(SoundManager.Sound.GameOver);
        Vector3 explosionParticlePosition = newParticle.transform.position;
        StartCoroutine(ExplosionCoroutine(explosionParticlePosition));
        yield return new WaitForSeconds(timeToWaitToDestroyExplosionParticle);
        Destroy(newParticle.gameObject);
    }
}
