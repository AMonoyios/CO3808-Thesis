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
	
	[Header("Interaction Colors")]
	public Color UnselectedGizmo;
	public Color SelectedGizmo;
	public Color FocusedGizmo;

	[Header("Enemy States Colors")]
	public Color PatrolingEnemyGizmo;
	public Color AttackingEnemyGizmo;
	public Color EvadingEnemyGizmo;

	[Header("NPC States Colors")]
	public Color IdleNPCGizmo;
	public Color FocusedNPCGizmo;

	[Header("Foliage Color")]
	public Color foliageGizmo;

	[Header("Shop Color")]
	public Color itemSpawnPoint;

	// Start is called before the first frame update
	void Start()
    {
		// Initializing List of Gizmos
		InitializeFocusObjArray();
	}

	public void UpdateGizmosList()
	{
		for (int i = 0; i < FocusObjects.Count - 1; i++)
		{
			if (FocusObjects[i] == null)
			{
				FocusObjects.RemoveAt(i);
			}

			if (GameObject.Find(FocusObjects[i].ToString()) == null)
			{
				FocusObjects.RemoveAt(i);
			}
		}
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
