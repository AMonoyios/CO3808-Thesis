using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class PlayerPointing : MonoBehaviour
{
    private bool hasIndicatorSpawned = false;

    private LayerMask DesiredLayerMask;
    private readonly float rayRange = 30.0f;
    RaycastHit hit;
    private Vector3 CurrentMousePosition;

    [Range(0.01f,0.12f)]
    public float Offset = 0.05f;
    public GameObject IndicatorPrefab;
    private GameObject SelectionModel;

    void Start()
    {
        DesiredLayerMask = LayerMask.NameToLayer("Focus");

        CurrentMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Makes sure that the player can not interact anything behind the InventoryUI
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            // Get Position of mouse in world space
            Vector3 MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, rayRange));

            // Get the Vector between camera position and mouse position
            Vector3 MouseDir = MousePos - Camera.main.transform.position;

            // Casting a ray whenever the mouse moves
            CurrentMousePosition = UpdateRay(MouseDir);
        }
    }

    Vector3 UpdateRay(Vector3 MouseDirection)
    {
        // Cast ray each time this function is called
        if (Physics.Raycast(Camera.main.transform.position, MouseDirection, out hit, rayRange))
        {
            // If Layer is correct then indicate
            if (hit.transform.gameObject.layer == DesiredLayerMask)
            {
                DrawPointLine(Camera.main.transform.position, hit.point, Color.green);

                GameObject hitObject = hit.transform.root.gameObject;
                IndicateObject(hitObject);
            }
            else // else try deleting the indicator
            {
                DrawPointLine(Camera.main.transform.position, hit.point, Color.red);

                ClearIndicator();
            }
            
            CurrentMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        }

        return CurrentMousePosition;
    }

    void DrawPointLine(Vector3 startPos, Vector3 endPos, Color color)
    {
        Debug.DrawLine(startPos, endPos, color);
    }

    void IndicateObject(GameObject hitObject)
    {
        if (!hasIndicatorSpawned)
        {
            // Calculate position of indicator
            Vector3 spawnPos = new Vector3(hitObject.transform.position.x,
                                      hitObject.transform.position.y,
                                      hitObject.transform.position.z);

            // Spawn Indicator prefab
            SelectionModel = (GameObject)Instantiate(IndicatorPrefab, spawnPos, Quaternion.identity);
            SelectionModel.transform.rotation *= Quaternion.Euler(-90,0,0);
            SelectionModel.name = hitObject.name + " Indicator";

            // Toggle Indicator boolean
            hasIndicatorSpawned = true;
        }
    }

    void ClearIndicator()
    {
        // Delete object
        Destroy(SelectionModel);

        // Toggle Interaction boolean
        hasIndicatorSpawned = false;
    }
}
