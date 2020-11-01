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
    public GameObject gizmoManager;
    public GizmosManager gizmos;

    [HideInInspector]
    public bool isFocus = false;
    [HideInInspector]
    public bool hasInteracted = false;
    [HideInInspector]
    public bool isSelected;

    Transform player;

    // We want to interact with all interactables but not all interactables have the same interaction
    // enemies have health attack etc but a loot chest has loot in it
    public virtual void Interact()
    {
        //if (FindConsoleBoxGUI())
        //    ConsoleBoxGUI.text += "DEBUG - PLAYER: Interacting with " + transform.name + "\n";

        gizmos.RemoveFocusObjFromArray(this.gameObject);

        Debug.Log("DEBUG - PLAYER: Interacting with " + transform.name);
    }

    private void Start()
    {
        gizmoManager = GameObject.Find("GizmoManager");
        gizmos = gizmoManager.GetComponent<GizmosManager>();
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