using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadTriggers : MonoBehaviour
{
	GameObject player;
	AllCharacterStats characterStats;
	Inventory inventory;

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

		try
		{
			Vector3 position;
			position.x = data.position[0];
			position.y = data.position[1];
			position.z = data.position[2];
			player.transform.position = position;
		}
		catch (System.Exception)
		{
			Debug.LogError("position not loaded");
		}

		try
		{
			Quaternion rotation;
			rotation.x = data.rotation[0];
			rotation.y = data.rotation[1];
			rotation.z = data.rotation[2];
			rotation.w = data.rotation[3];
			player.transform.rotation = rotation;
		}
		catch (System.Exception)
		{
			Debug.LogError("rotation not loaded");
		}

		try
		{
			characterStats.Balance = data.balance;
			characterStats.Health = data.health;
			characterStats.Protection = data.protection;
			characterStats.Speed = data.speed;
			characterStats.Damage = data.damage;
		}
		catch (System.Exception)
		{
			Debug.LogError("stats not loaded");
		}

		try
		{
			Inventory.InventoryInstance.InventorySlots = data.inventorySlots;
			//Inventory.InventoryInstance.InventoryItems = data.;
		}
		catch (System.Exception)
		{
			Debug.LogError("inventory not loaded");
		}
	}
}
