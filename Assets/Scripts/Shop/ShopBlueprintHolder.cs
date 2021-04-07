using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopBlueprintHolder : MonoBehaviour
{
	public GameObject player;
	public PlayerManager playerManager;
	public AllCharacterStats characterStats;
	public FocusController focusController;
	public Transform shop;
	public ItemBlueprint itemBP;
	public Vector3 itemSpawnPos;

	private void Awake()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		playerManager = player.GetComponent<PlayerManager>();
		characterStats = playerManager.player.GetComponent<AllCharacterStats>();
		focusController = playerManager.player.GetComponent<FocusController>();
		shop = focusController.focus.interactionPoint.GetComponent<Shop>().transform;
		itemSpawnPos = focusController.focus.interactionPoint.GetComponent<Shop>().itemsSpawnPosition;
	}

	public void Buy()
    {
        Debug.Log("DEBUG - Shop: Attempting to buy " + itemBP.ItemName + "...");

		if (characterStats.Balance >= itemBP.ItemPrice)
		{
			Debug.Log("DEBUG - Shop: Purchase successful");

			GameObject item = Instantiate(itemBP.itemPrefab, shop.position + itemSpawnPos, shop.rotation, parent: shop);
			item.name = itemBP.name;
		}
		else
		{
			Debug.Log("DEBUG - Shop: Purchase failed, player balance is " + characterStats.Balance + " < " + itemBP.ItemName + " price of " + itemBP.ItemPrice);
		}
	}
}
