﻿using UnityEngine;
using TMPro;

public class LootCurrency : InteractPoint
{
    [Header("Item Specific Properties")]
    [Header("Currency Scriptable Object")]
    public CurrencyBlueprint currency;

    private FocusController focusController;
    private AllCharacterStats characterStats;

    private TextMeshProUGUI ConsoleBoxGUI;

    // In order to avoid having to drag and drop each Gameobject to every single coin we create, 
    //  we can use GetComponent and search all the game files for the specific gameobject, this
    //  is not a perfect solution but we only do it once right before the game starts. 
    void Awake()
    {
        GameObject Player = GameObject.Find("Player");
        focusController = Player.GetComponent<FocusController>();
        characterStats = Player.GetComponent<AllCharacterStats>();
    }

    public override void Interact()
    {
        FindConsoleBoxGUI();
        
        base.Interact();

        PickUpCurrency();
    }

    void PickUpCurrency()
    {
        // Add to balance
        ConsoleBoxGUI.text += "DEBUG - CURRENCY: Added " + currency.name + " to your balance \n";
        Debug.Log("DEBUG - CURRENCY: Added " + currency.name + " to your balance");
        characterStats.Balance += currency.CurrencyValue;

        // Delete from game world
        ConsoleBoxGUI.text += "DEBUG - CURRENCY: Deleting " + gameObject.name + "\n";
        Debug.Log("DEBUG - CURRENCY: Deleting " + gameObject.name);
        Destroy(gameObject);

        // Stop Focusing the deleted currency
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
