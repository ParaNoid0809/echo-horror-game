using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerInventory : MonoBehaviour
{
    [SerializeField] private List<ItemData> items = new();

    private readonly HashSet<ItemData> itemLookup = new();

    private void Awake()
    {
        foreach (ItemData item in items)
        {
            if (item != null)
            {
                itemLookup.Add(item);
            }
        }
    }

    public bool TryAdd(ItemData item)
    {
        if (item == null || !itemLookup.Add(item))
        {
            return false;
        }

        items.Add(item);
        return true;
    }

    public bool HasItem(ItemData item)
    {
        return item != null && itemLookup.Contains(item);
    }
}