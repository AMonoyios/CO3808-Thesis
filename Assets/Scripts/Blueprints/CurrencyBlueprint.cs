using UnityEngine;

[CreateAssetMenu(fileName = "New Currency", menuName = "Custom Scriptable Objects/Game Objects/Currency")]
public class CurrencyBlueprint : ScriptableObject
{
    // Properties for all currency types
    public string CurrencyName = "New Currency";
    public int CurrencyValue = 0;
    public bool isDefault = false;
}
