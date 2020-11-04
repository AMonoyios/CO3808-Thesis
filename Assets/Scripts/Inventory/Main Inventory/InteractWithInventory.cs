using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InteractWithInventory : MonoBehaviour, IPointerClickHandler
{
    //private TextMeshProUGUI ConsoleBoxGUI;

    [Header("Each inventory slot components")]
    public Image SlotIcon;
    public Button DeleteButton;
    public TextMeshProUGUI ItemCounter;

    // B21 fix, items clip and fall under the world when dropped
    [Header("Inventory interaction properties")]
    [Range(-2.0f, 2.0f)]
    public float DropOffsetX = 0.0f;
    [Range(-2.0f, 2.0f)]
    public float DropOffsetY = 0.5f;
    [Range(-2.0f, 2.0f)]
    public float DropOffsetZ = 0.0f;

    [Header("Gizmo Reference")]
    // Cache Gizmos Script
    public GameObject gizmoManager;
    GizmosManager gizmos;

    [HideInInspector]
    public Vector3 DropOffset;

    // Used the Player Instance Class to avoid slowdowns
    //private GameObject Player;
    GameObject playerInstance;

    ItemBlueprint item;

    private void Start()
    {
        playerInstance = PlayerManager.Instance.player;
        //Player = GameObject.Find("Player");

        DropOffset = new Vector3(DropOffsetX, DropOffsetY, DropOffsetZ);

        gizmoManager = GameObject.Find("GizmoManager");
        gizmos = gizmoManager.GetComponent<GizmosManager>();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (item != null)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
				// Left clicked on item (Using item)
				//if (FindConsoleBoxGUI())
                //    ConsoleBoxGUI.text += "DEBUG - INVENTORY: Mouse button left was pressed on " + Inventory.InventoryInstance.PrintItemName(item) + "\n";
                Debug.Log("DEBUG - INVENTORY: Mouse button left was pressed on " + Inventory.InventoryInstance.PrintItemName(item));

                item.UseItem();
            }
            else if (eventData.button == PointerEventData.InputButton.Right)
            {
				// Right clicked on item (Drop item)
				//if (FindConsoleBoxGUI())
                //    ConsoleBoxGUI.text += "DEBUG - INVENTORY: Mouse button right was pressed on " + Inventory.InventoryInstance.PrintItemName(item) + "\n";
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
		//if (FindConsoleBoxGUI())
        //    ConsoleBoxGUI.text += "DEBUG - ITEM: Dropping " + SlotItem.ItemName + "\n";
        Debug.Log("DEBUG - ITEM: Dropping " + SlotItem.ItemName);

        // Spawn item in world
        GameObject DroppedItem = (GameObject)Instantiate(SlotItem.itemPrefab, playerInstance.transform.position + DropOffset, playerInstance.transform.rotation);
        //GameObject DroppedItem = (GameObject)Instantiate(SlotItem.itemPrefab, Player.transform.position + DropOffset, Player.transform.rotation);

        // Add item to list of Gizmos
        gizmos.AddFocusObjToArray(DroppedItem);

        // Delete from inventory
        DeleteItemOnSlot();
    }

    public void DeleteItemOnSlot()
    {
        Inventory.InventoryInstance.RemoveFromInventory(item);
    }

    //bool FindConsoleBoxGUI()
    //{
    //    // B19 fix, because the script inherits from another one instead of monobehaviour i can 
    //    //  not trigger awake when the object is being instansiated. TODO: try finding a more 
    //    //  efficient way to get the text meshproGUI instead of find();
    //
    //    // B20 when the custom console box is inactive it does not find the gameobject
    //    try
    //    {
    //        GameObject CustomConsoleBox = GameObject.Find("Developer Console");
    //        ConsoleBoxGUI = CustomConsoleBox.GetComponent<TextMeshProUGUI>();
    //    }
    //    catch (System.Exception)
    //    {
    //        return false;
    //    }
    //
    //    return true;
    //}
}
