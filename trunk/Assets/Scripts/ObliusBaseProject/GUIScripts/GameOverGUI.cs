using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;
using Oblius.Assets.AllInOneAdnetworks;

public class GameOverGUI : MonoBehaviour {

    public Text scoreText;
    public Text highScoreText;
    public Text diamondText;
    public Text coinText;
    public Button GetCoinButton;

    public Vector2Int RandomCoinsToAdd = new Vector2Int(10, 20);

    public UnityEvent onGetCoinVideoEntirelyWatched;
	public UnityEvent onGetCoinVideoSkipped;

    // Update is called once per frame
    void Update () {
        scoreText.text = "" + ScoreHandler.instance.score;
        highScoreText.text = "" + ScoreHandler.instance.highScore;
        diamondText.text = "" + ScoreHandler.instance.specialPoints;
	}


    public void OnGetCoinButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();
		AdNetworksManager.instance.ShowRewardedVideo ((bool value) => 
			{
				if(value){
					onGetCoinVideoEntirelyWatched.Invoke();
                    ScoreHandler.instance.increaseSpecialPoints(Random.Range(RandomCoinsToAdd.x, RandomCoinsToAdd.y));

                }
                else
                {
					onGetCoinVideoSkipped.Invoke();
				}
			}
		);
        GetCoinButton.interactable = false;
    }

    public void OnBallShopClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        Deactivate();
        GUIManager.instance.ShowShopGUI();
    }

    public void OnRemoveAdsButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

		Purchaser.instance.BuyNonConsumable(Purchaser.instance.purchaseItems[0].generalProductID);
    }

    public void OnRestorePurchaseButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        Purchaser.instance.RestorePurchases();
    }

    public void OnLeaderboardButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        Leaderboard.instance.showLeaderboard();
    }

    public void OnShareButtonClick()
    {
        SoundsManager.instance.PlayMenuButtonSound();

        ShareManager.instance.share();
    }

    public void OnPlayButtonClick()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }





}
