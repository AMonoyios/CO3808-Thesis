using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Passive NPC", menuName = "Custom Scriptable Objects/NPC/Passive")]
public class NPCBlueprint : CharacterBlueprint
{
    [Header("Passive NPC Properties")]
    public NPCState npcState = NPCState.idle;
    [Range(1.0f, 10.0f)]
    public float npcInteractionRadius = 2.0f;
    
}