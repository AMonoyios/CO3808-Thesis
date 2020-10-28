using UnityEngine;
using TMPro;
using UnityEditor;

public class InteractPoint : MonoBehaviour
{
    //private TextMeshProUGUI ConsoleBoxGUI;

    [Header("Interact Point Properties")]
    public Transform interactionPoint;
    [Range(1.25f, 4.0f)]
    public float radius = 2.0f;
    
    bool isFocus = false;
    bool hasInteracted = false;

    private bool isSelected;

    Transform player;

    // We want to interact with all interactables but not all interactables have the same interaction
    // enemies have health attack etc but a loot chest has loot in it
    public virtual void Interact()
    {
		//if (FindConsoleBoxGUI())
        //    ConsoleBoxGUI.text += "DEBUG - PLAYER: Interacting with " + transform.name + "\n";

        Debug.Log("DEBUG - PLAYER: Interacting with " + transform.name);
    }

    void Update()
    {
        // we want to interact with an interactable when we are focused and we aren't already interacting with it
        if (isFocus && hasInteracted != true)
        {
            // toggling the interact via comparing range of interactable item and player
            float distance = Vector3.Distance(player.position, interactionPoint.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }

    // setting the focus to the new one
    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    // "Reset" focus for a new one
    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
    }

    // Visualizes gizmos to all interactables in editor window for developing ease
    void OnDrawGizmos()
    {
        if (isSelected)
        {
            isSelected = false;
            return;
        }

		if (isFocus)
		{
            // Draw gizmos for focused items
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(interactionPoint.position, radius);
		}
		else
		{
            // Draw unselected gizmos
            Gizmos.color = Color.white * .5f;
            Gizmos.DrawWireSphere(interactionPoint.position, radius);
        }

        if (interactionPoint == null)
        {
            interactionPoint = transform;
        }
    }
    void OnDrawGizmosSelected()
    {
        isSelected = true;

        // Draw selected gizmos
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(interactionPoint.position, radius);
    }

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