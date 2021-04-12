using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootDrop
{
    public ItemBlueprint item = null;
    [Range(0.1f, 100.0f)]
    public float dropChance = 100.0f;
}

[System.Serializable]
public class CurrencyDrop
{
    public CurrencyBlueprint currency = null;
    [Range(0.1f, 100.0f)]
    public float dropChance = 100.0f;
}

[System.Serializable]
[CreateAssetMenu(fileName = "New Enemy", menuName = "Custom Scriptable Objects/NPC/Enemy")]
public class EnemyBlueprint : CharacterBlueprint
{
    [Header("Enemy Properties"), HideInInspector]
    public EnemyState enemyState = EnemyState.patrolling;

    [Header("Interactions radius")]
    [Range(1.0f, 3.0f)]
    public float enemyAttackRadius = 1.5f;
    [Range(1.0f, 10.0f)]
    public float enemySpotRadius = 2.0f;
    public float enemyForgetRadius = 15.0f;
    
    [Header("Small attack")]
    [Range(0.1f, 10.0f)]
    public float smallAttack = 5.0f;
    [Range(40.0f,50.0f)]
    public float smallAttackChance = 45.0f;
    
    [Header("Big attack")]
    [Range(10.1f, 25.0f)]
    public float bigAttack = 15.0f;
    [Range(20.0f, 35.0f)]
    public float bigAttackChance = 27.5f;
    
    [Header("Critical attack")]
    [Range(25.1f, 50.0f)]
    public float criticalAttack = 40.0f;
    [Range(2.5f,7.5f)]
    public float critiaclChance = 5.0f;

    [Header("Loot drops")]
    public List<LootDrop> loot;
    public List<CurrencyDrop> currency;
}