using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FocusController : MonoBehaviour
{
    private Camera cam;
    private float rayCastRange = 30.0f;

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
        if (Input.GetMouseButtonDown(0))
        {
            // creating a ray from the screen to a point in game
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // if the ray hits a specific "layer" in game
            if (Physics.Raycast(ray, out hit, rayCastRange, FocusMask))
            {
                InteractPoint interaction = hit.collider.GetComponent<InteractPoint>();

                if (interaction != null)
                {
                    // focus player to specific item
                    Debug.Log("DEBUG: focused " + hit.collider.name);
                    
                    Focus(interaction);
                }
                else
                {
                    // defocus player from any other focused items
                    Debug.Log("DEBUG: defocused " + hit.collider.name);
            
                    DeFocus();
                }
            }

            //// if the ray hits a specific "layer" in game
            //if (Physics.Raycast(ray, out hit, rayCastRange, FocusMask))
            //{
            //    InteractPoint interaction = hit.collider.GetComponent<InteractPoint>();
            //    if (interaction != null)
            //    {
            //    }
            //}
        }
    }

    void Focus (InteractPoint newFocus)
    {
        focus = newFocus;
    }

    void DeFocus()
    {
        focus = null;
    }
}
