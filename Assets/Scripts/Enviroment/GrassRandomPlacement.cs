using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GrassRandomPlacement : MonoBehaviour
{
    private TextMeshProUGUI DeveloperConsoleBox;

    public GameObject grassPrefab;
    public int PrefabsToSpawn;

    public Transform[] grassPrefabs;
    public float grassRange;

    private void Awake()
    {
        DeveloperConsoleBox = FindObjectOfType<TextMeshProUGUI>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //if (grassPrefabs.Length <= 0)
        //{
        //    DeveloperConsoleBox.text += "WARNING - ENVIROMENT: grass " + transform.name + " has 0 quantity \n";
        //    Debug.LogWarning("WARNING - ENVIROMENT: grass " + transform.name + " has 0 quantity");
        //}
        //else
        //{
        //    for (int i = 0; i < grassPrefabs.Length; i++)
        //    {                
        //        Vector2 randVector2 = new Vector2(Random.Range(-grassRange, grassRange), Random.Range(-grassRange, grassRange));
        //        grassPrefabs[i].position = new Vector3(grassPrefabs[i].position.x + randVector2.x, grassPrefabs[i].position.y, grassPrefabs[i].position.z + randVector2.y);
        //    }
        //}

        if (PrefabsToSpawn <= 0)
        {
            Debug.LogWarning("WARNING - ENVIROMENT: grass " + transform.name + " has 0 quantity");
        }
        else
        {
            for (int i = 0; i < PrefabsToSpawn; i++)
            {
                // spawn a prefab

                // calculate new random position
                Vector2 randVector2 = new Vector2(Random.Range(-grassRange, grassRange), Random.Range(-grassRange, grassRange));
                
                // apply position to new prefab
                grassPrefabs[i].position = new Vector3(grassPrefab.transform.position.x + randVector2.x, grassPrefab.transform.position.y, grassPrefab.transform.position.z + randVector2.y);
            }
        }
    }
}
