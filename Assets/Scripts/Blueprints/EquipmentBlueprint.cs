using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum EquipmentSlots 
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

[System.Serializable]
public struct Positives
{
	public PositiveTraits traits;
	[Range(1,5)]
	public int traitLevel;
}

[System.Serializable]
public enum PositiveTraits
{
	Protection,
	Speed,
	Damage
}

[System.Serializable]
public struct Negatives
{
	public NegativeTraits traits;
	[Range(1,5)]
	public int traitLevel;
}

[System.Serializable]
public enum NegativeTraits
{
	Slowness,
	Exposure
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Equipment", menuName = "Custom Scriptable Objects/Game Objects/Equipment")]
public class EquipmentBlueprint : ItemBlueprint
{
	[Header("Equipment specific properties")]
	// Properties for all equipment
	public EquipmentSlots EquipSlot;

	[Tooltip("Protection = Less damage taken \n Speed = Walk faster \n Damage = Deals more damage")]
	public List<Positives> PositiveTraits;
	[Tooltip("Slowness = Walk slower \n Exposure = Deals less damage")]
	public List<Negatives> NegativeTraits;

	public override void UseItem()
	{
		base.UseItem();

		// equip item
		Equipment.EquipmentInstance.Equip(this);

		// remove from inventory
		RemoveItem();
	}
}