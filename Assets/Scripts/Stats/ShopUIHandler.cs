using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUIHandler : MonoBehaviour
{
    public TextMeshProUGUI nameUI;
    public Canvas canvas;
    public GameObject mainCamera;
    private Vector3 canvasRot = Vector3.zero;

    public ShopBlueprint shopBP;

    // Start is called before the first frame update
    void Start()
    {
        nameUI.text = shopBP.shopName;
    }

    // Update is called once per frame
    void Update()
    {
        #region Custom UI rotate
        // Rotate the UI to fave the camera
        canvasRot.y = Quaternion.Slerp(canvas.transform.rotation,
                                             Quaternion.LookRotation(canvas.transform.position - mainCamera.transform.position),
                                             2.0f * Time.deltaTime).eulerAngles.y;
        canvasRot.y = Mathf.Clamp(canvasRot.y, 25.0f, 60.0f);

        canvas.transform.rotation = Quaternion.Euler(canvasRot);
        #endregion
    }
}
