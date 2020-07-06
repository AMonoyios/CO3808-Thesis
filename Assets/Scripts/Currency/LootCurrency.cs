using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LootCurrency : InteractPoint
{
    //private TextMeshProUGUI DeveloperConsoleBox;
    
    public CurrencyBlueprint currency;
    public FocusController focusController;
    public AllCharacterStats characterStats;

    // In order to avoid having to drag and drop each Gameobject to every single coin we create, 
    //  we can use GetComponent and search all the game files for the specific gameobject, this
    //  is not a perfect solution but we only do it once right before the game starts. 
    void Awake()
    {
        GameObject Player = GameObject.Find("Player");
        focusController = Player.GetComponent<FocusController>();
        characterStats = Player.GetComponent<AllCharacterStats>();

        DeveloperConsoleBox = FindObjectOfType<TextMeshProUGUI>();
    }

    public override void Interact()
    {
        base.Interact();

        PickUpCurrency();
    }

    void PickUpCurrency()
    {
        // Add to balance
        DeveloperConsoleBox.text += "DEBUG - CURRENCY: Added " + currency.name + " to your balance \n";
        Debug.Log("DEBUG - CURRENCY: Added " + currency.name + " to your balance");
        characterStats.Balance += currency.CurrencyValue;

        // Delete from game world
        DeveloperConsoleBox.text += "DEBUG - CURRENCY: Deleting " + gameObject.name + "\n";
        Debug.Log("DEBUG - CURRENCY: Deleting " + gameObject.name);
        Destroy(gameObject);

        // Stop Focusing the deleted currency
        focusController.DeFocus();
    }
}
