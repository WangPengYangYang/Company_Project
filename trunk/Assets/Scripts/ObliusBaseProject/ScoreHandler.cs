using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;

public class ScoreHandler : MonoBehaviour
{

	public int score;
	public int secondaryScore;
	public int lifetimeScore;
	public int highScore;
	public int specialPoints;
	public int numberOfGames;


	string highScorePlayerPrefsName = "HIGHSCORE";
	string specialPointsPlayerPrefsName = "SPECIALPOINTS";
	string numberOfGamesPlayerPrefsName = "NUMBEROFGAMES";
	string lifeTimeScorePlayerPrefsName = "LIFETIMESCORE";

	public bool highscoreScoredInThisSession;

	public delegate void ScoredHighscore ();

	public UnityIntEvent OnScoredHighscore;

	public static ScoreHandler instance;

	void Awake ()
	{
		instance = this;        
		loadHighScoreFromPrefs ();
		loadLifeTimeScoreFromPrefs ();
		loadSpecialPointsFromPlayerPrefs ();
		loadNumberOfGamesFromPlayerPrefs ();
	}

	// Use this for initialization
	void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
	{

	}


	public void increaseSpecialPoints (int valueToAdd)
	{
		specialPoints += valueToAdd;
		saveSpecialPointsToPlayerPrefs ();
	}

	public void increaseScore (int valueToAdd)
	{
		score += valueToAdd;
		lifetimeScore += valueToAdd;

		saveLifeTimeScoreToPrefs ();

		if (score > highScore) {
			
			if (!highscoreScoredInThisSession) {
				if (OnScoredHighscore != null) {
					OnScoredHighscore.Invoke (score);
				}
			}

			highscoreScoredInThisSession = true;
			highScore = score;
			saveHighScoreToPrefs ();
		}

	}


	public void increaseSecondaryScore (int valueToAdd)
	{
		secondaryScore += valueToAdd;
	}

	public void incrementNumberOfGames ()
	{
		numberOfGames++;
		PlayerPrefs.SetInt (numberOfGamesPlayerPrefsName, numberOfGames);
	}

	public void loadNumberOfGamesFromPlayerPrefs ()
	{
		numberOfGames = PlayerPrefs.GetInt (numberOfGamesPlayerPrefsName, 0);
	}

	public void removeSpecialPoints (int specialPointsToRemove)
	{
		specialPoints -= specialPointsToRemove;
		saveSpecialPointsToPlayerPrefs ();
	}

	void saveSpecialPointsToPlayerPrefs ()
	{
		PlayerPrefs.SetInt (specialPointsPlayerPrefsName, specialPoints);
	}

	void loadSpecialPointsFromPlayerPrefs ()
	{
		specialPoints = PlayerPrefs.GetInt (specialPointsPlayerPrefsName, 0);
	}


	void saveHighScoreToPrefs ()
	{
		PlayerPrefs.SetInt (highScorePlayerPrefsName, highScore);
	}

	void saveLifeTimeScoreToPrefs ()
	{
		PlayerPrefs.SetInt (lifeTimeScorePlayerPrefsName, lifetimeScore);
	}

	void loadLifeTimeScoreFromPrefs ()
	{
		lifetimeScore = PlayerPrefs.GetInt (lifeTimeScorePlayerPrefsName, 0);
	}

	void loadHighScoreFromPrefs ()
	{
		highScore = PlayerPrefs.GetInt (highScorePlayerPrefsName, 0);
	}

	public void reset ()
	{
		score = 0;
		highscoreScoredInThisSession = false;
		secondaryScore = 0;
	}

	[System.Serializable]
	public class UnityIntEvent : UnityEvent<int>
	{
		
	}
}
