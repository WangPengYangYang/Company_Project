using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopHandler : MonoBehaviour
{

    public static ShopHandler instance;
    Vector3 centerScreen;

    List<ShopItem> shopItems;
    public ShopItem selectedItem;

    public ShopItem shopItemToUse;
    public ShopItem defaulShopItem;

    string boughtShopItemsPlayerPrefString = "BOUGHTITEMS";
    string shopItemToUsePlayerPrefString = "SHOPITEMTOUSE";

    void Awake()
    {
        instance = this;

    }

    // Use this for initialization
    void Start()
    {
        GetShopItemToUseFromPlayerPrefs();
        shopItems = GetShopItems();

        Input.multiTouchEnabled = false;
        Camera currentCamera = ShopCamera.instance.currentCamera;
        centerScreen = currentCamera.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, currentCamera.nearClipPlane));
        centerScreen.x = currentCamera.transform.localPosition.x + centerScreen.x;

        SpawnShopItems(shopItems);
        selectedItem = shopItems[0];
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            resizeAllItems();
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 newItemContainerPosition = ShopItemContainer.instance.transform.position;
            newItemContainerPosition.x += ScrollHandler.instance.positionVariation / 50;
            ShopItemContainer.instance.transform.position = newItemContainerPosition;
        }
        else {

            ShopItem nearestItem = getNearestItem();
            if (nearestItem == shopItems[0] || nearestItem == shopItems[shopItems.Count - 1]) { ScrollHandler.instance.scrollPower = 0; }

            Vector3 newItemContainerPosition = ShopItemContainer.instance.transform.position;
            newItemContainerPosition.x += ScrollHandler.instance.scrollPower / 50;
            ShopItemContainer.instance.transform.position = newItemContainerPosition;

            if (ScrollHandler.instance.scrollPower == 0)
            {
                selectedItem = nearestItem;
                moveNearestItemToCenter();
                enlargeSelectedItem();
            }
        }

    }



    void SpawnShopItems(List<ShopItem> shopItems)
    {

        Vector3 target = ShopItemContainer.instance.transform.position;

        float distanceOffset = 1;

        for (int i = 0; i < shopItems.Count; i++)
        {

            if (i != 0)
            {

                float lastBoundX = shopItems[i - 1].sprite.bounds.size.x;
                float lastBound = (float)lastBoundX / 2;
                target.x += lastBound;

                float itemBoundX = shopItems[i].sprite.bounds.size.x;
                float itemBound = (float)itemBoundX / 2;
                target.x += itemBound;

                target.x += distanceOffset;
            }

            shopItems[i].transform.position = target;
        }
    }

    List<ShopItem> GetShopItems()
    {
        ShopItem[] unorderedShopItems = FindObjectsOfType<ShopItem>();
        List<ShopItem> orderedShopItems = new List<ShopItem>();

        foreach (ShopItem item in unorderedShopItems)
        {
            orderedShopItems.Add(item);
        }

        orderedShopItems.Sort(delegate (ShopItem go1, ShopItem go2)
        {
            return go1.transform.GetSiblingIndex().CompareTo(go2.transform.GetSiblingIndex());
        });

        return orderedShopItems;
    }


    void resizeAllItems()
    {
        foreach (ShopItem Item in shopItems)
        {
            Item.transform.localScale = Item.originalScale;
        }
    }


    public void moveNearestItemToCenter()
    {
        ShopItem ItemToMove = getNearestItem();
        float distanceBetweenItemToMoveAndCenter = centerScreen.x - ItemToMove.transform.position.x;

        foreach (ShopItem Item in shopItems)
        {
            Vector3 targetPosition = Item.transform.position;
            targetPosition.x += distanceBetweenItemToMoveAndCenter;
            Item.transform.position = Vector3.MoveTowards(Item.transform.position, targetPosition, Mathf.Abs(distanceBetweenItemToMoveAndCenter) * 10 * Time.deltaTime);
        }
    }



    public ShopItem getNearestItem()
    {
        ShopItem nearestItem = null;
        float lastFoundDistance = 100000;

        foreach (ShopItem Item in shopItems)
        {
            float distance = Vector3.Distance(Item.transform.position, centerScreen);
            if (distance < lastFoundDistance)
            {
                nearestItem = Item;
                lastFoundDistance = distance;
            }
        }
        return nearestItem;
    }

    void enlargeSelectedItem()
    {
        if (selectedItem.transform.localScale == selectedItem.originalScale)
        {
            selectedItem.transform.localScale = selectedItem.transform.localScale * 3f;
        }
    }

    public List<string> GetBoughtItemsNames()
    {
        List<string> listToReturn = new List<string>();
        string boughtItemsRawString = PlayerPrefs.GetString(boughtShopItemsPlayerPrefString);

        foreach (string itemName in boughtItemsRawString.Split(','))
        {
            listToReturn.Add(itemName);
        }

        return listToReturn;
    }

    public void UnlockShopItem(ShopItem item)
    {

        string boughtItemsRawString = PlayerPrefs.GetString(boughtShopItemsPlayerPrefString);

        if (!boughtItemsRawString.Contains(item.name))
        {
            boughtItemsRawString += item.name + ",";
            PlayerPrefs.SetString(boughtShopItemsPlayerPrefString, boughtItemsRawString);
        }

        ScoreHandler.instance.removeSpecialPoints(item.price);
    }

    public void SetShopItemToUse(ShopItem shopItem)
    {
        shopItemToUse = shopItem;
        PlayerPrefs.SetString(shopItemToUsePlayerPrefString, shopItem.name);
    }

    void GetShopItemToUseFromPlayerPrefs()
    {
        string defaulShopItemName = ShopItemContainer.instance.transform.GetChild(0).name;
        string shopItemToUseName = PlayerPrefs.GetString(shopItemToUsePlayerPrefString, defaulShopItemName);

        try
        {
            shopItemToUse = GameObject.Find(shopItemToUseName).GetComponent<ShopItem>();
        }
        catch
        {
            if (shopItemToUse == null)
            {
                shopItemToUse = GameObject.Find(defaulShopItemName).GetComponent<ShopItem>();
            }
        }




    }

    public void Activate()
    {
        GUIManager.instance.shopGUI.gameObject.SetActive(true);
        ShopCamera.instance.CameraActive(true);
        ScrollHandler.instance.enabled = (true);
    }

    public void Deactivate()
    {
        ShopCamera.instance.CameraActive(false);
        ScrollHandler.instance.enabled = (false);
        GUIManager.instance.shopGUI.gameObject.SetActive(false);
    }

}
