using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class NavMeshMove : MonoBehaviour
{
    private NavMeshAgent agent;
    private Vector3 targetLocation;
    private NavMeshPath targetPath;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updatePosition = false;
        agent.stoppingDistance = 3.0f;
    }

    public void SetStoppingDistance(float newDist)
    {
        agent.stoppingDistance = newDist;
    }


    // Set nav target, generates a path
    // Returns true on valid path, false otherwise
    public bool SetTarget(Vector3 target)
    {
        NavMeshHit nvh;
        if (NavMesh.SamplePosition(target, out nvh, 1.0f, NavMesh.AllAreas))
        {
            // New Path
            NavMeshPath newPath = new NavMeshPath();

            // Generate a path, and if one exists:
            if (agent.CalculatePath(nvh.position, newPath))
            {
                if (newPath.status == NavMeshPathStatus.PathComplete)
                {
                    // Set the goal
                    targetLocation = target;

                    // Set the path
                    targetPath = newPath;
                    agent.SetPath(targetPath);

                    return true;
                }
            }
        }

        return false;
    }

    // Get direction to aim in
    public Vector3 GetMoveDirection()
    {
        return agent.steeringTarget;
    }

    // Update NavMesh with new position
    public void UpdatePosition(Vector3 pos)
    {
        agent.nextPosition = pos;
    }

    // Did we reach our target
    public bool TargetReached()
    {
        return agent.remainingDistance <= agent.stoppingDistance;
    }

    // Cancel nav pathing
    public void ResetTarget()
    {
        targetLocation = new Vector3(float.MaxValue, 0, 0);
        if (agent != null)
            agent.ResetPath();
        targetPath = null;
    }

    // Are we moving?
    public bool IsStopped()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    return true;
                }
            }
        }

        return targetLocation.x == float.MaxValue || targetPath == null;
    }
}
