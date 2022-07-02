using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : MonoBehaviour
{
    [SerializeField] private LineRenderer laserLine;
    [SerializeField] private float rayDistance = 100f;
    [SerializeField] private LayerMask enemyLayer;
    
    void Update()
    {
        Ray ray = new Ray(laserLine.transform.position, laserLine.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayDistance, enemyLayer))
        {
            laserLine.SetPosition(1, laserLine.transform.InverseTransformPoint(hit.point));
        }
        else
        {
            laserLine.SetPosition(1, new Vector3(0,0,0));
        }
    }
}
