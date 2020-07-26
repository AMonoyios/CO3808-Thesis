using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EquipmentType
{
    Head,
    Chest,
    Legs,
    Feet,
    RightHand,
    LeftHand
}

public class EquipmentSlot : MonoBehaviour
{
    Equipment equipment;

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
    void UpdateEquipment_UI(EquipmentBlueprint Item)
    {
        Debug.Log("Update equipment UI: " + Item.ItemName);

		if ((int)Item.EquipSlot == (int)equipmentType)
		{
            AddEquipmentToSlot(Item);
		}
    }

    public void AddEquipmentToSlot(EquipmentBlueprint SlotItem)
    {
        SlotIcon.sprite = SlotItem.ItemIcon;
        SlotIcon.enabled = true;
        UnEquipButton.interactable = true;
    }

    // TODO 
    // 1) Disable movement or interactions when hovering over equipment UI
    // 2) Implement Un-Equip item button
    // 3) Add left hand and right hand equipment slots
    // 4) Add an Un-Equip all keybind action
}
