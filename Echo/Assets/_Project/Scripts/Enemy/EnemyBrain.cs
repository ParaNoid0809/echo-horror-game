using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public sealed class EnemyBrain : MonoBehaviour
{
    private enum EnemyState
    {
        Patrol,
        Investigate,
        Chase,
        Search
    }

    [Header("References")]
    [SerializeField] private EnemyPatrol patrol;
    [SerializeField] private EnemyVision vision;
    [SerializeField] private EnemyHearing hearing;

    [Header("Behaviour")]
    [SerializeField, Min(0.1f)] private float investigateDistance = 0.5f;
    [SerializeField, Min(0.1f)] private float searchDuration = 4f;
    [SerializeField, Min(0.1f)] private float lostSightDuration = 2f;
    [SerializeField, Min(0.1f)] private float chaseSpeed = 4f;

    private NavMeshAgent agent;
    private EnemyState currentState;
    private Vector3 lastKnownPlayerPosition;
    private float stateTimer;
    private float lostSightTimer;
    private float patrolSpeed;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        if (patrol == null) patrol = GetComponent<EnemyPatrol>();
        if (vision == null) vision = GetComponent<EnemyVision>();
        if (hearing == null) hearing = GetComponent<EnemyHearing>();

        patrolSpeed = agent.speed;
    }

    private void Start()
    {
        if (patrol == null || vision == null || hearing == null)
        {
            Debug.LogError(
                "EnemyBrain requires EnemyPatrol, EnemyVision, and EnemyHearing.",
                this
            );

            enabled = false;
            return;
        }

        SetState(EnemyState.Patrol);
    }

    private void Update()
    {
        UpdateDetection();

        switch (currentState)
        {
            case EnemyState.Patrol:
                break;

            case EnemyState.Investigate:
                UpdateInvestigate();
                break;

            case EnemyState.Chase:
                UpdateChase();
                break;

            case EnemyState.Search:
                UpdateSearch();
                break;
        }
    }

    private void UpdateDetection()
    {
        if (vision.CanSeePlayer)
        {
            lastKnownPlayerPosition = vision.PlayerPosition;
            lostSightTimer = 0f;

            if (currentState != EnemyState.Chase)
            {
                SetState(EnemyState.Chase);
            }

            return;
        }

        if (hearing.CanHearPlayer && currentState != EnemyState.Chase)
        {
            lastKnownPlayerPosition = hearing.LastHeardPosition;

            if (currentState != EnemyState.Investigate)
            {
                SetState(EnemyState.Investigate);
            }
        }
    }

    private void UpdateInvestigate()
    {
        if (vision.CanSeePlayer)
        {
            SetState(EnemyState.Chase);
            return;
        }

        if (hearing.CanHearPlayer)
        {
            lastKnownPlayerPosition = hearing.LastHeardPosition;
            agent.SetDestination(lastKnownPlayerPosition);
        }

        if (!agent.pathPending &&
            agent.remainingDistance <= investigateDistance)
        {
            SetState(EnemyState.Search);
        }
    }

    private void UpdateChase()
    {
        if (vision.CanSeePlayer)
        {
            lastKnownPlayerPosition = vision.PlayerPosition;
            agent.SetDestination(lastKnownPlayerPosition);
            lostSightTimer = 0f;
            return;
        }

        lostSightTimer += Time.deltaTime;

        if (lostSightTimer >= lostSightDuration)
        {
            SetState(EnemyState.Investigate);
        }
    }

    private void UpdateSearch()
    {
        if (vision.CanSeePlayer)
        {
            SetState(EnemyState.Chase);
            return;
        }

        if (hearing.CanHearPlayer)
        {
            lastKnownPlayerPosition = hearing.LastHeardPosition;
            SetState(EnemyState.Investigate);
            return;
        }

        stateTimer -= Time.deltaTime;

        if (stateTimer <= 0f)
        {
            SetState(EnemyState.Patrol);
        }
    }

    private void SetState(EnemyState newState)
    {
        currentState = newState;
        agent.isStopped = false;

        bool usePatrol = newState == EnemyState.Patrol;
        patrol.enabled = usePatrol;

        switch (newState)
        {
            case EnemyState.Patrol:
                agent.speed = patrolSpeed;
                break;

            case EnemyState.Investigate:
                agent.speed = patrolSpeed;
                agent.SetDestination(lastKnownPlayerPosition);
                break;

            case EnemyState.Chase:
                agent.speed = chaseSpeed;
                agent.SetDestination(lastKnownPlayerPosition);
                break;

            case EnemyState.Search:
                agent.speed = 0f;
                agent.isStopped = true;
                stateTimer = searchDuration;
                break;
        }
    }
}