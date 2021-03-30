using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Shop : InteractPoint
{
    AllCharacterStats characterStats;
    FocusController focusController;
    
    public TextMeshProUGUI nameUI;
    public MovingShopBlueprint movingShopBP;
    public ShopBlueprint staticShopBP;
    public Vector3 itemsSpawnPosition = Vector3.zero;

    public void Start()
    {
        playerManager = PlayerManager.Instance.player.GetComponent<PlayerManager>();
        characterStats = playerManager.player.GetComponent<AllCharacterStats>();

        focusController = playerManager.player.GetComponent<FocusController>();

        if (interactionPoint == null)
        {
            interactionPoint = this.transform;
        }
    }

    public override void Interact()
    {
        base.Interact();

        // remember to add the transform.position to the itemSpawn point when instantiating items

        // player views the shop catalog
        Debug.Log("OpenShop");

        // defocusing shop
        focusController.DeFocus();
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = gizmos.itemSpawnPoint;
        Gizmos.DrawWireCube(itemsSpawnPosition + this.transform.position, Vector3.one);
	}
}
