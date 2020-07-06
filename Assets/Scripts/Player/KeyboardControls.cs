using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControls : MonoBehaviour
{
    public GameObject DeveloperConsoleBox;
    private bool DeveloperConsoleToggleState;

    public GameObject InventoryUI;
    private bool InventoryToggleState;

    // Start is called before the first frame update
    void Start()
    {
        // Close the Developer Console when the game starts
        DeveloperConsoleToggleState = false;
        DeveloperConsoleBox.SetActive(DeveloperConsoleToggleState);

        // Close the inventory when the game starts
        InventoryToggleState = false;
        InventoryUI.SetActive(InventoryToggleState);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryToggleState = !InventoryToggleState;
            InventoryUI.SetActive(InventoryToggleState);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DeveloperConsoleToggleState = !DeveloperConsoleToggleState;
            DeveloperConsoleBox.SetActive(DeveloperConsoleToggleState);
        }
    }
}
