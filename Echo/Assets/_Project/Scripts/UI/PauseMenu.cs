using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private FirstPersonController playerController;
    [SerializeField] private PlayerInteractor playerInteractor;

    private bool isPaused;

    private void Awake()
    {
        SetPaused(false);
    }

    private void Update()
    {
        if (Keyboard.current?.escapeKey.wasPressedThisFrame == true)
        {
            SetPaused(!isPaused);
        }
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    private void SetPaused(bool paused)
    {
        isPaused = paused;
        Time.timeScale = paused ? 0f : 1f;

        if (pausePanel != null)
        {
            pausePanel.SetActive(paused);
        }

        if (playerController != null)
        {
            playerController.enabled = !paused;
        }

        if (playerInteractor != null)
        {
            playerInteractor.enabled = !paused;
        }

        Cursor.lockState = paused
            ? CursorLockMode.None
            : CursorLockMode.Locked;

        Cursor.visible = paused;
    }
}