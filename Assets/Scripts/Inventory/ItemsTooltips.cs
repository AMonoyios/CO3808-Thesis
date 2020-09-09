using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemsTooltips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Specific slot properties")]
	[Range(0, 15)]
	public int InventorySlotIndex;
	public GameObject TooltipUI;
	public Text TooltipTextBox;

	Inventory inventory;
	Equipment equipment;

	private void Start()
	{
		inventory = Inventory.InventoryInstance;
		equipment = Equipment.EquipmentInstance;

		try
		{
			TooltipTextBox.text = "";
			TooltipUI.transform.position = Vector3.zero;
			TooltipUI.SetActive(false);
		}
		catch (System.Exception)
		{
			Debug.LogError("ERROR - Inventory: Tooltip UI for " + this.transform.parent.parent.name + " not assigned!");
		}
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		Debug.Log("DEBUG - ItemsTooltips: Enter pointer event draw");

		if (eventData.pointerCurrentRaycast.gameObject.transform.name == "Item Icon")
		{
			Debug.Log("DEBUG - ItemsTooltips: (Inventory) Displaying item description for " + inventory.InventoryItems[InventorySlotIndex].item.ItemName);
			
			TooltipTextBox.text = inventory.InventoryItems[InventorySlotIndex].item.ItemDescription;
			TooltipUI.transform.position = transform.position;
			TooltipUI.SetActive(true);
		}
		else if (eventData.pointerCurrentRaycast.gameObject.transform.name == "Equipment Icon")
		{
			Debug.Log("DEBUG - ItemsTooltips: (Equipment) Displaying item description for " + equipment.equipmentSlot[InventorySlotIndex].ItemName);
			
			TooltipTextBox.text = equipment.equipmentSlot[InventorySlotIndex].ItemDescription;
			TooltipUI.transform.position = transform.position;
			TooltipUI.SetActive(true);
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("DEBUG - ItemsTooltips: Exit pointer event draw");
		
		TooltipTextBox.text = "";
		TooltipUI.transform.position = Vector3.zero;
		TooltipUI.SetActive(false);
	}
}
