using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    private Transform target;
    [SerializeField] private float lerpsSpeed = 5F;
    [SerializeField] private float turnSpeed = 10;
    [SerializeField] private Vector3 offset;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction=target.position-transform.position;
        Quaternion lookDiection = Quaternion.LookRotation(direction);
        transform.position = Vector3.Lerp(transform.position, target.position+offset, lerpsSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDiection,lerpsSpeed*Time.deltaTime);
    }
}
