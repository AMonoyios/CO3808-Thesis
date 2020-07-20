using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
	#region EquipmentController Singleton

	public static Equipment EquipmentInstance;

	private void Awake()
	{
		EquipmentInstance = this;
	}

	#endregion

	EquipmentBlueprint[] equipmentSlot;

	private void Start()
	{
		int SlotSize = System.Enum.GetNames(typeof(EquipmentSlots)).Length;
		equipmentSlot = new EquipmentBlueprint[SlotSize];
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
	}

}
