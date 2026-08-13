using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Route")]
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField, Min(0f)] private float waypointWaitTime = 1.5f;
    [SerializeField, Min(0.1f)] private float waypointReachedDistance = 0.4f;

    private NavMeshAgent agent;
    private int currentPointIndex;
    private float waitTimer;
    private bool isWaiting;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (patrolPoints == null || patrolPoints.Length == 0)
        {
            Debug.LogWarning(
                $"{nameof(EnemyPatrol)} on {name} has no patrol points assigned.",
                this
            );
            enabled = false;
            return;
        }

        MoveToCurrentPoint();
    }

    private void Update()
    {
        if (agent.pathPending || isWaiting)
        {
            UpdateWaitTimer();
            return;
        }

        if (agent.remainingDistance > waypointReachedDistance)
        {
            return;
        }

        isWaiting = true;
        waitTimer = waypointWaitTime;
        agent.isStopped = true;
    }

    private void UpdateWaitTimer()
    {
        if (!isWaiting)
        {
            return;
        }

        waitTimer -= Time.deltaTime;

        if (waitTimer > 0f)
        {
            return;
        }

        isWaiting = false;
        agent.isStopped = false;
        currentPointIndex = (currentPointIndex + 1) % patrolPoints.Length;
        MoveToCurrentPoint();
    }

    private void MoveToCurrentPoint()
    {
        Transform targetPoint = patrolPoints[currentPointIndex];

        if (targetPoint != null)
        {
            agent.SetDestination(targetPoint.position);
        }
    }
}