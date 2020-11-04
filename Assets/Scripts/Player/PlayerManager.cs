using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    // Declared an instance for the player to prevent 
    // GameObject.Find("...") every time we need player information

    [HideInInspector]
    public GameObject player;
    public static PlayerManager Instance { get; private set; } // static singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // Cache references of the player
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
