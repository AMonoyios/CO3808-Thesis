using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InventoryItem
{
    public string ItemName;
    public string ItemIcon;
    public bool isDefault;
    public int StackUntil;
    public string itemPrefab;
    public string ItemDescription;
}

[System.Serializable]
public class SerializeTexture
{
    public int x;
    public int y;
    public byte[] bytes;
}

[System.Serializable]
public class SavedData
{
    // Player data
    public float[] position;
    public float[] rotation;
    public int balance;
    public float health;
    public float protection;
    public float speed;
    public float damage;

    // Player inventory
    public int inventorySlots;
    public SerializeTexture exportObj = new SerializeTexture();
    public List<InventoryItem> inventoryItems;

    public SavedData (AllCharacterStats characterStats, Inventory inventory)
    {
        // player position/rotation
        position = new float[3];
        position[0] = characterStats.transform.position.x;
        position[1] = characterStats.transform.position.y;
        position[2] = characterStats.transform.position.z;

        rotation = new float[4];
        rotation[0] = characterStats.transform.rotation.x;
        rotation[1] = characterStats.transform.rotation.y;
        rotation[2] = characterStats.transform.rotation.z;
        rotation[3] = characterStats.transform.rotation.w;

        // player stats
        balance = characterStats.Balance;
        health = characterStats.Health;
        protection = characterStats.Protection;
        speed = characterStats.Speed;
        damage = characterStats.Damage;

        // player inventory
        inventorySlots = inventory.InventoryItems.Count;
        inventoryItems = new List<InventoryItem>();
		for (int i = 0; i < inventorySlots; i++)
		{
            Debug.Log("checking inventory slot at index" + i);

			if (inventory.GetInventory()[i] != null)
			{
                Debug.Log("found item at index " + i);

                ItemBlueprint itemBP = inventory.GetInventory()[i].item;
                InventoryItem item = new InventoryItem();
                item.ItemName = itemBP.ItemName;

                Texture2D tex = itemBP.ItemIcon.texture;
                exportObj.x = tex.width;
                exportObj.y = tex.height;
                exportObj.bytes = ImageConversion.EncodeToPNG(tex);
                string icon = JsonUtility.ToJson(exportObj, false);

                item.ItemIcon = icon;
                item.isDefault = itemBP.isDefault;
                item.StackUntil = itemBP.StackUntil;
                item.itemPrefab = null;
                item.ItemDescription = itemBP.ItemDescription;

                inventoryItems.Add(item);
            }
		}
    }
}
