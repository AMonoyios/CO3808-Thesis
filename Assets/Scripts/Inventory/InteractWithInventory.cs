using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractWithInventory : MonoBehaviour, IPointerClickHandler
{
    public Image SlotIcon;
    public Button DeleteButton;
    
    public ItemBlueprint Item { get => item; set => item = value; }
    ItemBlueprint item;
    
    //[HideInInspector]
    //public ItemBlueprint Item { get => item; set => item = value; }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // Left clicked on item (Using item)
                Debug.Log("DEBUG - INVENTORY: Mouse button left was pressed");

                item.UseItem();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                // Right clicked on item (Draging item)
                Debug.Log("DEBUG - INVENTORY: Mouse button right was pressed");
            }
        }
    }

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
}
