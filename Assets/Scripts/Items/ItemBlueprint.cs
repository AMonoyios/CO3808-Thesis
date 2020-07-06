﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "New Item", menuName = "Custom Scriptable Objects/Game Objects/Item")]
public class ItemBlueprint : ScriptableObject
{
    // Properties for all Item types
    public string ItemName = "New Item";
    public Sprite ItemIcon = null;
    public bool isDefault = false;
    public int StackUntil = 1;
    public GameObject itemPrefab = null;

    // The use for each item
    public virtual void UseItem()
    {
        Debug.Log("DEBUG - ITEM: Using " + ItemName);
    }
}