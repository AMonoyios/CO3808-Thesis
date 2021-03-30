using UnityEngine.EventSystems;
using UnityEngine;


public class MovementController : MonoBehaviour
{
    public float MovementSpeed;
    public float RotationSpeed;

    public AllCharacterStats characterStats;
    private PlayerManager playerManager;

    Quaternion newRotation;

    private void Start()
    {
        // Get player instance
        playerManager = PlayerManager.Instance.player.GetComponent<PlayerManager>();
        characterStats = playerManager.player.GetComponent<AllCharacterStats>();
    }

    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");
        
        Vector3 movement = new Vector3(moveVertical, 0.0f, -moveHorizontal);

        if (movement != Vector3.zero)
            newRotation = Quaternion.LookRotation(movement);

        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, Time.deltaTime * RotationSpeed);
            
        transform.Translate(movement * MovementSpeed * Mathf.Clamp(characterStats.Speed, 0.25f, float.MaxValue) * Time.deltaTime, Space.World);

        if (Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log(MovementSpeed * characterStats.Speed);
        }
    }
}
