using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;

public class FocusController : MonoBehaviour
{
    private Camera cam;

    [Range(10.0f,40.0f)]
    public float rayCastRange = 30.0f;

    public InteractPoint focus;
    public LayerMask FocusMask;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // when hitting the left mouse button 
        // B22 fix, player interacting with items behind the UI interface
        if (!EventSystem.current.IsPointerOverGameObject() && Input.GetMouseButtonDown(0))
        {
            // creating a ray from the screen to a point in game
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            // if the ray hits an object with a focus layer
            if (Physics.Raycast(ray, out RaycastHit hit, rayCastRange, FocusMask))  // inline variable declaration for RaycastHit
            {
                InteractPoint interaction = hit.collider.GetComponent<InteractPoint>();

                // if the ray actually hit anything
                if (interaction != null)
                {
                    // if we don't have a focused item already
                    if (focus == null)
                    {
                        Focus(interaction);
                    }

                    // if the item that we are trying to focus is not already focused
                    else if (focus.name != interaction.name)
                    {
                        DeFocus();
                        Focus(interaction);
                    }
                }
            }
            else if (Physics.Raycast(ray, rayCastRange) && focus != null)
            {
                DeFocus();
            }
        }
    }

    // focusing a specific interactable item
    void Focus (InteractPoint newFocus)
    {
        Debug.Log("DEBUG - PLAYER: focusing " + newFocus.name);
    
        focus = newFocus;
        newFocus.OnFocused();
    }

    // Defocusing interactable item
    public void DeFocus()
    {
        Debug.Log("DEBUG - PLAYER: defocusing " + focus.name);
        
        focus.OnDeFocused();
        focus = null;
    }
}
