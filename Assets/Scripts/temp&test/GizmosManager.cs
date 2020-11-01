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

	// Visualizes gizmos to all interactables in editor window for developing ease
	void OnDrawGizmos()
	{
		foreach (GameObject obj in FocusObjects)
		{ 
			if (UnityEditor.Selection.activeGameObject == this.gameObject)
			{
				if (obj.GetComponent<InteractPoint>().isSelected)
				{
					obj.GetComponent<InteractPoint>().isSelected = false;
					return;
				}

				if (obj.GetComponent<InteractPoint>().isFocus)
				{
					// Draw gizmos for focused items
					Gizmos.color = FocusedGizmo * FocusedIntensity;
					Gizmos.DrawWireSphere(obj.GetComponent<InteractPoint>().interactionPoint.position, obj.GetComponent<InteractPoint>().radius);
				}
				else
				{
					// Draw unselected gizmos
					Gizmos.color = UnselectedGizmo * UnselectedIntensity;
					Gizmos.DrawWireSphere(obj.GetComponent<InteractPoint>().interactionPoint.position, obj.GetComponent<InteractPoint>().radius);
				}

				if (obj.GetComponent<InteractPoint>().interactionPoint == null)
				{
					obj.GetComponent<InteractPoint>().interactionPoint = transform;
				}
			}
			else
			{
				return;
			}

		}
	}
	void OnDrawGizmosSelected()
	{
		foreach (GameObject obj in FocusObjects)
		{
			obj.GetComponent<InteractPoint>().isSelected = true;
		
			// Draw selected gizmos
			Gizmos.color = SelectedGizmo * SelectedIntensity;
			Gizmos.DrawWireSphere(obj.GetComponent<InteractPoint>().interactionPoint.position, obj.GetComponent<InteractPoint>().radius);
		}
	}
}
