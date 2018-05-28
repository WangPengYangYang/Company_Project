using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using System;
using Oblius.Assets.AllInOneAdnetworks;

public class FBLikeToUnlock : MonoBehaviour
{

	public string facebookURL;
	string playerPrefsString = "FACEBOOKLIKED";
	string likedDatePrefsString = "FACEBOOKLIKEDDATE";

	public UnityEvent CustomOnLike;
	public static FBLikeToUnlock instance;

	public double currentTime;

	void Awake ()
	{
		if (instance != null) {
			Destroy (gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		StartCoroutine (CheckIfHaveToEnableOrDisableAds ());
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}


	public IEnumerator CheckIfHaveToEnableOrDisableAds ()
	{
		while (true) {

			int timeDifference = GetCurrentTime () - GetPageLikedDate ();

			if (timeDifference > 3600) {
				AdNetworksManager.instance.gameObject.SetActive (true);
			} else {
				AdNetworksManager.instance.gameObject.SetActive (false);
			}

			yield return new WaitForSeconds (2);
		}
	}



	public void LikePage ()
	{
		StartCoroutine (LikePageRoutine ());
	}

	public IEnumerator LikePageRoutine ()
	{
		if (!Liked ()) {
			Application.OpenURL (facebookURL);
			yield return new WaitForSeconds (2);
			Util.ShowPopUp("Thanks!", "Thanks for liking our page! Enjoy the game without ads for one hour!");
		}

		SetPageLiked ();
	}

	void SetPageLiked ()
	{
		PlayerPrefs.SetInt (playerPrefsString, 1);
		PlayerPrefs.SetInt (likedDatePrefsString, GetCurrentTime ());
	}

	public bool Liked ()
	{
		if (PlayerPrefs.GetInt (playerPrefsString) == 0) {
			return false;
		} else {

			return true;
		}
	}

	public int GetPageLikedDate ()
	{
		return PlayerPrefs.GetInt (likedDatePrefsString);
	}

	public int GetCurrentTime ()
	{
		return (int)(DateTime.UtcNow.Subtract (new DateTime (1970, 1, 1))).TotalSeconds;
	}

}
