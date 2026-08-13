using UnityEngine;

public sealed class EnemyCatchPlayer : MonoBehaviour
{
    [SerializeField] private PlayerCaught playerCaught;
    [SerializeField, Min(0.1f)] private float catchDistance = 1.2f;

    private void Update()
    {
        if (playerCaught == null || playerCaught.IsCaught)
        {
            return;
        }

        Vector3 enemyPosition = transform.position;
        Vector3 playerPosition = playerCaught.transform.position;

        enemyPosition.y = 0f;
        playerPosition.y = 0f;

        float squaredDistance =
            (enemyPosition - playerPosition).sqrMagnitude;

        if (squaredDistance <= catchDistance * catchDistance)
        {
            playerCaught.SetCaught(true);
        }
    }
}