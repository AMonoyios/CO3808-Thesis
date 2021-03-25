using UnityEngine;

[System.Serializable]
public class AllCharacterStats : MonoBehaviour
{
    public int Balance = 0;
    public float Health = 100.0f;
    public float Protection = 1.0f;
    public float Speed = 1.0f;
    public float Damage = 1.0f;

	public void Save()
	{
		SaveManager.SaveData(this, Inventory.InventoryInstance);
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
			transform.position = position;
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
			transform.rotation = rotation;
		}
		catch (System.Exception)
		{
			Debug.LogError("rotation not loaded");
		}

		try
		{
			Balance = data.balance;
			Health = data.health;
			Protection = data.protection;
			Speed = data.speed;
			Damage = data.damage;
		}
		catch (System.Exception)
		{
			Debug.LogError("stats not loaded");
		}

		try
		{
			Inventory.InventoryInstance.InventorySlots = data.inventorySlots;
			Inventory.InventoryInstance.InventoryItems = data.inventoryItems;
		}
		catch (System.Exception)
		{
			Debug.LogError("inventory not loaded");
		}
	}
}
