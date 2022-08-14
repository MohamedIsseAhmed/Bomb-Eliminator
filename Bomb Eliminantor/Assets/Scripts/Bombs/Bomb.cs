using GameUI;
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
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            StopBombPointerAnimation?.Invoke(this, EventArgs.Empty);
            GameManager.instance.LevelComplated = true;
            bombVisual.SetActive(true);
        }
    }
   
}
