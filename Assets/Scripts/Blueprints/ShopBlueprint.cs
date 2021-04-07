using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public enum CanSell
{
	Items,
	Equipment,
	Both
}
[System.Serializable]
public class ItemBP
{
	public ItemBlueprint item;
	public int cost;
	[Range(0, 5)]
	public int stock;
}

[System.Serializable]
public class EquipmentBP
{
	public EquipmentBlueprint equipment;
	public int cost;
	[Range(1, 5)]
	public int stock;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Shop", menuName = "Custom Scriptable Objects/Shop/Static Shop")]
public class ShopBlueprint : ScriptableObject
{
	public string shopName;

	public CanSell canSell;

	public List<ItemBP> itemBlueprints;
	public List<EquipmentBP> equipmentBlueprints;
}