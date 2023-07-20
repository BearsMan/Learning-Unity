using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
[RequireComponent(typeof(ActionScheduler))]
[RequireComponent(typeof(NavMeshAgent))]
public class AIMover : MonoBehaviour, IAction
{
    public NavMeshAgent agent;

    public PatrolPath path;
    private int pathIndex = 0;
    public float arriveDistance = 1.5f;

    private void Start()
    {
        SetDestination(path.GetWayPoint(pathIndex));
    }

    public void SetDestination(Vector3 point)
    {
        GetComponent<ActionScheduler>().StartAction(this);
        agent.isStopped = false;
        agent.SetDestination(point);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, agent.destination);
        if (distance < arriveDistance)
        {
            pathIndex = path.GetNextIndex(pathIndex);
            SetDestination(path.GetWayPoint(pathIndex));
        }
    }

    public void Cancel()
    {
        agent.isStopped = true;
    }

    public void Resume()
    {
        SetDestination(path.GetWayPoint(pathIndex));
    }
}
