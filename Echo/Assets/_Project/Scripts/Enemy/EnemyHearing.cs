using UnityEngine;

public sealed class EnemyHearing : MonoBehaviour
{
    [SerializeField] private PlayerNoise playerNoise;

    public bool CanHearPlayer { get; private set; }
    public Vector3 LastHeardPosition { get; private set; }

    private void Update()
    {
        CanHearPlayer = CheckForNoise();

        if (CanHearPlayer)
        {
            LastHeardPosition = playerNoise.transform.position;
        }
    }

    private bool CheckForNoise()
    {
        if (playerNoise == null || !playerNoise.IsMakingNoise)
        {
            return false;
        }

        float hearingRange = playerNoise.NoiseRadius;

        return (transform.position - playerNoise.transform.position).sqrMagnitude
            <= hearingRange * hearingRange;
    }

    private void OnDrawGizmosSelected()
    {
        if (playerNoise == null)
        {
            return;
        }

        Gizmos.color = CanHearPlayer ? Color.red : Color.cyan;
        Gizmos.DrawWireSphere(
            playerNoise.transform.position,
            playerNoise.NoiseRadius
        );
    }
}