using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ShopBlueprint)), CanEditMultipleObjects]
public class ShopBlueprintEditor : Editor
{
	ShopBlueprint shopBP;

	private void OnEnable()
	{
		shopBP = target as ShopBlueprint;
	}

	public override void OnInspectorGUI()
	{
		//base.OnInspectorGUI();
		serializedObject.Update();
		
		shopBP.shopName = EditorGUILayout.TextField("Shop name: ", shopBP.shopName);

		shopBP.canSell = (CanSell)EditorGUILayout.EnumPopup("Shop can sell: ", shopBP.canSell);

		switch (shopBP.canSell)
		{
			case CanSell.Items:
				{
					int itemListCount = Mathf.Max(1, EditorGUILayout.IntField("Items to sell: ", shopBP.itemBlueprints.Count));
					while (itemListCount < shopBP.itemBlueprints.Count)
					{
						shopBP.itemBlueprints.RemoveAt(shopBP.itemBlueprints.Count - 1);
					}
					while (itemListCount > shopBP.itemBlueprints.Count)
					{
						ItemBP itemBP = new ItemBP();
						shopBP.itemBlueprints.Add(itemBP);
					}

					for (int i = 0; i < shopBP.itemBlueprints.Count; i++)
					{
						EditorGUILayout.Space();
						shopBP.itemBlueprints[i].item = (ItemBlueprint)EditorGUILayout.ObjectField(shopBP.itemBlueprints[i].item, typeof(ItemBlueprint), true);
						shopBP.itemBlueprints[i].cost = EditorGUILayout.IntField(shopBP.itemBlueprints[i].cost);
						shopBP.itemBlueprints[i].stock = EditorGUILayout.IntSlider(shopBP.itemBlueprints[i].stock, 0, 5);
						EditorGUILayout.Space(2);
					}
				}
				break;
			case CanSell.Equipment:
				{
					int equipmentListCount = Mathf.Max(1, EditorGUILayout.IntField("Equipment to sell: ", shopBP.equipmentBlueprints.Count));

					while (equipmentListCount < shopBP.equipmentBlueprints.Count)
					{
						shopBP.equipmentBlueprints.RemoveAt(shopBP.equipmentBlueprints.Count - 1);
					}
					while (equipmentListCount > shopBP.equipmentBlueprints.Count)
					{
						EquipmentBP equipmentBP = new EquipmentBP();
						shopBP.equipmentBlueprints.Add(equipmentBP);
					}

					for (int i = 0; i < shopBP.equipmentBlueprints.Count; i++)
					{
						EditorGUILayout.Space();
						shopBP.equipmentBlueprints[i].equipment = (EquipmentBlueprint)EditorGUILayout.ObjectField(shopBP.equipmentBlueprints[i].equipment, typeof(EquipmentBlueprint), true);
						shopBP.equipmentBlueprints[i].cost = EditorGUILayout.IntField(shopBP.equipmentBlueprints[i].cost);
						shopBP.equipmentBlueprints[i].stock = EditorGUILayout.IntSlider(shopBP.equipmentBlueprints[i].stock, 0, 5);
						EditorGUILayout.Space(2);
					}
				}
				break;
			case CanSell.Both:
				{
					int itemListCount = Mathf.Max(1, EditorGUILayout.IntField("Items to sell: ", shopBP.itemBlueprints.Count));

					while (itemListCount < shopBP.itemBlueprints.Count)
					{
						shopBP.itemBlueprints.RemoveAt(shopBP.itemBlueprints.Count - 1);
					}
					while (itemListCount > shopBP.itemBlueprints.Count)
					{
						ItemBP itemBP = new ItemBP();
						shopBP.itemBlueprints.Add(itemBP);
					}

					for (int i = 0; i < shopBP.itemBlueprints.Count; i++)
					{
						EditorGUILayout.Space();
						shopBP.itemBlueprints[i].item = (ItemBlueprint)EditorGUILayout.ObjectField(shopBP.itemBlueprints[i].item, typeof(ItemBlueprint), true);
						shopBP.itemBlueprints[i].cost = EditorGUILayout.IntField(shopBP.itemBlueprints[i].cost);
						shopBP.itemBlueprints[i].stock = EditorGUILayout.IntSlider(shopBP.itemBlueprints[i].stock, 0, 5);
						EditorGUILayout.Space(2);
					}

					GuiLine();

					int equipmentListCount = Mathf.Max(1, EditorGUILayout.IntField("Equipment to sell: ", shopBP.equipmentBlueprints.Count));

					while (equipmentListCount < shopBP.equipmentBlueprints.Count)
					{
						shopBP.equipmentBlueprints.RemoveAt(shopBP.equipmentBlueprints.Count - 1);
					}
					while (equipmentListCount > shopBP.equipmentBlueprints.Count)
					{
						EquipmentBP equipmentBP = new EquipmentBP();
						shopBP.equipmentBlueprints.Add(equipmentBP);
					}

					for (int i = 0; i < shopBP.equipmentBlueprints.Count; i++)
					{
						EditorGUILayout.Space();
						shopBP.equipmentBlueprints[i].equipment = (EquipmentBlueprint)EditorGUILayout.ObjectField(shopBP.equipmentBlueprints[i].equipment, typeof(EquipmentBlueprint), true);
						shopBP.equipmentBlueprints[i].cost = EditorGUILayout.IntField(shopBP.equipmentBlueprints[i].cost);
						shopBP.equipmentBlueprints[i].stock = EditorGUILayout.IntSlider(shopBP.equipmentBlueprints[i].stock, 0, 5);
						EditorGUILayout.Space(2);
					}
				}
				break;
			default:
				break;
		}

		serializedObject.ApplyModifiedProperties();
	}

	void GuiLine()
	{
		EditorGUILayout.Space(8);

		Rect rect = EditorGUILayout.GetControlRect(false, 1);

		rect.height = 1;

		EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
		EditorGUILayout.Space(6);
	}
}
