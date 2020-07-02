using UnityEngine;

public class InventorySetup : MonoBehaviour
{
    // Cache the inventory instancing
    Inventory inventory;

    public Transform InventoryBag;
    public GameObject InventoryUI;
    private bool InventoryToggleState;

    InteractWithInventory[] InventorySlots;
    //InventorySlot_Item[] InventorySlots;


    // Start is called before the first frame update
    void Start()
    {
        // Update the Inventory UI whenever the player interacts with the inventory
        inventory = Inventory.InventoryInstance;
        inventory.CallItemUpdated += UpdateInventory_UI;

        // Get the Components of the Inventory Bag
        InventorySlots = InventoryBag.GetComponentsInChildren<InteractWithInventory>();

        // Close the inventory when the game starts
        InventoryToggleState = false;
        InventoryUI.SetActive(InventoryToggleState);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryToggleState = !InventoryToggleState;
            InventoryUI.SetActive(InventoryToggleState);
        }   
    }

    void UpdateInventory_UI()
    {
        Debug.Log("DEBUG - INVENTORY: Updating inventory UI");
    
        for (int i = 0; i < InventorySlots.Length; i++)
        {
            if (i < inventory.InventoryItems.Count)
            {
                InventorySlots[i].AddItemToSlot(inventory.InventoryItems[i].item);

                // Fixed B15 - Counter displaying incorrect item quantity in inventory
                if (inventory.InventoryItems[i].itemQuantity > 1)
                {
                    InventorySlots[i].ItemCounter.SetText(inventory.InventoryItems[i].itemQuantity.ToString());
                }
                else
                {
                    InventorySlots[i].ItemCounter.SetText("");
                }
            }
            else
            {
                InventorySlots[i].ResetSlot();
            }
        }
    }
}
