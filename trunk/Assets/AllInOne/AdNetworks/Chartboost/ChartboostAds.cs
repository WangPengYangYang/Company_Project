using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if ALLINONE_CHARTBOOST
using ChartboostSDK;
#endif


namespace Oblius.Assets.AllInOneAdnetworks.ChartboostAds
{
	public class ChartboostAds : AdNetwork
	{

		public override string defineSymbolString {
			get {
				return "ALLINONE_CHARTBOOST";
			}
			set {
			}
		}

		public string googleAppID;
		public string googleAppSignature;

		public string iOSAppID;
		public string iOSAppSignature;



		#if ALLINONE_CHARTBOOST

		public CBInterstitial interstitial;
		public CBRewardedVideo rewardedVideo;


		void Awake(){
			if (Application.isPlaying) {
				GameObject chartboostObj = new GameObject ("chartboost");
				chartboostObj.AddComponent<Chartboost> ();
			}
		}

		void Start ()
		{
			#if UNITY_ANDROID
			CBSettings.setAppId (googleAppID, googleAppSignature);
			#endif

			#if UNITY_IOS
		    CBSettings.setAppId (iOSAppID, iOSAppSignature);
			#endif

			interstitial = new CBInterstitial ();
			rewardedVideo = new CBRewardedVideo ();
		}


		public void ShowInterstitial (Action onInterstitialClosed)
		{
			interstitial.Show (onInterstitialClosed);
		}

		public bool IsShowingInterstitial ()
		{
			return interstitial.isShowing;
		}

		public bool InterstitialLoaded ()
		{
			return interstitial.Loaded ();
		}

		public void ShowRewardedVideo (Action<bool> onRewardedVideoWatched)
		{
			rewardedVideo.Show (onRewardedVideoWatched);
		}

		public bool IsShowingRewardedVideo ()
		{
			return rewardedVideo.isShowing;
		}

		public bool RewardedVideoLoaded ()
		{
			return rewardedVideo.Loaded ();
		}



		#endif

	}
}