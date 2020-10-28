using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;
using System.Numerics;

public class GizmosManager : MonoBehaviour
{
    // Custom serializable struct
    [Serializable]
    public struct Vector2i
    {
        public string point;
        public bool isAccessible;

        public Vector2i(string point, bool isAccessible)
		{
            this.point = point;
            this.isAccessible = isAccessible;
		}
    }

    // Creating a custom drawer for the intaractables and their accessibility
    [CustomPropertyDrawer(typeof(Vector2i))]
    public class Interactable_CPD : PropertyDrawer
	{
        override public void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            float width = position.width;
            Rect pointRect = new Rect(position.x, position.y, width - (width / 15), position.height);
            Rect accessRect = new Rect(position.x + (width - width / 20), position.y, width / 20, position.height);

            // Draw fields - passs GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(pointRect, property.FindPropertyRelative("point"), GUIContent.none);
            EditorGUI.PropertyField(accessRect, property.FindPropertyRelative("isAccessible"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
		}
	}

    // An array for the custom struct
    public List<Vector2i> Interactables;

	private void Start()
	{
        GetInteractables();
	}

	public void GetInteractables()
	{
        Interactables.Clear();

        InteractPoint[] TempPoint = FindObjectsOfType(typeof(InteractPoint)) as InteractPoint[];
        Debug.Log("Found " + TempPoint.Length + " interactables in scene");

        foreach (InteractPoint item in TempPoint)
        {
            Interactables.Add(new Vector2i(item.name, true));
        }
    }

    // TODO Gizmos
    // create a function that enables the accesability of an item
    // create a function that disables the accesability of an item
}
