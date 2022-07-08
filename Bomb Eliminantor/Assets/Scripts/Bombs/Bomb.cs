using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{

    [SerializeField] private GameObject bombVisual;
    [SerializeField] private bool isFilled;
    private void Awake()
    {
        isFilled = bombVisual.GetComponent<BombVisual>().IsFilled;
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.instance.CanShowBombVisual)
        {
            bombVisual.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") &&GameManager.instance.CanShowBombVisual && isFilled)
        {
           //;
            bombVisual.GetComponent<BombVisual>().Timer = 0;
            bombVisual.SetActive(false);
        }
    }
}
