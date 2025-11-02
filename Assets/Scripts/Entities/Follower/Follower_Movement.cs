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
}
