using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public List<InventoryItems> inventoryItems;

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
        inventorySlots = inventory.InventorySlots;
        inventoryItems = inventory.GetInventory();
    }
}
