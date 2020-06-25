using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassRandomPlacement : MonoBehaviour
{
    public Transform[] grassPrefabs;
    public float grassRange;

    // Start is called before the first frame update
    void Start()
    {
        if (grassPrefabs.Length <= 0)
        {
            Debug.LogWarning("WARNING - ENVIROMENT: grass " + transform.name + " has 0 quantity");
        }
        else
        {
            for (int i = 0; i < grassPrefabs.Length; i++)
            {                
                Vector2 randVector2 = new Vector2(Random.Range(-grassRange, grassRange), Random.Range(-grassRange, grassRange));
                grassPrefabs[i].position = new Vector3(grassPrefabs[i].position.x + randVector2.x, grassPrefabs[i].position.y, grassPrefabs[i].position.z + randVector2.y);
            }
        }
    }
}
