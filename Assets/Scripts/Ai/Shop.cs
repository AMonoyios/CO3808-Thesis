using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShopUIHandler))]
public class Shop : InteractPoint
{
    AllCharacterStats characterStats;
    [HideInInspector]
    public FocusController focusController;
    
    public ShopBlueprint shopBP;
    public Vector3 itemsSpawnPosition = Vector3.zero;

    public bool isBrowsing = false;

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

        Debug.Log("OpenShop");

        // look at ShopUIHandler
        isBrowsing = true;
    }

	private void OnDrawGizmos()
	{
        Gizmos.color = gizmos.itemSpawnPoint;
        Gizmos.DrawWireCube(itemsSpawnPosition + this.transform.position, Vector3.one);
	}
}
