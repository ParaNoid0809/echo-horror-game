using UnityEngine;

[CreateAssetMenu(fileName = "Item_", menuName = "ECHO/Inventory Item")]
public sealed class ItemData : ScriptableObject
{
    [SerializeField] private string displayName = "New Item";

    public string DisplayName => displayName;
}