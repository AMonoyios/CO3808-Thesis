using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryItemSlotHolder : MonoBehaviour, IDropHandler
{
    public ItemBlueprint Item { get => item; set => item = value; }
    ItemBlueprint item;

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            Debug.Log("DEBUG - INVENTORY: Item " + item.ItemName + " dropped in inventory slot");
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            throw new System.NotImplementedException();
        }
    }
}
