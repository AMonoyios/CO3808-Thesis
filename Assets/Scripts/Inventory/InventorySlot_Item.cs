using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot_Item : MonoBehaviour
{
    public Image SlotIcon;
    public Button DeleteButton;

    // referencing the Blueprint Scriptable object to "assing" it top this particular slot
    ItemBlueprint item;

    public void AddItemToSlot(ItemBlueprint SlotItem)
    {
        item = SlotItem;
        SlotIcon.sprite = item.ItemIcon;
        SlotIcon.enabled = true;
        DeleteButton.interactable = true;
    }

    public void ResetSlot()
    {
        item = null;
        SlotIcon.sprite = null;
        SlotIcon.enabled = false;
        DeleteButton.interactable = false;
    }

    public void DeleteItemOnSlot()
    {
        Inventory.InventoryInstance.RemoveFromInventory(item);
    }

    public void UseItemOnSlot()
    {
        if (item != null)
        {
            item.UseItem();
        }
    }
}
