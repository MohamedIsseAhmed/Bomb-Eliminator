using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Bomb : MonoBehaviour
{

    [SerializeField] private GameObject bombVisual;

    public static event EventHandler StopBombPointerAnimation;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && GameManager.instance.CanShowBombVisual)
        {
            StopBombPointerAnimation?.Invoke(this, EventArgs.Empty);
            bombVisual.SetActive(true);
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") &&GameManager.instance.CanShowBombVisual)
        {
           
            bombVisual.GetComponent<BombVisual>().Timer = 0;
            bombVisual.SetActive(false);
        }
    }
}
