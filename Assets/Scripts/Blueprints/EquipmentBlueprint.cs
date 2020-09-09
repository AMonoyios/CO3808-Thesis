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

[CreateAssetMenu(fileName = "New Equipment", menuName = "Custom Scriptable Objects/Game Objects/Equipment")]
public class EquipmentBlueprint : ItemBlueprint
{
	[Header("Equipment specific properties")]
	// Properties for all equipment
	public EquipmentSlots EquipSlot;

	[Range(0,10)]
	public int Protection;
	[Range(0,10)]
	public int Damage;

	public override void UseItem()
	{
		base.UseItem();

		// equip item
		Equipment.EquipmentInstance.Equip(this);

		// remove from inventory
		RemoveItem();
	}
}