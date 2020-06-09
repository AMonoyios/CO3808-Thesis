using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	#region InventorySingleton
	// Create a singleton for the item in order to let the LootItem script which specific 
	//  item is being added or removed from the inventory. We could have used Find(...) but
	//  every frame it would try to find the specific item in all the game files
	public static Inventory InventoryInstance;
    void Awake() { InventoryInstance = this; }
	#endregion
    
    // Creating the list for the total items in the inventory
    public int InventorySlots = 10;
    public List<ItemBlueprint> items = new List<ItemBlueprint>();

    // In order to update the inventory UI without using the function in the Update() we can 
    //  use a delegate
    public delegate void ItemUpdated();
    public ItemUpdated CallItemUpdated;

    // Adding the item to the list
    public bool AddToInventory(ItemBlueprint item)
    {
        // if an item is a default one then the item is already in "use" so there is no point
        //  of wasting space in inventory for it. If we want to display the items that are in
        //  "use" then we can remove this if statement
        if (item.isDefault == false)
        {
            if (items.Count < InventorySlots)
            {
                items.Add(item);

                // update the inventory UI
                CallItemUpdated.Invoke();
            }
            else
            {
                Debug.Log("DEBUG: Inventory does not have enough space for " + item.ItemName);
                return false;
            }
        }
        Debug.Log("Test");
        return true;
    }

    // Removing the item from the list
    public void RemoveFromInventory(ItemBlueprint item)
    {
        items.Remove(item);

        // update the inventory UI
        CallItemUpdated.Invoke();
    }
}
