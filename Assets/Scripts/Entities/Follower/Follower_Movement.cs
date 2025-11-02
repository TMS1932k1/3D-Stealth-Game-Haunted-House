using UnityEngine;

public class Follower_Movement : WaypointPatrol
{
    public bool IsMoving()
    {
        return navMeshAgent.velocity.magnitude > 0.1f
            && navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance - 0.2f;
    }

    public void DisableFollowMovement()
    {
        navMeshAgent.isStopped = true; // Stop Follower
        waypoints[currentWaypointIndex].GetComponent<IGetCaught>()?.DisableInputManager(); // Stop Player
    }

    public void AddFollowTarget(Transform target)
    {
        if (waypoints.Count > 0)
        {
            Debug.Log($"[{name}]: Follower had target");
            return;
        }

        waypoints.Add(target);
        navMeshAgent.SetDestination(waypoints[0].position);
    }
}
