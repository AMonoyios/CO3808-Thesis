using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidableFoliage : MonoBehaviour
{
    public Material[] materials;
    
    Transform player;
    Vector3 playerPosition;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(editFoliageMaterial());
    }

    IEnumerator editFoliageMaterial()
    {
        while (true)
        {
            // get the player position and the material
            playerPosition = transform.position;
            for (int i = 0; i < materials.Length; i++)
            {
                // the shader property is referenced
                materials[i].SetVector("_playerposition", playerPosition);
            }

            yield return true;
        }
    }
}
