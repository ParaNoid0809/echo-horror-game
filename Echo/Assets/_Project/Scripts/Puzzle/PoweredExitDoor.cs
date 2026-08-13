using System.Collections;
using UnityEngine;

public sealed class PoweredExitDoor : MonoBehaviour
{
    [SerializeField] private Transform doorPivot;
    [SerializeField] private float openAngle = 90f;
    [SerializeField, Min(0.05f)] private float openDuration = 0.75f;

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

    public void Open()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;
        StartCoroutine(OpenRoutine());
    }

    private IEnumerator OpenRoutine()
    {
        Quaternion targetRotation =
            closedRotation * Quaternion.Euler(0f, openAngle, 0f);

        float elapsed = 0f;

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;

            float progress = Mathf.SmoothStep(
                0f,
                1f,
                elapsed / openDuration
            );

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