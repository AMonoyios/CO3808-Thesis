using UnityEngine;
using TMPro;
using UnityEditor;
using System.Collections;

public class InteractPoint : MonoBehaviour
{
    //private TextMeshProUGUI ConsoleBoxGUI;

    [Header("Interact Point Properties")]
    public Transform interactionPoint;
    [Range(1.25f, 4.0f)]
    public float radius = 2.0f;
    
    [Header("Gizmo Reference")]
    // Cache Gizmos Script
    public GizmosManager gizmos;

    [HideInInspector]
    public bool isFocus = false;
    [HideInInspector]
    public bool hasInteracted = false;
    [HideInInspector]
    public bool isSelected;

	// Used the Player Instance Class to avoid slowdowns
	// For this class we initialize it as public so the 
	// classes that inherit from it have access to it.
	public GameObject playerInstance;
	//Transform player;

	void Start()
	{
		playerInstance = PlayerManager.Instance.player;
		gizmos = GameObject.Find("GizmoManager").GetComponent<GizmosManager>();
	}

	// We want to interact with all interactables but not all interactables have the same interaction
	// enemies have health attack etc but a loot chest has loot in it
	public virtual void Interact()
    {
        //if (FindConsoleBoxGUI())
        //    ConsoleBoxGUI.text += "DEBUG - PLAYER: Interacting with " + transform.name + "\n";

        gizmos.RemoveFocusObjFromArray(this.gameObject);

        Debug.Log("DEBUG - PLAYER: Interacting with " + transform.name);
    }

    void Update()
    {
        // we want to interact with an interactable when we are focused and we aren't already interacting with it
        if (isFocus && hasInteracted != true)
        {
            // toggling the interact via comparing range of interactable item and player
            float distance = Vector3.Distance(playerInstance.transform.position, interactionPoint.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    // setting the focus to the new one
    public void OnFocused()
    {
        isFocus = !isFocus;
        hasInteracted = false;
    }

    // "Reset" focus for a new one
    public void OnDeFocused()
    {
        isFocus = !isFocus;
    }

#if UNITY_EDITOR
	// Visualizes gizmos to all interactables in editor window for developing ease
	void OnDrawGizmos()
	{
		if (gizmos.FocusObjects.Count > 0)
		{
			foreach (GameObject obj in gizmos.FocusObjects)
			{
				if (obj.name == this.name)
				{
					if (isSelected)
					{
						isSelected = false;
						return;
					}
					
					if (isFocus)
					{
						// Draw gizmos for focused items
						Gizmos.color = gizmos.FocusedGizmo * gizmos.FocusedIntensity;
						Gizmos.DrawWireSphere(interactionPoint.position, radius);
					}
					else
					{
						// Draw unselected gizmos
						Gizmos.color = gizmos.UnselectedGizmo * gizmos.UnselectedIntensity;
						Gizmos.DrawWireSphere(interactionPoint.position, radius);
					}
				}
			}
		}
	}

	void OnDrawGizmosSelected()
	{
		isSelected = true;

		// Draw selected gizmos
		Gizmos.color = gizmos.SelectedGizmo * gizmos.SelectedIntensity;
		Gizmos.DrawWireSphere(interactionPoint.position, radius);
	}
#endif

	//bool FindConsoleBoxGUI()
	//{
	//	// B19 fix, because the script inherits from another one instead of monobehaviour i can 
	//	//  not trigger awake when the object is being instansiated. TODO: try finding a more 
	//	//  efficient way to get the text meshproGUI instead of find();
	//
	//	// B20 when the custom console box is inactive it does not find the gameobject
	//	try
	//	{
	//        GameObject CustomConsoleBox = GameObject.Find("Developer Console");
	//        ConsoleBoxGUI = CustomConsoleBox.GetComponent<TextMeshProUGUI>();
	//	}
	//	catch (System.Exception)
	//	{
	//        return false;
	//	}
	//
	//    return true;
	//}
}