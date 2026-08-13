using UnityEngine;
using UnityEngine.Events;

public sealed class GeneratorFuseSocket : Interactable
{
    [Header("Required item")]
    [SerializeField] private ItemData requiredFuse;

    [Header("Visuals")]
    [SerializeField] private GameObject insertedFuseVisual;

    [Header("Puzzle event")]
    [SerializeField] private UnityEvent onFuseInserted;

    private bool isPowered;

    private void Awake()
    {
        if (insertedFuseVisual != null)
        {
            insertedFuseVisual.SetActive(false);
        }
    }

    public override string GetPrompt(PlayerInventory inventory)
    {
        if (isPowered)
        {
            return "Generator powered";
        }

        if (inventory != null && inventory.HasItem(requiredFuse))
        {
            return "Insert Generator Fuse [E]";
        }

        return "Generator requires a fuse";
    }

    public override void Interact(PlayerInventory inventory)
    {
        if (isPowered || inventory == null || !inventory.HasItem(requiredFuse))
        {
            return;
        }

        isPowered = true;

        if (insertedFuseVisual != null)
        {
            insertedFuseVisual.SetActive(true);
        }

        onFuseInserted?.Invoke();
    }
}