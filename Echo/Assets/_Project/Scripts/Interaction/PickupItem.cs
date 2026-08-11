using UnityEngine;

public sealed class PickupItem : Interactable
{
    [SerializeField] private ItemData item;

    public override string GetPrompt(PlayerInventory inventory)
    {
        return item == null ? string.Empty : $"Pick up {item.DisplayName} [E]";
    }

    public override void Interact(PlayerInventory inventory)
    {
        if (inventory != null && inventory.TryAdd(item))
        {
            Destroy(gameObject);
        }
    }
}