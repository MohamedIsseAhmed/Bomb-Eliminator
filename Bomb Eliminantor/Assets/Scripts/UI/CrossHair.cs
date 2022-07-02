using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHair : MonoBehaviour
{
    private RectTransform rectTransform;
    [SerializeField] private Image crossHair;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private HealthSystem healthSystem;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        crossHair.gameObject.SetActive(false);
        
    }
    private void OnEnable()
    {
        healthSystem.OnDead += HealthSystem_OnDead;
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        crossHair.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        healthSystem.OnDead -= HealthSystem_OnDead;
    }
    private void Update()
    {
        Rotate();
    }
    public void Rotate()
    {
        if (!crossHair.gameObject.activeSelf) return;
        rectTransform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime, Space.World);
    }
    public void ShowCrossHair(bool is›nShootingRange)
    {
       crossHair.gameObject.SetActive(is›nShootingRange);
    }
}
