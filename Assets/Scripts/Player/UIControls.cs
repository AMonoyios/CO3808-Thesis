using UnityEngine;
using TMPro;

// idea #2
enum ValidKeybinds
{
    q,w,e,r,t,y,u,i,o,p,
    a,s,d,f,g,h,j,k,l,
    z,x,c,v,b,n,m
}

public class UIControls: MonoBehaviour
{
    private TextMeshProUGUI ConsoleBoxGUI;

    [Header("Custom Console UI settings")]
    public GameObject DeveloperConsoleBox;
    public string ConsoleKeybind;
    private bool DeveloperConsoleToggleState;

    [Header("Inventory UI settings")]
    public GameObject InventoryUI;
    public string InventoryKeybind;
    private bool InventoryToggleState;

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

        CheckKeybinds(ConsoleKeybind, InventoryKeybind);
    }

	// Update is called once per frame
	void Update()
    {
        // idea #1
		try
		{
            if (Input.GetKeyDown(InventoryKeybind))
            {
                InventoryToggleState = !InventoryToggleState;
                InventoryUI.SetActive(InventoryToggleState);
            }
		}
		catch
		{
            ConsoleBoxGUI.text += "WARNING - Player: Keybind for Inventory toggle is invalid -> " + InventoryKeybind + " was converted to 'i' \n";
            Debug.LogWarning("WARNING - Player: Keybind for Inventory toggle is invalid -> " + InventoryKeybind + " was converted to 'i' ");

            InventoryKeybind = "i";
		}

        if (Input.GetKeyDown(KeyCode.Space))
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

    void CheckKeybinds(string ConsoleKeybind, string InventoryKeybind)
	{
        try
        {
            
        }
        catch
        {
            ConsoleBoxGUI.text += "WARNING - Player: Keybind for Inventory toggle is invalid -> " + InventoryKeybind + " was converted to 'i' \n";
            Debug.LogWarning("WARNING - Player: Keybind for Inventory toggle is invalid -> " + InventoryKeybind + " was converted to 'i' ");

            InventoryKeybind = "i";
        }
    }
}
