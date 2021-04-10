using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class EnemyUIHandler : MonoBehaviour
{
    public TextMeshProUGUI nameUI;
    public Slider healthUI;
    public Canvas canvas;
    GameObject mainCamera;
    private Vector3 canvasRot = Vector3.zero;

    PlayerAttack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerAttack = transform.GetComponent<PlayerAttack>();
        
        nameUI.text = playerAttack.enemy.prefabName;
        healthUI.maxValue = playerAttack.enemy.maxHealth;
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

        healthUI.value = playerAttack.health;
    }
}
