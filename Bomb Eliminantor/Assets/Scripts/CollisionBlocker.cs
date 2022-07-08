using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionBlocker : MonoBehaviour
{
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private CapsuleCollider capsuleCollider2;

    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private BoxCollider boxCollider2;
    void Start()
    {
        if(capsuleCollider != null)
        {
            Physics.IgnoreCollision(capsuleCollider, capsuleCollider2,true);
        }
        if (boxCollider != null)
            Physics.IgnoreCollision(boxCollider, boxCollider2,true);
    }
}
