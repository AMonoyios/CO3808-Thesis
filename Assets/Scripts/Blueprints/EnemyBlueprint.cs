using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Enemy", menuName = "Custom Scriptable Objects/NPC/Enemy")]
public class EnemyBlueprint : ScriptableObject
{
    [Header("Enemy Properties")]
    public bool isEnemy = true;
    public NPCState npcState = NPCState.none;
    [Range(1.0f, 10.0f)]
    public float npcInteractionRadius = 2.0f;
    public EnemyState enemyState = EnemyState.none;
    [Range(1.0f, 10.0f)]
    public float enemyInteractionRadius = 2.0f;
    public float enemyForgetRadius = 15.0f;
}