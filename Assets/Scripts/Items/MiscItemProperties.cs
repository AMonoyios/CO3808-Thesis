﻿using UnityEngine;
using TMPro;

public class MiscItemProperties : MonoBehaviour
{
    private TextMeshProUGUI ConsoleBoxGUI;

    private void Awake()
    {
        FindConsoleBoxGUI();
    }

    // Update is called once per frame
    void Update()
    {
        DeleteUnpickableItems();

        IgnorePlayerCollision();
        IgnoreOtherInteractablesCollision();
    }

    void DeleteUnpickableItems()
    {
        // delete items outside the playable area
        if (transform.position.y <= -20)
        {
            ConsoleBoxGUI.text += "WARNING - ITEM: Deleted " + this.gameObject.name + " because it is out of player bounds. \n";
            Debug.LogWarning("WARNING - ITEM: Deleted " + this.gameObject.name + " because it is out of player bounds.");
            Destroy(this.gameObject);
        }
    }

    void IgnorePlayerCollision()
    {
        // ignore collision with player and focus items
        Physics.IgnoreLayerCollision(8, 10, true);
    }

    void IgnoreOtherInteractablesCollision()
    {
        // ignore collision with player and focus items
        Physics.IgnoreLayerCollision(8, 8, true);
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
