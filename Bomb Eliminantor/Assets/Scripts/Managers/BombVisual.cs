using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace GameUI
{
    public class BombVisual : MonoBehaviour
    {
        [SerializeField] private Image fill›mage;
        [SerializeField] private float timer;
        [SerializeField] private float timerMax;
        public float Timer { get { return timer; } set { timer = value; } }
        [SerializeField] private bool isFilled = false;
        public bool IsFilled { get { return isFilled; } set { isFilled = value; } }

        [SerializeField] private GameObject winParticlePrefab;
        [SerializeField] private Transform winParticleSpawnPosition;
        [SerializeField] private float waitTimeToDestroyParticle = 2;
        public static event EventHandler OnFilledEvent;
        private bool isBombExploded = false;
        private void Start()
        {
            ExplosionTimer.OnTimeOverEvent += ExplosionTimer_OnTimeOverEvent;
        }

        private void ExplosionTimer_OnTimeOverEvent(object sender, EventArgs e)
        {
            isBombExploded = true;
        }

        void Update()
        {
            if (isBombExploded)
            {
                return;
            }
            if (isFilled) return;
            timer += Time.deltaTime;
            if (timer > timerMax)
            {
                timer = timerMax;
                isFilled = true;
                OnFilledEvent?.Invoke(this, EventArgs.Empty);
                StartCoroutine(CreateWinParticle());
            }

            fill›mage.fillAmount = timer / timerMax;
            fill›mage.fillAmount = Mathf.Clamp01(fill›mage.fillAmount);

        }
        private void OnDisable()
        {
            ExplosionTimer.OnTimeOverEvent -= ExplosionTimer_OnTimeOverEvent;
        }
        private IEnumerator CreateWinParticle()
        {
            Transform newParticle = Instantiate(winParticlePrefab.transform, winParticleSpawnPosition.position, Quaternion.identity);

            yield return new WaitForSeconds(waitTimeToDestroyParticle);

            Destroy(newParticle.gameObject);
        }
    }
}

