using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum ShopType
{
	MovingShop,
	StaticShop
}

[CustomEditor(typeof(Shop), true)]
[CanEditMultipleObjects]
public class ShopEditor : Editor
{
	SerializedProperty _interactionPoint;
	SerializedProperty _radius;
	SerializedProperty _gizmos;
	SerializedProperty _playerManager;

	public ShopType _shopType;
	SerializedProperty _movingShopBP;
	SerializedProperty _staticShopBP;

	SerializedProperty _itemSpawnPos;

	private void OnEnable()
	{
		_movingShopBP = serializedObject.FindProperty("movingShopBP");
		_staticShopBP = serializedObject.FindProperty("staticShopBP");

		_interactionPoint = serializedObject.FindProperty("interactionPoint");
		_radius = serializedObject.FindProperty("radius");
		_gizmos = serializedObject.FindProperty("gizmos");
		_playerManager = serializedObject.FindProperty("playerManager");

		_itemSpawnPos = serializedObject.FindProperty("itemsSpawnPosition");
	}

	public override void OnInspectorGUI()
	{
		EditorGUILayout.PropertyField(_interactionPoint, new GUIContent("Interaction point"));
		EditorGUILayout.PropertyField(_radius, new GUIContent("Interaction radius"));
		EditorGUILayout.PropertyField(_gizmos, new GUIContent("Gizmos"));
		EditorGUILayout.PropertyField(_playerManager, new GUIContent("Player manager"));

		// show header for shop properties
		EditorGUILayout.Space();
		EditorGUILayout.LabelField("Shop Properties", EditorStyles.boldLabel);

		_shopType = (ShopType)EditorGUILayout.EnumPopup("Shop Type: ", _shopType);
		if (_shopType == ShopType.MovingShop)
		{
			EditorGUILayout.PropertyField(_movingShopBP, new GUIContent("Moving Shop Blueprint"));
		}
		else
		{
			EditorGUILayout.PropertyField(_staticShopBP, new GUIContent("Static Shop Blueprint"));
		}

		EditorGUILayout.PropertyField(_itemSpawnPos, new GUIContent("Item spawn position"));

		serializedObject.ApplyModifiedProperties();
	}
}
