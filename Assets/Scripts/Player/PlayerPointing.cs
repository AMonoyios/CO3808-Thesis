using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointing : MonoBehaviour
{
    private bool hasIndicatorSpawned = false;

    private GameObject hoverOverObject;
    
    private LayerMask DesiredLayerMask;
    private readonly float rayRange = 30.0f;

    RaycastHit hit;

    private Vector3 CurrentMousePosition;

    [Range(0.01f,0.12f)]
    public float Offset = 0.05f; 
    GameObject ClonedModel;

    public Material GlowMaterial;
    new Renderer renderer;

    void Start()
    {
        DesiredLayerMask = LayerMask.NameToLayer("Focus");

        CurrentMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        // Get Position of mouse in world space
        Vector3 MousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, rayRange));

        // Get the Vector between camera position and mouse position
        Vector3 MouseDir = MousePos - Camera.main.transform.position;

        // Casting a ray whenever the mouse moves
        if (Input.mousePosition != CurrentMousePosition)
        {
            UpdateRay(MouseDir);
        }

        CurrentMousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
    }

    void UpdateRay(Vector3 MouseDirection)
    {
        if (Physics.Raycast(Camera.main.transform.position, MouseDirection, out hit, rayRange))
        {
            if (hit.transform.gameObject.layer == DesiredLayerMask)
            {
                DrawPointLine(Camera.main.transform.position, hit.point, Color.green);

                GameObject hitObject = hit.transform.root.gameObject;
                IndicateObject(hitObject);
            }
            else
            {
                DrawPointLine(Camera.main.transform.position, hit.point, Color.red);

                ClearIndicator();
            }
        }
    }

    void DrawPointLine(Vector3 startPos, Vector3 endPos, Color color)
    {
        Debug.DrawLine(startPos, endPos, color);
    }

    void IndicateObject(GameObject gameObject)
    {
        hoverOverObject = gameObject;

        // Indicate 
        if (!hasIndicatorSpawned)
        {
            Quaternion hoverObjectRotation = new Quaternion(hoverOverObject.transform.rotation.x, hoverOverObject.transform.rotation.y, hoverOverObject.transform.rotation.z, hoverOverObject.transform.rotation.w);
            ClonedModel = (GameObject)Instantiate(hoverOverObject, hoverOverObject.gameObject.transform.position, hoverObjectRotation);
            ClonedModel.name = "Cloned " + hoverOverObject.name;

            renderer = ClonedModel.GetComponent<Renderer>();
            renderer.material = GlowMaterial;

            Collider ClonedModelCollider = ClonedModel.GetComponent<Collider>();
            ClonedModelCollider.enabled = false;

            Vector3 ClonedModelScale = new Vector3(hoverOverObject.transform.localScale.x + Offset, hoverOverObject.transform.localScale.y + Offset, hoverOverObject.transform.localScale.z + Offset);
            ClonedModel.transform.localScale = ClonedModelScale;

            hasIndicatorSpawned = true;
        }
    }

    void ClearIndicator()
    {
        Destroy(ClonedModel);

        hasIndicatorSpawned = false;
        hoverOverObject = null;
    }
}
