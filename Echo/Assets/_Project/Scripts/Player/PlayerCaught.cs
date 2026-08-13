using UnityEngine;

public sealed class PlayerCaught : MonoBehaviour
{
    [SerializeField] private FirstPersonController controller;
    [SerializeField] private PlayerInteractor interactor;
    [SerializeField] private GameObject gameOverPanel;

    public bool IsCaught { get; private set; }

    private void Awake()
    {
        if (controller == null)
        {
            controller = GetComponent<FirstPersonController>();
        }

        if (interactor == null)
        {
            interactor = GetComponent<PlayerInteractor>();
        }

        SetCaught(false);
    }

    public void SetCaught(bool caught)
    {
      

        IsCaught = caught;

        if (controller != null)
        {
            controller.enabled = !caught;
        }

        if (interactor != null)
        {
            interactor.enabled = !caught;
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(caught);
        }

        Time.timeScale = caught ? 0f : 1f;
        Cursor.lockState = caught
            ? CursorLockMode.None
            : CursorLockMode.Locked;
        Cursor.visible = caught;
    }
}