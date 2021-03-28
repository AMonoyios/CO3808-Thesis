using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTriggers : MonoBehaviour
{
	GameObject player;
	AllCharacterStats characterStats;
	Inventory inventory;
	
	//  use a delegate
	public delegate void ItemUpdated();
	public ItemUpdated CallItemUpdated;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		characterStats = player.GetComponent<AllCharacterStats>();
		inventory = Inventory.InventoryInstance;
	}

	public void Save()
	{
		SaveManager.SaveData(characterStats, inventory);
	}

	public void Load()
	{
		SavedData data = SaveManager.LoadData();

		Vector3 position;
		position.x = data.position[0];
		position.y = data.position[1];
		position.z = data.position[2];
		player.transform.position = position;
			
		Quaternion rotation;
		rotation.x = data.rotation[0];
		rotation.y = data.rotation[1];
		rotation.z = data.rotation[2];
		rotation.w = data.rotation[3];
		player.transform.rotation = rotation;

		characterStats.Balance = data.balance;
		characterStats.Health = data.health;
		characterStats.Protection = data.protection;
		characterStats.Speed = data.speed;
		characterStats.Damage = data.damage;
		
		Inventory.InventoryInstance.InventoryItems = new List<InventoryItems>();
		for (int i = 0; i < 15; i++)
		{
			Debug.Log("loading item " + i + "...");

			ItemBlueprint itemBP = ScriptableObject.CreateInstance<ItemBlueprint>();
			
			itemBP.ItemName = data.inventoryItems[i].ItemName;

			SerializeTexture importObj = new SerializeTexture();
			string iconText = data.inventoryItems[i].ItemIcon;
			importObj = JsonUtility.FromJson<SerializeTexture>(iconText);
			Texture2D tex = new Texture2D(importObj.x, importObj.y);
			ImageConversion.LoadImage(tex, importObj.bytes);
			Sprite iconSprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
			itemBP.ItemIcon = iconSprite;
			
			itemBP.isDefault = data.inventoryItems[i].isDefault;
			
			itemBP.StackUntil = data.inventoryItems[i].StackUntil;
			
			itemBP.Bundle = data.inventoryItems[i].Bundle;
			itemBP.AssetName = data.inventoryItems[i].AssetName;
			
			itemBP.ItemDescription = data.inventoryItems[i].ItemDescription;
			
			InventoryItems inventoryItems = new InventoryItems(itemBP, 1);
			Inventory.InventoryInstance.InventoryItems.Add(inventoryItems);

			//update the inventory UI
			if (CallItemUpdated != null)    // B9 fix
			{
				CallItemUpdated.Invoke();
			}
		}
	}
}
