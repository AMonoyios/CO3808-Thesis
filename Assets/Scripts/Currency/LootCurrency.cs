using UnityEngine;

public class LootCurrency : InteractPoint
{
    [Header("Item Specific Properties")]
    [Header("Currency Scriptable Object")]
    public CurrencyBlueprint currency;

    private FocusController focusController;
    private AllCharacterStats characterStats;

    // Used the Player Instance Class to avoid slowdowns
    //private GameObject playerInstance;

    // In order to avoid having to drag and drop each Gameobject to every single coin we create, 
    //  we can use GetComponent and search all the game files for the specific gameobject, this
    //  is not a perfect solution but we only do it once right before the game starts. 
    private void Start()
    {
        // Removed this in order to prevent unessesary game slowdowns
        // Replaced it with an instance of the player in 1 global script
        //GameObject Player = GameObject.Find("Player");
        //focusController = Player.GetComponent<FocusController>();
        //characterStats = Player.GetComponent<AllCharacterStats>();

        // Get player instance
        playerManager = PlayerManager.Instance.player.GetComponent<PlayerManager>();

        focusController = playerManager.player.GetComponent<FocusController>();
        characterStats = playerManager.player.GetComponent<AllCharacterStats>();
    }

    public override void Interact()
    {
        base.Interact();

        PickUpCurrency();
    }

    void PickUpCurrency()
    {
        // Add to balance
        Debug.Log("DEBUG - CURRENCY: Added " + currency.name + " to your balance");
        characterStats.Balance += currency.CurrencyValue;

		// Delete from game world
        Debug.Log("DEBUG - CURRENCY: Deleting " + gameObject.name);
        Destroy(gameObject);

        // Stop Focusing the deleted currency
        focusController.DeFocus();
    }
}
