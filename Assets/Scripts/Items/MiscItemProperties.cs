using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MiscItemProperties : MonoBehaviour
{
    public TextMeshProUGUI DeveloperConsoleBox;

    private void Awake()
    {
        DeveloperConsoleBox = FindObjectOfType<TextMeshProUGUI>();
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
            DeveloperConsoleBox.text += "WARNING - ITEM: Deleted " + this.gameObject.name + " because it is out of player bounds. \n";
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
}
