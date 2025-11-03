using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    protected NavMeshAgent navMeshAgent;

    [SerializeField] protected List<Transform> waypoints = new();
    protected int currentWaypointIndex = 0;


    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (waypoints != null && waypoints.Count > 0)
            navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
    }

    private void Update()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
            {
                currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
                navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
            }
        }
    }

    public void EnableMovement(bool enable)
    {
        navMeshAgent.isStopped = !enable;
    }
}
