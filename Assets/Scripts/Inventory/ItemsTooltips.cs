using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ItemsTooltips : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	[Header("Specific slot properties")]
	[Range(0, 14)]
	public int InventorySlotIndex;
	public GameObject TooltipUI;
	public Text ToolDescription;
	public Text ToolPositive;
	public Text ToolNegative;

	bool CollectedEquipData = false;

	Inventory inventory;
	Equipment equipment;

	private void Start()
	{
		inventory = Inventory.InventoryInstance;
		equipment = Equipment.EquipmentInstance;

		try
		{
			ToolDescription.text = "";
			ToolPositive.text = "";
			ToolNegative.text = "";
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

			ItemBlueprint equipBP = inventory.InventoryItems[InventorySlotIndex].item;
			DisplayItemDataUI(equipBP);
		}
		else if (eventData.pointerCurrentRaycast.gameObject.transform.name == "Equipment Icon")
		{
			Debug.Log("DEBUG - ItemsTooltips: (Equipment) Displaying item description for " + equipment.equipmentSlot[InventorySlotIndex].ItemName);

			ItemBlueprint equipBP = equipment.equipmentSlot[InventorySlotIndex];
			DisplayItemDataUI(equipBP);
		}
	}

	void DisplayItemDataUI(ItemBlueprint item) 
	{
		if (!CollectedEquipData)
		{
			// print item description
			ToolDescription.text = item.ItemDescription;

			// Fixed - B28) Items that are not equipable do not work with UI
			if (item.itemPrefab.CompareTag("Item"))
			{
				// Player pointing in inventory at an item

			}
			else if (item.itemPrefab.CompareTag("Equipment"))
			{
				// Player pointing in inventory at an equipable item

				// print item positive stats
				foreach (Positives PosTraits in item.equipBP.PositiveTraits)
				{
					ToolPositive.text += PosTraits.traits.ToString() + ": " + PosTraits.traitLevel.ToString() + "\n";
				}

				// print item negaive stats
				foreach (Negatives NegTraits in item.equipBP.NegativeTraits)
				{
					ToolNegative.text += NegTraits.traits.ToString() + ": " + NegTraits.traitLevel.ToString() + "\n";
				}
			}

			// position and activate Tooltip UI
			TooltipUI.transform.position = transform.position;
			TooltipUI.SetActive(true);

			// To stop it from printing over and over again the stats of the item in the UI
			CollectedEquipData = true;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		Debug.Log("DEBUG - ItemsTooltips: Exit pointer event draw");

		// Reset Tooltip UI
		ToolDescription.text = "";
		ToolNegative.text = "";
		ToolPositive.text = "";
		TooltipUI.SetActive(false);

		// Reset the data collection loop
		CollectedEquipData = false;
	}
}