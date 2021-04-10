using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AiSpawner))]
public class AiSpawnerEditor : Editor
{
	public override void OnInspectorGUI()
    {
        AiSpawner spawner = target as AiSpawner;

        spawner.characterType = (CharacterType)EditorGUILayout.EnumPopup("Character Type", spawner.characterType);
        spawner.spawnSpecificCharacters = EditorGUILayout.Toggle("Spawn Specific Character", spawner.spawnSpecificCharacters);

        int listCount;
        // enemy list
        if (spawner.characterType == CharacterType.Enemy)
        {
            listCount = Mathf.Max(0, EditorGUILayout.IntField("Enemies Blueprints", spawner.EnemyBlueprints.Count));

            while (listCount < spawner.EnemyBlueprints.Count)
            {
                spawner.EnemyBlueprints.RemoveAt(spawner.EnemyBlueprints.Count - 1);
            }
            while (listCount > spawner.EnemyBlueprints.Count)
            {
                spawner.EnemyBlueprints.Add(null);
            }

            for (int i = 0; i < spawner.EnemyBlueprints.Count; i++)
            {
                spawner.EnemyBlueprints[i] = (EnemyBlueprint)EditorGUILayout.ObjectField(spawner.EnemyBlueprints[i], typeof(EnemyBlueprint), true);
            }
        }
        // npc list
        else
        {
            listCount = Mathf.Max(0, EditorGUILayout.IntField("Passive NPCs Blueprints", spawner.passiveNPCBlueprints.Count));
            while (listCount < spawner.passiveNPCBlueprints.Count)
            {
                spawner.passiveNPCBlueprints.RemoveAt(spawner.passiveNPCBlueprints.Count - 1);
            }
            while (listCount > spawner.passiveNPCBlueprints.Count)
            {
                spawner.passiveNPCBlueprints.Add(null);
            }

            for (int i = 0; i < spawner.passiveNPCBlueprints.Count; i++)
            {
                spawner.passiveNPCBlueprints[i] = (NPCBlueprint)EditorGUILayout.ObjectField(spawner.passiveNPCBlueprints[i], typeof(NPCBlueprint), true);
            }
        }

        if (!spawner.spawnSpecificCharacters)
        {
            spawner.charactersToSpawn = EditorGUILayout.IntSlider(spawner.charactersToSpawn, 1, 20);
		}

        spawner.characterParent = (CharacterParent)EditorGUILayout.EnumPopup("Character Parent", spawner.characterParent);
        if (spawner.characterParent == CharacterParent.Parent)
		{
            spawner.parent = (Transform)EditorGUILayout.ObjectField(spawner.parent, typeof(Transform), true);
		}
    }
}

