using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    NPC,
    Enemy
}

public enum CharacterParent
{
    NoParent,
    Parent
}

public class AiSpawner : MonoBehaviour
{
    public CharacterType characterType = CharacterType.NPC;

    public bool spawnSpecificCharacters = true;

    public List<NPCBlueprint> passiveNPCBlueprints;
    public List<EnemyBlueprint> EnemyBlueprints;

    public int charactersToSpawn = 1;

    public CharacterParent characterParent = CharacterParent.NoParent;
    public Transform parent;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        int count = 0;
        GameObject obj = null;

		if (spawnSpecificCharacters)
		{
            while (count < charactersToSpawn)
            {
                if (characterType == CharacterType.Enemy)
                {
					if (characterParent == CharacterParent.NoParent)
					{
                        obj = Instantiate(EnemyBlueprints[count].prefab);
					}
					else
					{
                        obj = Instantiate(EnemyBlueprints[count].prefab, parent);
                    }

                    obj.name = EnemyBlueprints[count].prefabName;
                }
                else
                {
					if (characterParent == CharacterParent.NoParent)
					{
                        obj = Instantiate(passiveNPCBlueprints[count].prefab);
					}
					else
					{
                        obj = Instantiate(passiveNPCBlueprints[count].prefab, parent);
                    }

                    obj.name = passiveNPCBlueprints[count].prefabName;
                }

                count++;
            }
		}
		else
		{
            while (count < charactersToSpawn)
            {
                if (characterType == CharacterType.Enemy)
                {
                    int randomEnemyToSpawn = Random.Range(0, EnemyBlueprints.Count);

                    if (characterParent == CharacterParent.NoParent)
                    {
                        obj = Instantiate(EnemyBlueprints[randomEnemyToSpawn].prefab);
                    }
                    else
                    {
                        obj = Instantiate(EnemyBlueprints[randomEnemyToSpawn].prefab, parent);
                    }
                    
                    obj.name = EnemyBlueprints[randomEnemyToSpawn].prefabName;
                }
                else
                {
                    int randomNPCToSpawn = Random.Range(0, EnemyBlueprints.Count);

                    if (characterParent == CharacterParent.NoParent)
                    {
                        obj = Instantiate(passiveNPCBlueprints[randomNPCToSpawn].prefab);
                    }
                    else
                    {
                        obj = Instantiate(passiveNPCBlueprints[randomNPCToSpawn].prefab, parent);
                    }

                    obj.name = passiveNPCBlueprints[randomNPCToSpawn].prefabName;
                }

                count++;
            }
        }

        //Transform child = transform.GetChild(Random.Range(0, transform.childCount - 1));
        //obj.GetComponent<WaypointNavigator>().currentWaypoint = child.GetComponent<Waypoint>();
        //obj.transform.position = child.position;

        yield return new WaitForEndOfFrame();
    }
}