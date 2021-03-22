using System.Collections.Generic;
using UnityEngine;

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
	[Range(0.1f,1.0f)]
	public float traitLevel;
}

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
	[Range(0.1f,1.0f)]
	public float traitLevel;
}

public enum NegativeTraits
{
	Slowness,
	Exposure
}

[CreateAssetMenu(fileName = "New Equipment", menuName = "Custom Scriptable Objects/Game Objects/Equipment")]
public class EquipmentBlueprint : ItemBlueprint
{
	[Header("Equipment specific properties")]
	// Properties for all equipment
	public EquipmentSlots EquipSlot;

	public List<Positives> PositiveTraits;
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