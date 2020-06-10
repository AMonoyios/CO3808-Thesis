using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingSelection : MonoBehaviour
{
    public LayerMask Mask;
    public new Camera camera;

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(camera.transform.position, camera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            if (hit.transform.gameObject.layer == Mask)
            {
                Debug.Log("hit Focus");
            }
        }
    }
}
