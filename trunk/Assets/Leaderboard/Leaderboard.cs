using UnityEngine;
using System.Collections;

#if UNITY_ANDROID
using GooglePlayGames;
#endif
using UnityEngine.SocialPlatforms;

#if UNITY_IOS
using UnityEngine.SocialPlatforms.GameCenter;
#endif


public class Leaderboard : MonoBehaviour
{

	public static Leaderboard instance;

	public string googlePlayLeaderboardID;
	public string gameCenterLeaderboardID;

	string leaderboardIdToUse;
	protected bool isLogged;

	void Awake ()
	{
		if (instance != null) {
			Destroy (gameObject);
			return;
		}

		DontDestroyOnLoad (gameObject);

		instance = this;

		initialize ();
		signIn ();
	}

	// Use this for initialization
	void Start ()
	{
     
	}

	// Update is called once per frame
	void Update ()
	{

	}

	void signIn ()
	{
		// authenticate user:
		Social.localUser.Authenticate ((bool success) => {
			if (success) {
				isLogged = true;
			} else {
				isLogged = false;
			}
		});
	}

	public void reportScore (long score)
	{
		try {
			Social.ReportScore (score, leaderboardIdToUse, (bool success) => {

			});
		} catch {

		}
	}

	public void showLeaderboard ()
	{
#if UNITY_ANDROID
		PlayGamesPlatform.Instance.ShowLeaderboardUI (leaderboardIdToUse);
#endif

#if UNITY_IOS
 GameCenterPlatform.ShowLeaderboardUI(leaderboardIdToUse,0);
#endif
	}

	void initialize ()
	{
#if UNITY_ANDROID
		PlayGamesPlatform.Activate ();
		leaderboardIdToUse = googlePlayLeaderboardID;
#endif

#if UNITY_IOS
 leaderboardIdToUse = gameCenterLeaderboardID ;
#endif
	}

}

