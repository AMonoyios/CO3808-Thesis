using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUIHandler : MonoBehaviour
{
    Shop shop;

    [Header("Shop UI")]
    public GameObject shopCanvas;
    public TextMeshProUGUI shopName;
    public GameObject shopContent;
    public Object sellingItemPrefab;
    public Object positiveStatPrefab;
    public Object negativeStatPrefab;
    public bool isSetup = false;

    [Header("Character UI above head")]
    public TextMeshProUGUI characterNameUI;
    public Canvas characterHoverCanvas;
    public GameObject mainCamera;
    private Vector3 canvasRot = Vector3.zero;

    [Header("Character shop blueprint")]
    public ShopBlueprint shopBP;

    [Header("Player properties")]
    public GameObject player;
    public FocusController focusController;
    public InteractPoint interactPoint;

    // Start is called before the first frame update
    void Start()
    {
        shop = transform.GetComponent<Shop>();
        player = GameObject.FindGameObjectWithTag("Player").gameObject;
        focusController = player.GetComponent<FocusController>();

        characterNameUI.text = shopBP.shopName;

        if (!isSetup)
        {
            shopCanvas.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        #region Custom UI rotate
        // Rotate the UI to fave the camera
        canvasRot.y = Quaternion.Slerp(characterHoverCanvas.transform.rotation,
                                             Quaternion.LookRotation(characterHoverCanvas.transform.position - mainCamera.transform.position),
                                             2.0f * Time.deltaTime).eulerAngles.y;
        canvasRot.y = Mathf.Clamp(canvasRot.y, 25.0f, 60.0f);

        characterHoverCanvas.transform.rotation = Quaternion.Euler(canvasRot);
        #endregion

        interactPoint = focusController.focus;

		if (shop.isBrowsing)
		{
            // Handle shop UI when player is close or not
			if (Vector3.Distance(transform.position, shop.playerManager.transform.position) < shop.radius)
			{
                shopCanvas.gameObject.SetActive(true);

				if (!isSetup)
				{
                    SetupUI();

                    isSetup = true;
				}
            }
			else
			{
                if (isSetup == true)
				{
                    Debug.Log("DEBUG - Stats: Closing shop...");

                    CloseShop();
				}
            }

			// Handle shop UI when player defocuses while browsing shop
			if (interactPoint == null)
			{
                CloseShop();
			}
		}
    }

    void SetupUI()
	{
        shopName.text = shopBP.shopName;

        for (int i = 0; i < shopBP.itemBlueprints.Count; i++)
        {
            Debug.Log("DEBUG - Shop: Setting up UI for item " + shopBP.itemBlueprints[i].item.ItemName + "...");
            
            // instantiating the prefab for the selling item
            GameObject item = (GameObject)Instantiate(sellingItemPrefab, shopContent.transform);
            
            // re-name the prefab
            item.name = shopBP.itemBlueprints[i].item.ItemName;

            // setting the item blueprint
            item.GetComponent<ShopBlueprintHolder>().itemBP = shopBP.itemBlueprints[i].item;

            // item icon
            Image itemIcon = item.gameObject.transform.Find("ItemIcon").GetComponent<Image>();
            itemIcon.overrideSprite = shopBP.itemBlueprints[i].item.ItemIcon;

            // item name
            GameObject itemName = item.gameObject.transform.Find("ItemName").gameObject;
            itemName.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.itemBlueprints[i].item.ItemName;

            // item description
            GameObject itemDesc = item.gameObject.transform.Find("ItemDescription").gameObject;
            itemDesc.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.itemBlueprints[i].item.ItemDescription;

            // items do not have positive or negative stats stats 
            item.gameObject.transform.Find("PositiveStatPanel").gameObject.SetActive(false);
            item.gameObject.transform.Find("NegativeStatPanel").gameObject.SetActive(false);

            // item price
            GameObject itemPrice = item.gameObject.transform.Find("BuyButton").gameObject;
            itemPrice.gameObject.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = shopBP.itemBlueprints[i].item.ItemPrice.ToString();

            Debug.Log("DEBUG - Shop: Finished UI setup for item " + shopBP.itemBlueprints[i].item.ItemName + "...");
        }

        for (int i = 0; i < shopBP.equipmentBlueprints.Count; i++)
        {
            Debug.Log("DEBUG - Shop: Setting up UI for equipment " + shopBP.equipmentBlueprints[i].equipment.ItemName + "...");

            // instantiate the selling item prefab
            GameObject equipment = (GameObject)Instantiate(sellingItemPrefab, shopContent.transform);

            // re-name the prefab
            equipment.name = shopBP.equipmentBlueprints[i].equipment.ItemName;

            // setting the equipment blueprint
            equipment.GetComponent<ShopBlueprintHolder>().itemBP = shopBP.equipmentBlueprints[i].equipment;

            // equipment icon
            Image equipmentIcon = equipment.gameObject.transform.Find("ItemIcon").GetComponent<Image>();
            equipmentIcon.overrideSprite = shopBP.equipmentBlueprints[i].equipment.ItemIcon;

            // equipment name
            GameObject equipmentName = equipment.gameObject.transform.Find("ItemName").gameObject;
            equipmentName.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.ItemName;

            // equipment description 
            GameObject equipmentDesc = equipment.gameObject.transform.Find("ItemDescription").gameObject;
            equipmentDesc.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.ItemDescription;

            // positive equipment stats
            GameObject equipmentPosStat = equipment.gameObject.transform.Find("PositiveStatPanel").gameObject;
			for (int posStatIndex = 0; posStatIndex < shopBP.equipmentBlueprints[i].equipment.PositiveTraits.Count; posStatIndex++)
			{
                // instantiating positive stat prefab
                GameObject posStat = (GameObject)Instantiate(positiveStatPrefab, equipmentPosStat.transform);
                
                // re-name the positive stat prefab
                posStat.name = shopBP.equipmentBlueprints[i].equipment.ItemName;

                // positive stat type
                GameObject posStatType = posStat.gameObject.transform.Find("StatType").gameObject;
                posStatType.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.PositiveTraits[posStatIndex].traits.ToString();

                // positive stat level
                GameObject posStatLevel = posStat.gameObject.transform.Find("StatLevel").gameObject;
                posStatLevel.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.PositiveTraits[posStatIndex].traitLevel.ToString();
            }

            // negative equipment stats
            GameObject equipmentNegStat = equipment.gameObject.transform.Find("NegativeStatPanel").gameObject;
            for (int negStatIndex = 0; negStatIndex < shopBP.equipmentBlueprints[i].equipment.NegativeTraits.Count; negStatIndex++)
            {
                // instantiating negative stat prefab
                GameObject negStat = (GameObject)Instantiate(negativeStatPrefab, equipmentNegStat.transform);

                // re-name the negative stat prefab
                negStat.name = shopBP.equipmentBlueprints[i].equipment.ItemName;

                // negative stat type
                GameObject negStatType = negStat.gameObject.transform.Find("StatType").gameObject;
                negStatType.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.NegativeTraits[negStatIndex].traits.ToString();

                // negative stat level
                GameObject negStatLevel = negStat.gameObject.transform.Find("StatLevel").gameObject;
                negStatLevel.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.NegativeTraits[negStatIndex].traitLevel.ToString();
            }

            // equipment price
            GameObject equipmentPrice = equipment.gameObject.transform.Find("BuyButton").gameObject;
            equipmentPrice.gameObject.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.ItemPrice.ToString();

            Debug.Log("DEBUG - Shop: Finished UI setup for equipment " + shopBP.equipmentBlueprints[i].equipment.ItemName + "...");
        }
    }

    void CloseShop()
	{
        // deactivate the shop
        shopCanvas.gameObject.SetActive(false);
        shop.isBrowsing = false;
        isSetup = false;

        // reset the shop UI
		foreach (Transform item in shopContent.transform)
		{
			Destroy(item.gameObject);
		}
    }
}
