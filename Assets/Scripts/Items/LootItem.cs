using UnityEngine;

public class LootItem : InteractPoint
{
    public ItemBlueprint itemBlueprint;
    public FocusController focusController;

    // In order to avoid having to drag and drop each Gameobject to every single item we create, 
    //  we can use GetComponent and search all the game files for the specific gameobject, this
    //  is not a perfect solution but we only do it once right before the game starts. 
    void Awake()
    {
        GameObject Player = GameObject.Find("Player");
        focusController = Player.GetComponent<FocusController>();
    }

    public override void Interact()
    {
        base.Interact();

        PickUpItem();
    }

    void PickUpItem()
    {
        // Add item to inventory
        Debug.Log("DEBUG: Added " + itemBlueprint.name + " to inventory");
        bool successfulPickup = Inventory.InventoryInstance.AddToInventory(itemBlueprint);

        // Delete item from game world
        if (successfulPickup == true)
        {
            Destroy(gameObject);
        }

        // Stop Focusing the deleted item
        focusController.DeFocus();
    }
}
