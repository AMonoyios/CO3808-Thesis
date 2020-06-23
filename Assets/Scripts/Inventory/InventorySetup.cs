using UnityEngine;

public class InventorySetup : MonoBehaviour
{
    // Cache the inventory instancing
    Inventory inventory;

    public Transform InventoryBag;
    public GameObject InventoryUI;
    private bool InventoryToggleState;

    InventorySlot_Item[] InventorySlots;

    // Start is called before the first frame update
    void Start()
    {
        // Update the Inventory UI whenever the player interacts with the inventory
        inventory = Inventory.InventoryInstance;
        inventory.CallItemUpdated += UpdateInventory_UI;

        // Get the Components of the Inventory Bag
        InventorySlots = InventoryBag.GetComponentsInChildren<InventorySlot_Item>();

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
        Debug.Log("DEBUG: Updating inventory UI");

        for (int i = 0; i < InventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                InventorySlots[i].AddItemToSlot(inventory.items[i]);
            }
            else
            {
                InventorySlots[i].ResetSlot();
            }
        }
    }
}
