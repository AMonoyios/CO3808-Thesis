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

            // if the ray hits an object with a focus layer
            if (Physics.Raycast(ray, out RaycastHit hit, rayCastRange, FocusMask))  // inline variable declaration for RaycastHit
            {
                InteractPoint interaction = hit.collider.GetComponent<InteractPoint>();

                if (interaction != null)
                {
                    Debug.Log("DEBUG: focusing " + interaction.name);
                    Focus(interaction);
                }
            }
            else if (Physics.Raycast(ray, rayCastRange) && focus != null)
            {
                Debug.Log("DEBUG: defocusing " + focus.name);
                DeFocus();
            }
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
