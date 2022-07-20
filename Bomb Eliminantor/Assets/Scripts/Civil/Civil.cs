using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civil : MonoBehaviour
{
    private Animator animator;
   
    private void Start()
    {
        animator = GetComponent<Animator>();
        BombVisual.OnFilledEvent += BombVisual_OnFilledEvent;
    }

    private void BombVisual_OnFilledEvent(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Victory");
    }
    private void OnDisable()
    {
        BombVisual.OnFilledEvent -= BombVisual_OnFilledEvent;
    }
}
