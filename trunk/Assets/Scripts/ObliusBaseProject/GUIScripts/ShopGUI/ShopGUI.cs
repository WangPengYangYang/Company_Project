using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShopGUI : MonoBehaviour {

    public static ShopGUI instance;

    public Text selectedItemText;
    public Text selectedItemPriceText;
    public Text specialPointsText;

    public GameObject unlockButton;
    public GameObject playButton;

    public Image bottomSpecialPointImage;

    void Awake()
    {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        unlockButton.SetActive(false);
        playButton.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

        selectedItemText.text = ShopHandler.instance.selectedItem.name;
        selectedItemPriceText.text = "" + ShopHandler.instance.selectedItem.price;
        specialPointsText.text = ScoreHandler.instance.specialPoints.ToString();


        if (ShopHandler.instance.selectedItem.unlocked)
        {

            if (bottomSpecialPointImage.enabled)
            {
                bottomSpecialPointImage.enabled = false;
                selectedItemPriceText.enabled = false;
            }
           

            if (unlockButton.activeSelf)
            {
                unlockButton.SetActive(false);
            }

            if (!playButton.activeSelf)
            {
                playButton.SetActive(true);
            }
        }
        else if(!ShopHandler.instance.selectedItem.unlocked)
        {

            if (!bottomSpecialPointImage.enabled)
            {
                bottomSpecialPointImage.enabled = true;
                selectedItemPriceText.enabled = true;
            }



            if (playButton.activeSelf)
            {
                playButton.SetActive(false);
            }
            if (!unlockButton.activeSelf)
            {
                unlockButton.SetActive(true);
            }
        }

	}

    public void OnBackButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        GUIManager.instance.ShowMainMenuGUI();
        ShopHandler.instance.Deactivate();
    }

    public void onUnlockButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        if (ShopHandler.instance.selectedItem.price <= ScoreHandler.instance.specialPoints)
        {
            ShopHandler.instance.selectedItem.Buy();
        }
    }

    public void onPlayButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        GUIManager.instance.ShowMainMenuGUI();
        ShopHandler.instance.Deactivate();
        ShopHandler.instance.SetShopItemToUse(ShopHandler.instance.selectedItem);
    }

}
