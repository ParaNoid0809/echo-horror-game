using UnityEngine;

[RequireComponent(typeof(Collider))]
public sealed class EscapeTrigger : MonoBehaviour
{
    [SerializeField] private GameObject escapePanel;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private PlayerInteractor playerInteractor;

    private bool hasEscaped;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;

        if (escapePanel != null)
        {
            escapePanel.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasEscaped || other.GetComponent<PlayerCheckpoint>() == null)
        {
            return;
        }

        hasEscaped = true;

        if (playerController == null)
        {
            playerController =
                other.GetComponent<FirstPersonController>();
        }

        if (playerInteractor == null)
        {
            playerInteractor =
                other.GetComponent<PlayerInteractor>();
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        if (playerInteractor != null)
        {
            playerInteractor.enabled = false;
        }

        if (escapePanel != null)
        {
            escapePanel.SetActive(true);
        }

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}