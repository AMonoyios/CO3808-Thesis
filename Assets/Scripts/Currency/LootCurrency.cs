using UnityEngine;

public class LootCurrency : InteractPoint
{
    public CurrencyBlueprint currencyBlueprint;
    public FocusController focusController;
    public AllCharacterStats characterStats;

    // In order to avoid having to drag and drop each Gameobject to every single coin we add 
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
        base.Interact();

        CurrencyUp();
    }

    void CurrencyUp()
    {
        // Add to balance
        Debug.Log("DEBUG: Added " + currencyBlueprint.name + " to your balance");
        characterStats.Balance += currencyBlueprint.CurrencyValue;

        // Delete from game world
        Debug.Log("DEBUG: Deleting " + gameObject.name);
        Destroy(gameObject);

        // Stop Focusing the deleted currency
        focusController.DeFocus();
    }
}
