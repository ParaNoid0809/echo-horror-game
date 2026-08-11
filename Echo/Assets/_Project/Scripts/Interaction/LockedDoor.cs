using System.Collections;
using UnityEngine;

public sealed class LockedDoor : Interactable
{
    [SerializeField] private ItemData requiredKey;
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField, Min(0.05f)] private float openDuration = 0.5f;

    private bool isOpen;
    private Quaternion closedRotation;

    private void Awake()
    {
        if (doorPivot == null)
        {
            doorPivot = transform;
        }

        closedRotation = doorPivot.localRotation;
    }

    public override string GetPrompt(PlayerInventory inventory)
    {
        if (isOpen)
        {
            return string.Empty;
        }

        return inventory != null && inventory.HasItem(requiredKey)
            ? "Unlock door [E]"
            : $"Locked — requires {requiredKey.DisplayName}";
    }

    public override void Interact(PlayerInventory inventory)
    {
        if (isOpen || inventory == null || !inventory.HasItem(requiredKey))
        {
            return;
        }

        isOpen = true;
        StartCoroutine(OpenDoor());
    }

    private IEnumerator OpenDoor()
    {
        Quaternion targetRotation =
            closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        float elapsed = 0f;

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.SmoothStep(0f, 1f, elapsed / openDuration);

            doorPivot.localRotation = Quaternion.Slerp(
                closedRotation,
                targetRotation,
                progress
            );

            yield return null;
        }

        doorPivot.localRotation = targetRotation;
    }
}