﻿using UnityEngine;
using TMPro;

public class LootItem : InteractPoint
{
    [Header("Item Specific Properties")]
    [Header("Loot Scriptable Object")]
    public ItemBlueprint itemBlueprint;
    
    private FocusController focusController;

    private TextMeshProUGUI ConsoleBoxGUI;

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

        FindConsoleBoxGUI();

        PickUpItem();
    }

    void PickUpItem()
    {
        // Add item to inventory
        ConsoleBoxGUI.text += "DEBUG - ITEM: Added " + itemBlueprint.name + " to inventory \n";
        Debug.Log("DEBUG - ITEM: Added " + itemBlueprint.name + " to inventory");
        bool successfulPickup = Inventory.InventoryInstance.AddToInventory(itemBlueprint);

        // Delete item from game world
        if (successfulPickup == true)
        {
            Destroy(gameObject);
        }

        // Stop Focusing the deleted item
        focusController.DeFocus();
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
