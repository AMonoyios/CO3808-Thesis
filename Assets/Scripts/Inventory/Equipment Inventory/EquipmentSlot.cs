using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public enum EquipmentType
{
    Head,
    Chest,
    Legs,
    Feet,
    RightHand,
    LeftHand,
    NeckAcc,
    HandAcc
}

public class EquipmentSlot : MonoBehaviour
{
    Equipment equipment;
    public GameObject EquipmentBag;

    [Header("Each equipment slot components")]
    public Image SlotIcon;
    public Button UnEquipButton;
    public EquipmentType equipmentType;

    // Start is called before the first frame update
    void Start()
    {
        equipment = Equipment.EquipmentInstance;
        equipment.CallEquipmentUpdated += UpdateEquipment_UI;
    }

    // Update is called once per frame
    void UpdateEquipment_UI(EquipmentBlueprint Item, bool AddorRemove)
    {
		if (AddorRemove == true)
		{
		    if ((int)Item.EquipSlot == (int)equipmentType && AddEquipmentToSlot(Item) == true)
		    {
                Debug.Log("DEBUG - EquipmentInventory: Update equipment UI: " + Item.ItemName);
		    }
		}
		else if (AddorRemove == false)
		{
		    for (int i = 0; i < Enum.GetNames(typeof(EquipmentType)).Length; i++)
		    {
		    	if (i == (int)Item.EquipSlot && RemoveEquipmentFromSlot(i) == true)
		    	{
                    Debug.Log("DEBUG - EquipmentInventory: Equipment slot " + i + " unequiped");
                }
            }
		}
		else
		{
            Debug.LogError("ERROR - EquipmentInventory: UpdateEquipment_UI has an invalid action index!");
		}
    }

    bool AddEquipmentToSlot(EquipmentBlueprint SlotItem)
    {
		// display equipment to slot
		try
		{
            SlotIcon.sprite = SlotItem.ItemIcon;
            SlotIcon.enabled = true;
            UnEquipButton.interactable = true;
		}
		catch (System.Exception)
		{
            return false;
			throw;
		}

        return true;
    }

    public bool RemoveEquipmentFromSlot(int EquipmentSlotIndex)
	{
		// reset equipment slot
		try
		{
            GameObject EquipmentSlot = EquipmentBag.transform.GetChild(EquipmentSlotIndex).gameObject;
            
            EquipmentSlot.transform.GetChild(0).GetComponent<EquipmentSlot>().SlotIcon.sprite = null;
            EquipmentSlot.transform.GetChild(0).GetComponent<EquipmentSlot>().SlotIcon.enabled = false;
            EquipmentSlot.transform.GetChild(0).GetComponent<EquipmentSlot>().UnEquipButton.interactable = false;
        }
		catch (System.Exception)
		{
            return false;
            throw;
		}

        return true;
    }

    // TODO 
    // DONE     - 1) Disable movement or interactions when hovering over equipment UI
    // DONE     - 2) Implement Un-Equip item button
    // DONE     - 3) Add left hand and right hand equipment slots
    // WORKING  - 4) Add an Un-Equip all keybind action
}
