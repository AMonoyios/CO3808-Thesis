using UnityEngine;

public class MiscItemProperties : MonoBehaviour
{
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
