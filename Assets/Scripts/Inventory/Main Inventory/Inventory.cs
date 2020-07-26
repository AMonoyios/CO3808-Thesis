using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public class InventoryItems
{
    public ItemBlueprint item;
    public int itemQuantity;

    public InventoryItems(ItemBlueprint _item, int _itemQuantity)
    {
        item = _item;
        itemQuantity = _itemQuantity;
    }
}

public class Inventory : MonoBehaviour
{
	#region InventorySingleton
	// Create a singleton for the item in order to let the LootItem script which specific 
	//  item is being added or removed from the inventory. We could have used Find(...) but
	//  every frame it would try to find the specific item in all the game files
	public static Inventory InventoryInstance;
    void Awake() 
    { 
        InventoryInstance = this;
    }
    #endregion

    private TextMeshProUGUI ConsoleBoxGUI;

    // Creating the list for the total items in the inventory
    public int InventorySlots = 15;
    public List<InventoryItems> InventoryItems = new List<InventoryItems>();

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
            if (InventoryItems.Count < InventorySlots)
            {
				if (FindConsoleBoxGUI())
                    ConsoleBoxGUI.text += "DEBUG - INVENTORY: Inventory has " + (InventorySlots - InventoryItems.Count) + " slot(s) left \n";
                Debug.Log("DEBUG - INVENTORY: Inventory has " + (InventorySlots-InventoryItems.Count) + " slot(s) left");

                if (InventoryItems.Count > 0)
                {
                    if (!SuccessfullyStackedItem(item))
                    {
                        InventoryItems.Add(new InventoryItems(item, 1));
                    }
                }
                else
                {
                    InventoryItems.Add(new InventoryItems(item, 1));
                }

                //update the inventory UI
                if (CallItemUpdated != null)    // B9 fix
                {
                    CallItemUpdated.Invoke();
                }
            }
            else
            {
				if (FindConsoleBoxGUI())
                    ConsoleBoxGUI.text += "DEBUG - INVENTORY: Inventory does not have enough space for " + item.ItemName + "\n";
                Debug.Log("DEBUG - INVENTORY: Inventory does not have enough space for " + item.ItemName);
                return false;
            }
        }
        return true;
    }

    // Removing the item from the list
    public void RemoveFromInventory(ItemBlueprint item)
    {
        if (FindSpecificItem(item) >= 0)
        {
            int index = FindSpecificItem(item);

            // check if we have multible items stacked at the same slot
            if (InventoryItems[index].itemQuantity > 1)
            {
                InventoryItems[index].itemQuantity--;
            }
            else
            {
                InventoryItems.RemoveAt(index);
            }
        }

        // update the inventory UI
        if (CallItemUpdated != null)    // B9 fix
        {
            CallItemUpdated.Invoke();
        }
    }

    public bool SuccessfullyStackedItem(ItemBlueprint item)
    {
        for (int i = 0; i < InventoryItems.Count; i++)
        {
			if (FindConsoleBoxGUI())
                ConsoleBoxGUI.text += "DEBUG - INVENTORY: inventory checking at index -> " + i + "\n";
            Debug.Log("DEBUG - INVENTORY: item stacking checking at index -> " + i);

            if (InventoryItems[i].item.ItemName == item.ItemName && InventoryItems[i].itemQuantity < item.StackUntil)
            {
                InventoryItems[i].itemQuantity += 1;
                
                return true;
            }
        }

        return false;
    }

    private int FindSpecificItem(ItemBlueprint item)
    {
        for (int i = 0; i < InventoryItems.Count; i++)
        {
			if (FindConsoleBoxGUI())
                ConsoleBoxGUI.text += "DEBUG - INVENTORY: item in inventory checking at index -> " + i + "\n";
            Debug.Log("DEBUG - INVENTORY: item in inventory checking at index -> " + i);

            if (InventoryItems[i].item.ItemName == item.ItemName)
            {
                return i;
            }
        }

        return 0;
    }

    // ONLY FOR TESTING
    public string PrintItemName(ItemBlueprint item)
    {
        if (FindSpecificItem(item) >= 0)
        {
            int index = FindSpecificItem(item);

            return InventoryItems[index].item.ItemName;
        }
        else
        {
			if (FindConsoleBoxGUI())
                ConsoleBoxGUI.text += "WARNING - INVENTORY: Item name not found! \n";
            Debug.LogWarning("WARNING - INVENTORY: Item name not found!");
            return "ITEM NOT FOUND";
        }
    }

    bool FindConsoleBoxGUI()
    {
        // B19 fix, because the script inherits from another one instead of monobehaviour i can 
        //  not trigger awake when the object is being instansiated. TODO: try finding a more 
        //  efficient way to get the text meshproGUI instead of find();

        // B20 when the custom console box is inactive it does not find the gameobject
        try
        {
            GameObject CustomConsoleBox = GameObject.Find("Developer Console");
            ConsoleBoxGUI = CustomConsoleBox.GetComponent<TextMeshProUGUI>();
        }
        catch (System.Exception)
        {
            return false;
        }

        return true;
    }
}
