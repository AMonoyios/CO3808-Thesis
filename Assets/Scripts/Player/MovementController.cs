using UnityEngine.EventSystems;
using UnityEngine;


public class MovementController : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;

    Quaternion newRotation;

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        
        Vector3 movement = new Vector3(-moveVertical, 0.0f, moveHorizontal);

        if (movement != Vector3.zero)
            newRotation = Quaternion.LookRotation(movement);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * RotationSpeed);
            
        transform.Translate(movement * MovementSpeed * Time.deltaTime, Space.World);
    }
}
