﻿using UnityEngine;
using TMPro;

public class InteractPoint : MonoBehaviour
{
    private TextMeshProUGUI ConsoleBoxGUI;

    [Header("Interact Point Properties")]
    public Transform interactionPoint;
    [Range(1.25f, 4.0f)]
    public float radius = 2.0f;
    
    bool isFocus = false;
    bool hasInteracted = false;

    Transform player;

    // We want to interact with all interactables but not all interactables have the same interaction
    // enemies have health attack etc but a loot chest has loot in it
    public virtual void Interact()
    {
		if (FindConsoleBoxGUI())
            ConsoleBoxGUI.text += "DEBUG - PLAYER: Interacting with " + transform.name + "\n";

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

    // Visualizes a sphere around an interactable object, for developing ease
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(interactionPoint.position, radius);

        // Bug fix B7 
        if (interactionPoint == null)
        {
            interactionPoint = transform;
        }
    }

    bool FindConsoleBoxGUI()
    {
		// B19 fix, because the script inherits from another one instead of monobehaviour i can 
		//  not trigger awake when the object is being instansiated. TODO: try finding a more 
		//  efficient way to get the text meshproGUI instead of find();

		// B20 when the custom console box is inactive it does not find the gameobject
		try
		{
            GameObject CustomConsoleBox = GameObject.Find("Developer Console");
            ConsoleBoxGUI = CustomConsoleBox.GetComponent<TextMeshProUGUI>();
		}
		catch (System.Exception)
		{
            return false;
		}

        return true;
    }
}