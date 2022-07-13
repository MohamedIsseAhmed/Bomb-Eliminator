using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Civil : MonoBehaviour
{
    private Animator animator;
    
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        BombVisual.OnFilledEvent += BombVisual_OnFilledEvent;
    }

    private void BombVisual_OnFilledEvent(object sender, System.EventArgs e)
    {
        animator.SetTrigger("Victory");
    }
}
