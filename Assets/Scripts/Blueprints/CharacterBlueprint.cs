using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBlueprint : ScriptableObject
{
    [Header("Character Properties")]
    public GameObject prefab;
    public string prefabName = "New Prefab";
    [Range(0.5f, 4.0f)]
    public float movingSpeed = 1.5f;
    [Range(100.0f, 300.0f)]
    public float rotationSpeed = 200.0f;
}
