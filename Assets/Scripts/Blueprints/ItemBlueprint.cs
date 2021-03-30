using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Item", menuName = "Custom Scriptable Objects/Game Objects/Item")]
public class ItemBlueprint : ScriptableObject
{
    public EquipmentBlueprint equipBP;

    [Header("Item specific properties")]
    // Properties for all Item types
    public string ItemName = "New Item";
    public Sprite ItemIcon = null;
    public bool isDefault = false;
    public int StackUntil = 1;
    public GameObject itemPrefab = null;
    public string ItemDescription;
    
    [HideInInspector]
    public string Bundle = "Bundle";
    [HideInInspector]
    public string AssetName = "Asset_Name";

    // The use for each item
    public virtual void UseItem()
    {
        Debug.Log("DEBUG - ITEM: Using " + ItemName);
    }

    public void RemoveItem()
    {
        Inventory.InventoryInstance.RemoveFromInventory(this);
    }
}