using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	#region EquipmentSingleton

	public static Equipment EquipmentInstance;

	private void Awake()
	{
		EquipmentInstance = this;
	}

	#endregion

	public EquipmentBlueprint[] equipmentSlot;

	private AllCharacterStats characterStats;
	private PlayerManager playerManager;

	// In order to update the equipment UI without using the function in the Update() we can 
	//  use a delegate
	public delegate void EquipmentUpdated(EquipmentBlueprint item, bool AddorRemove);
	public EquipmentUpdated CallEquipmentUpdated;

	private void Start()
	{
		int SlotSize = System.Enum.GetNames(typeof(EquipmentSlots)).Length;
		equipmentSlot = new EquipmentBlueprint[SlotSize];

		// Get player instance
		playerManager = PlayerManager.Instance.player.GetComponent<PlayerManager>();
		characterStats = playerManager.player.GetComponent<AllCharacterStats>();
	}

	public void Equip(EquipmentBlueprint Item)
	{
		int SlotIndex = (int)Item.EquipSlot;

		// get equipment currently in use
		EquipmentBlueprint oldEquipment;
		
		// Swap new equipment with old
		if (equipmentSlot[SlotIndex] != null)
		{
			oldEquipment = equipmentSlot[SlotIndex];
			Inventory.InventoryInstance.AddToInventory(oldEquipment);
		}

		equipmentSlot[SlotIndex] = Item;

		// apply the trait values of the equipment to the player stat
		ApplyTraitsToPlayer(Item);

		if (CallEquipmentUpdated != null)
		{
			CallEquipmentUpdated.Invoke(Item, true);
		}
	}

	public void UnEquip(int SlotIndex)
	{
		// Try adding equipment item to inventory
		if (Inventory.InventoryInstance.InventorySlots > Inventory.InventoryInstance.InventoryItems.Count)
		{
			EquipmentBlueprint ItemToUnequip = Equipment.EquipmentInstance.equipmentSlot[SlotIndex];
			Inventory.InventoryInstance.AddToInventory(ItemToUnequip);

			// remove trait values of the equipment to the player stat
			RemoveTraitsFromPlayer(ItemToUnequip);

			if (CallEquipmentUpdated != null)  // Fixed: items are being deleted when you have more than one equipped item and then click unequip
			{
				CallEquipmentUpdated.Invoke(ItemToUnequip, false);
			}
			equipmentSlot[SlotIndex] = null;
		}
		else
		{
			Debug.LogError("ERROR - EquipmentInventory: Item can not be unequip, no space in inventory!");
		}
	}

	public void ApplyTraitsToPlayer(ItemBlueprint Item) 
	{
		// apply the positive trait values of the equipment to the player stat
		foreach (Positives PosTraits in Item.equipBP.PositiveTraits)
		{
			Debug.Log("Applying " + PosTraits.traitLevel.ToString() + " level(s) of " + PosTraits.traits.ToString());

			switch (PosTraits.traits)
			{
				case PositiveTraits.Protection:
					{
						characterStats.Protection += PosTraits.traitLevel;
					}
					break;
				case PositiveTraits.Speed:
					{
						characterStats.Speed += PosTraits.traitLevel;
					}
					break;
				case PositiveTraits.Damage:
					{
						characterStats.Damage += PosTraits.traitLevel;
					}
					break;
				default:
					break;
			}
		}

		// apply the negative trait values of the equipment to the player stat
		foreach (Negatives NegTraits in Item.equipBP.NegativeTraits)
		{
			Debug.Log("Applying " + NegTraits.traitLevel.ToString() + " level(s) of " + NegTraits.traits.ToString());

			switch (NegTraits.traits)
			{
				case NegativeTraits.Slowness:
					{
						characterStats.Speed -= NegTraits.traitLevel;
					}
					break;
				case NegativeTraits.Exposure:
					{
						characterStats.Damage -= NegTraits.traitLevel;
					}
					break;
				default:
					break;
			}
		}
	}
	public void RemoveTraitsFromPlayer(ItemBlueprint Item)
	{
		foreach (Positives PosTraits in Item.equipBP.PositiveTraits)
		{
			Debug.Log("Removing " + PosTraits.traitLevel.ToString() + " level(s) of " + PosTraits.traits.ToString());

			switch (PosTraits.traits)
			{
				case PositiveTraits.Protection:
					{
						characterStats.Protection -= PosTraits.traitLevel;
					}
					break;
				case PositiveTraits.Speed:
					{
						characterStats.Speed -= PosTraits.traitLevel;
					}
					break;
				case PositiveTraits.Damage:
					{
						characterStats.Damage -= PosTraits.traitLevel;
					}
					break;
				default:
					break;
			}
		}

		// apply the negative trait values of the equipment to the player stat
		foreach (Negatives NegTraits in Item.equipBP.NegativeTraits)
		{
			Debug.Log("Applying " + NegTraits.traitLevel.ToString() + " level(s) of " + NegTraits.traits.ToString());

			switch (NegTraits.traits)
			{
				case NegativeTraits.Slowness:
					{
						characterStats.Speed += NegTraits.traitLevel;
					}
					break;
				case NegativeTraits.Exposure:
					{
						characterStats.Damage += NegTraits.traitLevel;
					}
					break;
				default:
					break;
			}
		}
	}
}
