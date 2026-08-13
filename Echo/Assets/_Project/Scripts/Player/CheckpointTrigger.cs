using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class CheckpointTrigger : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject activatedVisual;

    private bool hasActivated;

    private void Awake()
    {
        Collider triggerCollider = GetComponent<Collider>();
        triggerCollider.isTrigger = true;

        if (spawnPoint == null)
        {
            spawnPoint = transform;
        }

        if (activatedVisual != null)
        {
            activatedVisual.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasActivated)
        {
            return;
        }

        PlayerCheckpoint playerCheckpoint =
            other.GetComponent<PlayerCheckpoint>();

        if (playerCheckpoint == null)
        {
            return;
        }

        hasActivated = true;

        playerCheckpoint.SaveCheckpoint(
            spawnPoint.position,
            spawnPoint.rotation
        );

        if (activatedVisual != null)
        {
            activatedVisual.SetActive(true);
        }

        Debug.Log("Checkpoint activated.", this);
    }
}