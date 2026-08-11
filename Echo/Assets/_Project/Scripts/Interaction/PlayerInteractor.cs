using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class PlayerInteractor : MonoBehaviour
{
    [SerializeField] private Camera playerCamera;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField, Min(0.1f)] private float interactionRange = 3f;
    [SerializeField] private LayerMask interactionLayers = ~0;

    private Interactable currentInteractable;

    private void Awake()
    {
        if (playerCamera == null)
        {
            playerCamera = GetComponentInChildren<Camera>();
        }

        if (inventory == null)
        {
            inventory = GetComponent<PlayerInventory>();
        }

        SetPrompt(string.Empty);
    }

    private void Update()
    {
        Interactable candidate = FindInteractable();

        if (candidate != currentInteractable)
        {
            currentInteractable = candidate;
            SetPrompt(
                currentInteractable == null
                    ? string.Empty
                    : currentInteractable.GetPrompt(inventory)
            );
        }

        if (currentInteractable != null &&
            Keyboard.current?.eKey.wasPressedThisFrame == true)
        {
            currentInteractable.Interact(inventory);
            currentInteractable = null;
            SetPrompt(string.Empty);
        }
    }

    private Interactable FindInteractable()
    {
        if (playerCamera == null)
        {
            return null;
        }

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (!Physics.Raycast(
                ray,
                out RaycastHit hit,
                interactionRange,
                interactionLayers,
                QueryTriggerInteraction.Ignore))
        {
            return null;
        }

        return hit.collider.GetComponentInParent<Interactable>();
    }

    private void SetPrompt(string message)
    {
        if (promptText != null)
        {
            promptText.text = message;
        }
    }
}