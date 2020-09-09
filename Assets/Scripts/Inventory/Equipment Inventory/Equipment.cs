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

	// In order to update the equipment UI without using the function in the Update() we can 
	//  use a delegate
	public delegate void EquipmentUpdated(EquipmentBlueprint item, bool AddorRemove);
	public EquipmentUpdated CallEquipmentUpdated;

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

			if (CallEquipmentUpdated != null)  // TODO: items are being deleted when you have more than one equipped item and then click unequip
			{
				CallEquipmentUpdated.Invoke(ItemToUnequip, false);
			}
			equipmentSlot[SlotIndex] = null;
		}
		else
		{
			Debug.LogError("ERROR - EquipmentInventory: Item can not be unequip, no space in inventory!");
		}

		//// Reset equipment slot
		//SlotIcon.sprite = null;
		//SlotIcon.enabled = false;
		//UnEquipButton.interactable = false;
	}
}
