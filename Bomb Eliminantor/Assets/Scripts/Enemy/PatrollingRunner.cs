using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingRunner : MonoBehaviour
{
    public static PatrollingRunner instance;
    [SerializeField] private List<Transform> paths;
    private void Awake()
    {
        instance = this;
    }
  
    public List<Transform> GetPathsList() 
    {
        return paths;
    }

}
