using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public virtual string GetPrompt(PlayerInventory inventory)
    {
        return "Interact";
    }

    public abstract void Interact(PlayerInventory inventory);
}