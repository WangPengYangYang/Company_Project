using UnityEngine;
using System.Collections;
using Oblius.Assets.AllInOneAdnetworks;

public class ObliusGameManager : MonoBehaviour
{

	public static ObliusGameManager instance;

	public enum GameState
	{
		menu,
		game,
		gameover,
		shop

	}

	public GameState gameState;
	public bool oneMoreChanceUsed = false;

	void Awake ()
	{
		instance = this;
	}

    void Start ()
	{
		Application.targetFrameRate = 60;
	}

	

	public IEnumerator GameOverCoroutine (float delay)
	{
		gameState = GameState.gameover;
		yield return new WaitForSeconds (delay);
		AdNetworksManager.instance.HideBanner ();

		Leaderboard.instance.reportScore (ScoreHandler.instance.score);
		GUIManager.instance.ShowGameOverGUI ();
		InGameGUI.instance.gameObject.SetActive (false);
		AdNetworksManager.instance.ShowInterstitial (()=> Debug.Log("Interstitial Closed"));
	}


	public void GameOver (float delay)
	{
		StartCoroutine (GameOverCoroutine (delay));
	}

	public void StartGame ()
	{
		ResetGame ();
		ScoreHandler.instance.incrementNumberOfGames ();
		GUIManager.instance.ShowInGameGUI ();
		AdNetworksManager.instance.ShowBanner ();
		gameState = GameState.game;
        GameEventsCollection.instance.StartGame();
	}

	public void ResetGame (bool resetScore = true, bool resetOneMoreChance = true)
	{
		if (resetOneMoreChance) {
			oneMoreChanceUsed = false;
		}

		if (resetScore) {
			ScoreHandler.instance.reset ();
		}
	}


}
