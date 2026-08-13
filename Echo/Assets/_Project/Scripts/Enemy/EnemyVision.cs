using UnityEngine;

public sealed class EnemyVision : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Vision")]
    [SerializeField, Min(0.1f)] private float viewDistance = 8f;
    [SerializeField, Range(1f, 360f)] private float viewAngle = 100f;
    [SerializeField] private LayerMask obstructionLayers = ~0;
    [SerializeField] private float eyeHeight = 1.5f;
    [SerializeField] private float targetHeight = 1.2f;

    public bool CanSeePlayer { get; private set; }
    public Vector3 PlayerPosition =>
    player != null ? player.position : transform.position;

    private void Update()
    {
        CanSeePlayer = CheckForPlayer();
    }

    private bool CheckForPlayer()
    {
        if (player == null)
        {
            return false;
        }

        Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;
        Vector3 targetPosition = player.position + Vector3.up * targetHeight;
        Vector3 directionToPlayer = targetPosition - eyePosition;

        float distanceToPlayer = directionToPlayer.magnitude;

        if (distanceToPlayer > viewDistance)
        {
            return false;
        }

        Vector3 flatForward = transform.forward;
        flatForward.y = 0f;

        Vector3 flatDirection = directionToPlayer;
        flatDirection.y = 0f;

        if (flatDirection.sqrMagnitude < 0.001f)
        {
            return true;
        }

        float angleToPlayer = Vector3.Angle(
            flatForward.normalized,
            flatDirection.normalized
        );

        if (angleToPlayer > viewAngle * 0.5f)
        {
            return false;
        }

        if (Physics.Raycast(
                eyePosition,
                directionToPlayer.normalized,
                out RaycastHit hit,
                distanceToPlayer,
                obstructionLayers,
                QueryTriggerInteraction.Ignore))
        {
            return hit.transform == player || hit.transform.IsChildOf(player);
        }

        return true;
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 eyePosition = transform.position + Vector3.up * eyeHeight;

        Gizmos.color = CanSeePlayer ? Color.red : Color.yellow;
        Gizmos.DrawWireSphere(eyePosition, viewDistance);

        Vector3 leftBoundary = Quaternion.Euler(0f, -viewAngle * 0.5f, 0f)
            * transform.forward;

        Vector3 rightBoundary = Quaternion.Euler(0f, viewAngle * 0.5f, 0f)
            * transform.forward;

        Gizmos.DrawLine(eyePosition, eyePosition + leftBoundary * viewDistance);
        Gizmos.DrawLine(eyePosition, eyePosition + rightBoundary * viewDistance);
    }
}