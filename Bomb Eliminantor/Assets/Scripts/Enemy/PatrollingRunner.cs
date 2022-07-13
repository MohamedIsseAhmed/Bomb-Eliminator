using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrollingRunner : MonoBehaviour
{
    public static PatrollingRunner instance;
    [SerializeField] private List<Transform> paths;
    [SerializeField] private string pathTag;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        //paths=new List<GameObject>();
       // paths.AddRange(GameObject.FindGameObjectsWithTag("path"));
       
    }
    //public IEnumerator FollowPath(NavMeshAgent navMeshAgent,Vector3[] pathPositions)
    //{
    //    Debug.Log("Update");
    //    Vector3 currentWaypoint = pathPositions[0];
    //    int waypoint›ndex = 0;
    //    float speed = 10f;
    //    float distanceThreshHold = 0.05f;
    //    while (true)
    //    {
    //        Debug.Log("ok");
    //        //npc.transform.position = Vector3.MoveTowards(npc.transform.position, currentWaypoint, speed*Time.deltaTime);
    //        navMeshAgent.SetDestination(currentWaypoint);

    //        if (Vector3.Distance(navMeshAgent.transform.position, currentWaypoint) < distanceThreshHold)
    //        {
    //            waypoint›ndex = (waypoint›ndex + 1) % pathPositions.Length;
    //            currentWaypoint = pathPositions[waypoint›ndex];
    //            yield return new WaitForSeconds(0.2f);
    //        }
    //    }
    //}
    public List<Transform> GetPathsList() 
    {
        return paths;
    }

}
