using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InteractWithInventory : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI ConsoleBoxGUI;

    [Header("Each inventory slot components")]
    public Image SlotIcon;
    public Button DeleteButton;
    public TextMeshProUGUI ItemCounter;

    private GameObject Player;

    ItemBlueprint item;

    private void Awake()
    {
        FindConsoleBoxGUI();
    }

    private void Start()
    {
        Player = GameObject.Find("Player");
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                // Left clicked on item (Using item)
                ConsoleBoxGUI.text += "DEBUG - INVENTORY: Mouse button left was pressed on " + Inventory.InventoryInstance.PrintItemName(item) + "\n";
                Debug.Log("DEBUG - INVENTORY: Mouse button left was pressed on " + Inventory.InventoryInstance.PrintItemName(item));

                item.UseItem();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
                // Right clicked on item (Drop item)
                ConsoleBoxGUI.text += "DEBUG - INVENTORY: Mouse button right was pressed on " + Inventory.InventoryInstance.PrintItemName(item) + "\n";
                Debug.Log("DEBUG - INVENTORY: Mouse button right was pressed on " + Inventory.InventoryInstance.PrintItemName(item));

                DropItem(item);
            }
        }
    }

    public void AddItemToSlot(ItemBlueprint SlotItem)
    {
        item = SlotItem;
        SlotIcon.sprite = item.ItemIcon;
        SlotIcon.enabled = true;
        DeleteButton.interactable = true;
        ItemCounter.enabled = true;
    }

    public void ResetSlot()
    {
        item = null;
        SlotIcon.sprite = null;
        SlotIcon.enabled = false;
        DeleteButton.interactable = false;
        ItemCounter.enabled = false;
    }

    public void DropItem(ItemBlueprint SlotItem)
    {
        ConsoleBoxGUI.text += "DEBUG - ITEM: Dropping " + SlotItem.ItemName + "\n";
        Debug.Log("DEBUG - ITEM: Dropping " + SlotItem.ItemName);

        GameObject itemName = (GameObject)Instantiate(SlotItem.itemPrefab, Player.transform.position, Player.transform.rotation);
        DeleteItemOnSlot();
    }

    public void DeleteItemOnSlot()
    {
        Inventory.InventoryInstance.RemoveFromInventory(item);
    }

    void FindConsoleBoxGUI()
    {
        // B19 fix, because the script inherits from another one instead of monobehaviour i can 
        //  not trigger awake when the object is being instansiated. TODO: try finding a more 
        //  efficient way to get the text meshproGUI instead of find();
        GameObject CustomConsoleBox = GameObject.Find("Developer Console");
        ConsoleBoxGUI = CustomConsoleBox.GetComponent<TextMeshProUGUI>();
    }
}
