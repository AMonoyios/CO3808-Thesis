using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIControls: MonoBehaviour
{
    //private TextMeshProUGUI ConsoleBoxGUI;

    //[Header("Custom Console UI settings")]
    //public GameObject DeveloperConsoleBox;
    //public KeyCode ConsoleKeybind;
    //private bool DeveloperConsoleToggleState;

    [Header("Inventory UI settings")]
    public GameObject InventoryUI;
    public KeyCode InventoryKeybind;
    private bool InventoryToggleState;

    [Header("Equipment UI settings")]
    public GameObject EquipmentUI;
    public KeyCode EquipmentKeybind;
    private bool EquipmentToggleState;

    [HideInInspector]
    public List<KeyCode> AllKeybinds = new List<KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        //// Close the Developer Console when the game starts
        //DeveloperConsoleToggleState = false;
        //DeveloperConsoleBox.SetActive(DeveloperConsoleToggleState);

        // Close the inventory when the game starts
        InventoryToggleState = false;
        InventoryUI.SetActive(InventoryToggleState);

        // Close the equipment when the game starts
        EquipmentToggleState = false;
        EquipmentUI.SetActive(EquipmentToggleState);
    }

	void Awake()
	{
        CheckKeybinds(AllKeybinds);
    }

	// Update is called once per frame
	void Update()
    {
        if (Input.GetKeyDown(InventoryKeybind))
        {
            InventoryToggleState = !InventoryToggleState;
            InventoryUI.SetActive(InventoryToggleState);
        }

        if (Input.GetKeyDown(EquipmentKeybind))
        {
            EquipmentToggleState = !EquipmentToggleState;
            EquipmentUI.SetActive(EquipmentToggleState);
        }

        //if (Input.GetKeyDown(ConsoleKeybind))
        //{
        //    DeveloperConsoleToggleState = !DeveloperConsoleToggleState;
        //    DeveloperConsoleBox.SetActive(DeveloperConsoleToggleState);
        //}
    }

    void CheckKeybinds(List<KeyCode> AllKeybinds)
	{
        //AllKeybinds.Add(ConsoleKeybind);
        AllKeybinds.Add(InventoryKeybind);
        AllKeybinds.Add(EquipmentKeybind);

        for (int i = 0; i < AllKeybinds.Count; i++)
		{
			if (AllKeybinds[i] == KeyCode.None)
			{
                Debug.LogWarning("WARNING - Player: " + AllKeybinds[i] + " is unsigned");
			}
		}

		for (int i = 0; i < AllKeybinds.Count; i++)
		{
			for (int j = 0; j < AllKeybinds.Count; j++)
			{
				if (AllKeybinds[i] == AllKeybinds[j] && i != j)
				{
                    Debug.LogWarning("WARNING - Player: " + AllKeybinds[i] + " is signed to two actions");
				}
			}
		}
    }
}
