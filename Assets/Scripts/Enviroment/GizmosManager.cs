using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GizmosManager : MonoBehaviour
{
	// TODO NULL REFERENCE OnDrawGizmos()
	
	[Header("All interactable objects in scene")]
    public List<GameObject> FocusObjects;
    private GameObject[] gameObjects;
	
	[Header("Gizmos Properties")]
	public Color UnselectedGizmo;
	[Range(0.0f, 1.0f)]
	public float UnselectedIntensity = 1.0f;

	public Color SelectedGizmo;
	[Range(0.0f, 1.0f)]
	public float SelectedIntensity = 1.0f;

	public Color FocusedGizmo;
	[Range(0.0f, 1.0f)]
	public float FocusedIntensity = 1.0f;

	public Color PatrolingEnemyGizmo;
	[Range(0.0f, 1.0f)]
	public float PatrolingEnemyIntensity = 1.0f;

	public Color AttackingEnemyGizmo;
	[Range(0.0f, 1.0f)]
	public float AttackingEnemyIntensity = 1.0f;

	public Color EvadingEnemyGizmo;
	[Range(0.0f, 1.0f)]
	public float EvadingEnemyIntensity = 1.0f;

	public Color IdleNPCGizmo;
	[Range(0.0f, 1.0f)]
	public float IdleNPCIntensity = 1.0f;

	public Color FocusedNPCGizmo;
	[Range(0.0f, 1.0f)]
	public float FocusedNPCIntensity = 1.0f;

	// Start is called before the first frame update
	void Start()
    {
		// Initializing List of Gizmos
		InitializeFocusObjArray();
	}

	public void InitializeFocusObjArray()
	{
		// Find all gameobjects in Scene
		gameObjects = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		
		// Check one by one if they are in focus layer
		foreach (GameObject obj in gameObjects.ToList())
		{
			if (obj.layer == 8)
			{
				// Add them to the list
				FocusObjects.Add(obj);
			}
		}
	}

	public void AddFocusObjToArray(GameObject item)
	{
		// Check if item is in focus layer
		if (item.layer==8)
		{
			// Add it to the list
            FocusObjects.Add(item);
		}
	}

    public void RemoveFocusObjFromArray(GameObject item)
	{
		// Find the item in the list
		foreach (GameObject obj in FocusObjects.ToList())
		{
			if (obj.name == item.name)
			{
				// Remove it
				FocusObjects.Remove(obj);
			}
		}
	}

}
