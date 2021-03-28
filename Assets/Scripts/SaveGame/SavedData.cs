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
    public string Bundle;
    public string AssetName;
    public string ItemDescription;
}

[System.Serializable]
public struct InventoryEquipment
{
    public EquipmentSlots EquipSlot;
    public List<Positives> PositiveTraits;
    public List<Negatives> NegativeTraits;

    public string ItemName;
    public string ItemIcon;
    public bool isDefault;
    public int StackUntil;
    public string Bundle;
    public string AssetName;
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
    public List<InventoryEquipment> inventoryEquipment;

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
        inventoryEquipment = new List<InventoryEquipment>();
		for (int i = 0; i < inventorySlots; i++)
		{
            Debug.Log("checking inventory slot at index" + i);

			if (inventory.GetInventory()[i] != null)
			{
                Debug.Log("found item at index " + i);

				
                if (inventory.GetInventory()[i].item is EquipmentBlueprint)
				{
                    InventoryEquipment item = new InventoryEquipment();
                    EquipmentBlueprint equipBP = inventory.GetInventory()[i].item.equipBP;

                    EquipmentSlots equipmentSlots = equipBP.EquipSlot;
                    item.EquipSlot = equipmentSlots;

                    List<Positives> positives = new List<Positives>();
					for (int j = 0; j < equipBP.PositiveTraits.Count; j++)
					{
                        Positives posTrait = new Positives();
                        posTrait.traitLevel = equipBP.PositiveTraits[j].traitLevel;
                        posTrait.traits = equipBP.PositiveTraits[j].traits;

                        positives.Add(posTrait);
					}

                    List<Negatives> negatives = new List<Negatives>();
                    for (int j = 0; j < equipBP.NegativeTraits.Count; j++)
                    {
                        Negatives negTrait = new Negatives();
                        negTrait.traitLevel = equipBP.NegativeTraits[j].traitLevel;
                        negTrait.traits = equipBP.NegativeTraits[j].traits;

                        negatives.Add(negTrait);
                    }

                    item.ItemName = equipBP.ItemName;

                    Texture2D tex = equipBP.ItemIcon.texture;
                    exportObj.x = tex.width;
                    exportObj.y = tex.height;
                    exportObj.bytes = ImageConversion.EncodeToPNG(tex);
                    string icon = JsonUtility.ToJson(exportObj, false);

                    item.ItemIcon = icon;
                    item.isDefault = equipBP.isDefault;
                    item.StackUntil = equipBP.StackUntil;

                    item.Bundle = equipBP.Bundle;
                    item.AssetName = equipBP.AssetName;

                    item.ItemDescription = equipBP.ItemDescription;

                    inventoryEquipment.Add(item);
				}
				else
				{
                    InventoryItem item = new InventoryItem();
                    ItemBlueprint itemBP = inventory.GetInventory()[i].item;
                    
                    item.ItemName = itemBP.ItemName;

                    Texture2D tex = itemBP.ItemIcon.texture;
                    exportObj.x = tex.width;
                    exportObj.y = tex.height;
                    exportObj.bytes = ImageConversion.EncodeToPNG(tex);
                    string icon = JsonUtility.ToJson(exportObj, false);

                    item.ItemIcon = icon;
                    item.isDefault = itemBP.isDefault;
                    item.StackUntil = itemBP.StackUntil;

                    item.Bundle = itemBP.Bundle;
                    item.AssetName = itemBP.AssetName;

                    item.ItemDescription = itemBP.ItemDescription;

                    inventoryItems.Add(item);
				}
                
            }
		}
    }
}
