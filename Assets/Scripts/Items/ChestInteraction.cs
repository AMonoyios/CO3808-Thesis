using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChestInteraction : InteractPoint
{
    [Header("Controllers")]
    public FocusController focusController;
    public Animator animator;

    [Header("Chest Loot")]
    public bool hasBeenLooted = false;
    public GameObject[] ChestLootCount;

    [Header("Loot Spawn properties")]
    public float UpForce;
    public float SideForce;
    public Vector3 LootSpawnPoint;
    public float xOffset, yOffset, zOffset = 0.0f;

    private string FocusChestName;

    void Awake()
    {
        GameObject Player = GameObject.Find("Player");
        focusController = Player.GetComponent<FocusController>();
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
        Debug.Log("DEBUG: Chest " + FocusChestName + " Opening");

        animator = focusController.focus.GetComponent<Animator>();
        animator.SetBool("Open_Chest", true);

        if (!hasBeenLooted)
        {
            Debug.Log("DEBUG: Looting " + FocusChestName);

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
            Debug.Log("DEBUG: Chest " + FocusChestName + " already looted");
        }
    }

    void CloseChest()
    {
        Debug.Log("DEBUG: Chest " + FocusChestName + " Closing");

        animator.SetBool("Open_Chest", false);
    }

    void SpawnChestLoot(int ChestLootIndex)
    {
        Debug.Log("DEBUG: Spawning " + ChestLootCount[ChestLootIndex].name);

        // Calculate the spawn velocity of the loot
        float xForce = Random.Range(-SideForce, 0);
        float yForce = UpForce;
        float zForce = Random.Range(-SideForce, SideForce);
        Vector3 force = new Vector3(xForce, yForce, zForce);

        // Spawn Indexed loot
        GameObject NewLoot = (GameObject)Instantiate(ChestLootCount[ChestLootIndex], LootSpawnPoint, Quaternion.Euler(-90,0,0));

        // Apply velocity to the newly spawned loot
        NewLoot.GetComponent<Rigidbody>().velocity = force;
    }
}
