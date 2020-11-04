using System.Collections;
using UnityEngine;
using TMPro;

public class ChestInteraction : InteractPoint
{
    //TextMeshProUGUI ConsoleBoxGUI;

    private FocusController focusController;
    private Animator animator;

    [Header("Chest Loot")]
    public bool hasBeenLooted = false;
    public GameObject[] ChestLootCount;

    [Header("Loot Spawn properties")]
    public float xOffset = 0.0f;
    public float yOffset = 0.0f;
    public float zOffset = 0.0f;

    [Header("Loot Velocity Properites")]
    public float ForwardForce;
    public float UpwardForce;
    public float LeftToRightForce;

    private Vector3 LootSpawnPoint;
    private string FocusChestName;

    // Used the Player Instance Class to avoid slowdowns
    //private GameObject Player;
    
    //GameObject playerInstance;

    void Awake()
    {
        //Player = GameObject.Find("Player");
        //focusController = Player.GetComponent<FocusController>();

        // Get player instance
        playerInstance = PlayerManager.Instance.player;
        focusController = playerInstance.GetComponent<FocusController>();
    }

    public override void Interact()
    {
        base.Interact();
        
        CollectInfo();

        StartCoroutine(IOpenChest());
    }

    IEnumerator IOpenChest()
    {
        OpenChest();

        yield return new WaitForSeconds(3);

        CloseChest();

        focusController.DeFocus();
    }

    void CollectInfo()
    {
        FocusChestName = focusController.focus.name;
        LootSpawnPoint = new Vector3(transform.position.x + xOffset, transform.position.y + yOffset, transform.position.z + zOffset);
    }

    void OpenChest()
    {
		//if (FindConsoleBoxGUI())
        //    ConsoleBoxGUI.text += "DEBUG - ITEM: Chest " + FocusChestName + " Opening \n";
        Debug.Log("DEBUG - ITEM: Chest " + FocusChestName + " Opening");

        animator = focusController.focus.GetComponent<Animator>();
        animator.SetBool("Open_Chest", true);

        if (!hasBeenLooted)
        {
			//if (FindConsoleBoxGUI())
            //    ConsoleBoxGUI.text += "DEBUG - ITEM: Looting " + FocusChestName + "\n";
            Debug.Log("DEBUG - ITEM: Looting " + FocusChestName);

            // Spawn all loot of the chest
            for (int i = 0; i < ChestLootCount.Length; i++)
            {
                SpawnChestLoot(i);
            }

            // Toggle hasBeenLooted boolean
            hasBeenLooted = true;
        }
        else
        {
			// for now throw a message
			//if (FindConsoleBoxGUI())
            //    ConsoleBoxGUI.text += "DEBUG - ITEM: Chest " + FocusChestName + " already looted \n";
            Debug.Log("DEBUG - ITEM: Chest " + FocusChestName + " already looted");
        }
    }

    void CloseChest()
    {
		//if (FindConsoleBoxGUI())
        //    ConsoleBoxGUI.text += "DEBUG - ITEM: Chest " + FocusChestName + " Closing \n";
        Debug.Log("DEBUG - ITEM: Chest " + FocusChestName + " Closing");

        animator.SetBool("Open_Chest", false);
    }

    void SpawnChestLoot(int ChestLootIndex)
    {
		//if (FindConsoleBoxGUI())
        //    ConsoleBoxGUI.text += "DEBUG - ITEM: Spawning " + ChestLootCount[ChestLootIndex].name + "\n";
        Debug.Log("DEBUG - ITEM: Spawning " + ChestLootCount[ChestLootIndex].name);

        // Calculate the spawn velocity of the loot
        float xForce = Random.Range(ForwardForce / 2, ForwardForce) * -1;
        float yForce = Random.Range(UpwardForce / 2, UpwardForce);
        float zForce = Random.Range(-LeftToRightForce, LeftToRightForce);
        Vector3 itemSpawnForce = new Vector3(xForce, yForce, zForce);

        // Spawn Indexed loot
        GameObject NewLoot = (GameObject)Instantiate(ChestLootCount[ChestLootIndex], LootSpawnPoint, this.transform.rotation);//Quaternion.Euler(-90,0,0));

        // Apply velocity to the newly spawned loot (changed to relative force B12 fix)
        NewLoot.GetComponent<Rigidbody>().AddRelativeForce(itemSpawnForce);

        // Add Chest loot to list (Gizmos)
        gizmos.AddFocusObjToArray(NewLoot);
    }

    //bool FindConsoleBoxGUI()
    //{
    //    // B19 fix, because the script inherits from another one instead of monobehaviour i can 
    //    //  not trigger awake when the object is being instansiated. TODO: try finding a more 
    //    //  efficient way to get the text meshproGUI instead of find();
    //
    //    // B20 when the custom console box is inactive it does not find the gameobject
    //    try
    //    {
    //        GameObject CustomConsoleBox = GameObject.Find("Developer Console");
    //        ConsoleBoxGUI = CustomConsoleBox.GetComponent<TextMeshProUGUI>();
    //    }
    //    catch (System.Exception)
    //    {
    //        return false;
    //    }
    //
    //    return true;
    //}
}
