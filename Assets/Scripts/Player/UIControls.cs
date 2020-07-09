using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIControls: MonoBehaviour
{
    private TextMeshProUGUI ConsoleBoxGUI;

    [Header("Custom Console UI settings")]
    public GameObject DeveloperConsoleBox;
    public KeyCode ConsoleKeybind;
    private bool DeveloperConsoleToggleState;

    [Header("Inventory UI settings")]
    public GameObject InventoryUI;
    public KeyCode InventoryKeybind;
    private bool InventoryToggleState;

    [HideInInspector]
    public List<KeyCode> AllKeybinds = new List<KeyCode>();

    // Start is called before the first frame update
    void Start()
    {
        // Close the Developer Console when the game starts
        DeveloperConsoleToggleState = false;
        DeveloperConsoleBox.SetActive(DeveloperConsoleToggleState);

        // Close the inventory when the game starts
        InventoryToggleState = false;
        InventoryUI.SetActive(InventoryToggleState);
    }

	void Awake()
	{
        FindConsoleBoxGUI();

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

        if (Input.GetKeyDown(ConsoleKeybind))
        {
            DeveloperConsoleToggleState = !DeveloperConsoleToggleState;
            DeveloperConsoleBox.SetActive(DeveloperConsoleToggleState);
        }
    }

    void FindConsoleBoxGUI()
    {
        // B19 fix, because the script inherits from another one instead of monobehaviour i can 
        //  not trigger awake when the object is being instansiated. TODO: try finding a more 
        //  efficient way to get the text meshproGUI instead of find();
        GameObject CustomConsoleBox = GameObject.Find("Developer Console");
        ConsoleBoxGUI = CustomConsoleBox.GetComponent<TextMeshProUGUI>();
    }

    void CheckKeybinds(List<KeyCode> AllKeybinds)
	{
        AllKeybinds.Add(ConsoleKeybind);
        AllKeybinds.Add(InventoryKeybind);

		for (int i = 0; i < AllKeybinds.Count; i++)
		{
			if (AllKeybinds[i] == KeyCode.None)
			{
                ConsoleBoxGUI.text += "WARNING - Player: " + AllKeybinds[i] + " is unsigned \n";
                Debug.LogWarning("WARNING - Player: " + AllKeybinds[i] + " is unsigned");
			}
		}

		for (int i = 0; i < AllKeybinds.Count; i++)
		{
			for (int j = 0; j < AllKeybinds.Count; j++)
			{
				if (AllKeybinds[i] == AllKeybinds[j] && i != j)
				{
                    ConsoleBoxGUI.text += "WARNING - Player: " + AllKeybinds[i] + " is signed to two actions \n";
                    Debug.LogWarning("WARNING - Player: " + AllKeybinds[i] + " is signed to two actions");
				}
			}
		}
    }
}
