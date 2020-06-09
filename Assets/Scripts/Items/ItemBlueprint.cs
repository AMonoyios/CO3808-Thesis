using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Custom Scriptable Objects/Game Objects/Item")]
public class ItemBlueprint : ScriptableObject
{
    // Properties for all Item types
    public string ItemName = "New Item";
    public Sprite ItemIcon = null;
    public bool isDefault = false;
}