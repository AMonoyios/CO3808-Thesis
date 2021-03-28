﻿using System.Collections;
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

	public Color SelectedGizmo;

	public Color FocusedGizmo;

	public Color PatrolingEnemyGizmo;

	public Color AttackingEnemyGizmo;

	public Color EvadingEnemyGizmo;

	public Color IdleNPCGizmo;

	public Color FocusedNPCGizmo;

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
