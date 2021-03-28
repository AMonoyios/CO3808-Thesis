using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    NPC,
    Enemy
}

public class AiSpawner : MonoBehaviour
{
    public CharacterType characterType = CharacterType.NPC;

    public bool spawnSpecificCharacters = true;

    public List<NPCBlueprint> passiveNPCBlueprints;
    public List<EnemyBlueprint> EnemyBlueprints;

    public int charactersToSpawn = 1;

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
                    obj = Instantiate(EnemyBlueprints[count].prefab);
                    obj.name = EnemyBlueprints[count].prefabName;
                }
                else
                {
                    obj = Instantiate(passiveNPCBlueprints[count].prefab);
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
                    
                    obj = Instantiate(EnemyBlueprints[randomEnemyToSpawn].prefab);
                    obj.name = EnemyBlueprints[randomEnemyToSpawn].prefabName;
                }
                else
                {
                    int randomNPCToSpawn = Random.Range(0, EnemyBlueprints.Count);

                    obj = Instantiate(passiveNPCBlueprints[randomNPCToSpawn].prefab);
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