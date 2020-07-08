using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FocusController : MonoBehaviour
{
    private TextMeshProUGUI ConsoleBoxGUI;
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

    private void Awake()
    {
        FindConsoleBoxGUI();
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
        ConsoleBoxGUI.text += "DEBUG - PLAYER: focusing " + newFocus.name + "\n";
        Debug.Log("DEBUG - PLAYER: focusing " + newFocus.name);
        focus = newFocus;
        newFocus.OnFocused(this.transform);
    }

    // Defocusing interactable item
    public void DeFocus()
    {
        ConsoleBoxGUI.text += "DEBUG - PLAYER: defocusing " + focus.name + "\n";
        Debug.Log("DEBUG - PLAYER: defocusing " + focus.name);
        focus.OnDeFocused();
        focus = null;
    }

    void FindConsoleBoxGUI()
    {
        // B19 fix, because the script inherits from another one instead of monobehaviour i can 
        //  not trigger awake when the object is being instansiated. TODO: try finding a more 
        //  efficient way to get the text meshproGUI instead of find();
        GameObject CustomConsoleBox = GameObject.Find("Developer Console");
        ConsoleBoxGUI = CustomConsoleBox.GetComponent<TextMeshProUGUI>();
    }
}
