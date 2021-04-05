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
    public GameObject shopList;
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

    // Start is called before the first frame update
    void Start()
    {
        shop = transform.GetComponent<Shop>();

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

		if (shop.isBrowsing)
		{
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
                shop.focusController.DeFocus();

                shopCanvas.gameObject.SetActive(false);
				
                if (isSetup)
				{
                    ResetUI();
				}
                
                shop.isBrowsing = false;
                isSetup = false;
            }
		}
    }

    void SetupUI()
	{
        shopName.text = shopBP.shopName;

        for (int i = 0; i < shopBP.itemBlueprints.Count; i++)
        {
            Debug.Log("DEBUG - AI: Setting up UI for item " + shopBP.itemBlueprints[i].item.ItemName + "...");
            
            GameObject item = (GameObject)Instantiate(sellingItemPrefab, shopList.transform);
            
            item.name = shopBP.itemBlueprints[i].item.ItemName;
            
            GameObject itemIcon = item.gameObject.transform.Find("ItemIcon").gameObject;
            itemIcon.gameObject.transform.Find("Image").GetComponent<Image>().overrideSprite = shopBP.itemBlueprints[i].item.ItemIcon;

            GameObject itemName = item.gameObject.transform.Find("ItemName").gameObject;
            itemName.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.itemBlueprints[i].item.ItemName;

            GameObject itemDesc = item.gameObject.transform.Find("ItemDescription").gameObject;
            itemDesc.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.itemBlueprints[i].item.ItemDescription;

            item.gameObject.transform.Find("PositiveStatPanel").gameObject.SetActive(false);
            item.gameObject.transform.Find("NegativeStatPanel").gameObject.SetActive(false);

            GameObject itemPrice = item.gameObject.transform.Find("BuyButton").gameObject;
            itemPrice.gameObject.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = shopBP.itemBlueprints[i].item.ItemPrice.ToString();

            Debug.Log("DEBUG - AI: Finished UI setup for item " + shopBP.itemBlueprints[i].item.ItemName + "...");
        }

        for (int i = 0; i < shopBP.equipmentBlueprints.Count; i++)
        {
            Debug.Log("DEBUG - AI: Setting up UI for equipment " + shopBP.equipmentBlueprints[i].equipment.ItemName + "...");

            GameObject equipment = (GameObject)Instantiate(sellingItemPrefab, shopList.transform);

            equipment.name = shopBP.equipmentBlueprints[i].equipment.ItemName;

            GameObject equipmentIcon = equipment.gameObject.transform.Find("ItemIcon").gameObject;
            equipmentIcon.gameObject.transform.Find("Image").GetComponent<Image>().overrideSprite = shopBP.equipmentBlueprints[i].equipment.ItemIcon;

            GameObject equipmentName = equipment.gameObject.transform.Find("ItemName").gameObject;
            equipmentName.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.ItemName;

            GameObject equipmentDesc = equipment.gameObject.transform.Find("ItemDescription").gameObject;
            equipmentDesc.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.ItemDescription;

            GameObject equipmentPosStat = equipment.gameObject.transform.Find("PositiveStatPanel").gameObject;
			for (int posStatIndex = 0; posStatIndex < shopBP.equipmentBlueprints[i].equipment.PositiveTraits.Count; posStatIndex++)
			{
                GameObject posStat = (GameObject)Instantiate(positiveStatPrefab, equipmentPosStat.transform);
                
                posStat.name = shopBP.equipmentBlueprints[i].equipment.ItemName;

                GameObject posStatType = posStat.gameObject.transform.Find("StatType").gameObject;
                posStatType.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.PositiveTraits[posStatIndex].traits.ToString();

                GameObject posStatLevel = posStat.gameObject.transform.Find("StatLevel").gameObject;
                posStatLevel.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.PositiveTraits[posStatIndex].traitLevel.ToString();
            }

            GameObject equipmentNegStat = equipment.gameObject.transform.Find("NegativeStatPanel").gameObject;
            for (int negStatIndex = 0; negStatIndex < shopBP.equipmentBlueprints[i].equipment.NegativeTraits.Count; negStatIndex++)
            {
                GameObject negStat = (GameObject)Instantiate(negativeStatPrefab, equipmentNegStat.transform);

                negStat.name = shopBP.equipmentBlueprints[i].equipment.ItemName;

                GameObject negStatType = negStat.gameObject.transform.Find("StatType").gameObject;
                negStatType.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.NegativeTraits[negStatIndex].traits.ToString();

                GameObject negStatLevel = negStat.gameObject.transform.Find("StatLevel").gameObject;
                negStatLevel.gameObject.GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.NegativeTraits[negStatIndex].traitLevel.ToString();
            }

            GameObject equipmentPrice = equipment.gameObject.transform.Find("BuyButton").gameObject;
            equipmentPrice.gameObject.transform.Find("ItemPrice").GetComponent<TextMeshProUGUI>().text = shopBP.equipmentBlueprints[i].equipment.ItemPrice.ToString();

            Debug.Log("DEBUG - AI: Finished UI setup for equipment " + shopBP.equipmentBlueprints[i].equipment.ItemName + "...");

        }
    }

    void ResetUI()
	{
        shopName.text = "Shop Name";

		foreach (Transform item in shopList.transform)
		{
            GameObject.Destroy(item.gameObject);
		}
	}
}
