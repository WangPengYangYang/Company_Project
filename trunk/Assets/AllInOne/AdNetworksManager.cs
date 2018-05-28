using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Oblius.Assets.AllInOneAdnetworks.Admob;
using Oblius.Assets.AllInOneAdnetworks.UnityADS;
using Oblius.Assets.AllInOneAdnetworks.ChartboostAds;

namespace Oblius.Assets.AllInOneAdnetworks
{
	public class AdNetworksManager : MonoBehaviour
	{
		public static AdNetworksManager instance;

		#if ALLINONE_ADMOB
		AdMob adMob;
		#endif

		#if ALLINONE_UNITYADS
		UnityAds unityAds;
		#endif

		#if ALLINONE_CHARTBOOST
		ChartboostAds.ChartboostAds chartboost;
		#endif

		public enum BannerAdNetworks
		{
			none,
			#if ALLINONE_ADMOB
			admob
			#endif
		}

		public enum InterstitialAdNetworks
		{
			none,
			#if ALLINONE_ADMOB
			admob,
			#endif

			#if ALLINONE_CHARTBOOST
			chartboost
			#endif
		}

		public enum RewardedVideoAdNetworks
		{
			none,

			#if ALLINONE_ADMOB
			admob,
			#endif

			#if ALLINONE_UNITYADS
			unityAds,
			#endif

			#if ALLINONE_CHARTBOOST
			chartboost
			#endif

		}

		public BannerAdNetworks bannerAdNetworkToUse;
		public InterstitialAdNetworks interstitialAdNetworkToUse;
		public RewardedVideoAdNetworks rewardedVideoAdNetworkToUse;


		public string advertisingId;

		string adsEnabledPlayerPrefString = "ALLINONE_ADS_ENABLED";

		public bool AdsEnabled {
			get {
				int value = PlayerPrefs.GetInt (adsEnabledPlayerPrefString, 1);
				if (value == 1) {
					return true;
				} else {
					return false;
				}
			}
			set {
				if (value) {
					PlayerPrefs.SetInt (adsEnabledPlayerPrefString, 1);
				} else {
					PlayerPrefs.SetInt (adsEnabledPlayerPrefString, 0);
				}
			}
		}

		void Awake ()
		{
			if (instance != null) {
				Destroy (gameObject);
			} else {
				
				DontDestroyOnLoad (gameObject);
				instance = this;

				#if ALLINONE_ADMOB
				adMob = GetComponent<AdMob> ();
				#endif

				#if ALLINONE_UNITYADS
				unityAds = GetComponent<UnityAds> ();
				#endif

				#if ALLINONE_CHARTBOOST
				chartboost = GetComponent<ChartboostAds.ChartboostAds> ();
				#endif

			}
		}

		// Use this for initialization
		void Start ()
		{
			RequestAndSetAdvertisingID ();
		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}

		public void ShowBanner ()
		{
			if (AdsEnabled) {
				switch (bannerAdNetworkToUse) {
			#if ALLINONE_ADMOB
				case BannerAdNetworks.admob:
					adMob.ShowBanner ();
					break;
			#endif
				default:
					break;   
				}
			} else {
				Debug.LogWarning ("Calling banner but ads are not enabled");
			}
		}

		public void HideBanner ()
		{
			switch (bannerAdNetworkToUse) {
		#if ALLINONE_ADMOB
			case BannerAdNetworks.admob:
				adMob.HideBanner ();
				break;
		#endif
			default:
				Debug.Log ("None");
				break;   
			}
		}


		public void ShowInterstitial (Action onAdClosed)
		{
			#if UNITY_EDITOR
			onAdClosed ();
			return;
			#endif

			if (AdsEnabled) {


				switch (interstitialAdNetworkToUse) {
				#if ALLINONE_ADMOB
				case InterstitialAdNetworks.admob:
					adMob.ShowInterstitial (onAdClosed);
					break;
				#endif
					#if ALLINONE_CHARTBOOST
				case InterstitialAdNetworks.chartboost:
					chartboost.ShowInterstitial (onAdClosed);
					break;
					#endif
				default:
					break;   
				}

			} else {
				Debug.LogWarning ("Calling interstitial but ads are not enabled");
			}
		}

		public bool IsShowingInterstitial ()
		{
			switch (interstitialAdNetworkToUse) {
		#if ALLINONE_ADMOB
			case InterstitialAdNetworks.admob:
				return adMob.IsShowingInterstitial ();
				break;
		#endif
				#if ALLINONE_CHARTBOOST
			case InterstitialAdNetworks.chartboost:
				return chartboost.IsShowingInterstitial ();
				break;
				#endif
			default:
				return false;
				break;   
			}
		}

		public bool InterstitialLoaded ()
		{
			switch (interstitialAdNetworkToUse) {

		#if ALLINONE_ADMOB
			case InterstitialAdNetworks.admob:
				return adMob.InterstitialLoaded ();
				break;
		#endif
				
				#if ALLINONE_CHARTBOOST
			case InterstitialAdNetworks.chartboost:
				return chartboost.InterstitialLoaded ();
				break;
				#endif
				
			default:
				return false;
				break;   
			}
		}

		public void ShowRewardedVideo (Action<bool> onRewardVideoWatched)
		{	
			#if UNITY_EDITOR
			onRewardVideoWatched (true);
			return;
			#endif
		
			switch (rewardedVideoAdNetworkToUse) {

			#if ALLINONE_ADMOB
			case RewardedVideoAdNetworks.admob:
				adMob.ShowRewardedVideo (onRewardVideoWatched);
				break;
			#endif
			
			#if ALLINONE_UNITYADS
			case RewardedVideoAdNetworks.unityAds:
				unityAds.ShowRewardedVideo (onRewardVideoWatched);
				break;
			#endif

				#if ALLINONE_CHARTBOOST
			case RewardedVideoAdNetworks.chartboost:
				chartboost.ShowRewardedVideo (onRewardVideoWatched);
				break;
			#endif

			default:
				Debug.Log ("None");
				break;   
			}
		 
		}

		public void RequestAndSetAdvertisingID ()
		{
			Application.RequestAdvertisingIdentifierAsync (
				(string advertisingId, bool trackingEnabled, string error) => {
					this.advertisingId = advertisingId;
					Debug.Log ("advertisingId " + advertisingId + " " + trackingEnabled + " " + error);
				}
			);
		}

		public bool IsShowingRewardedVideo ()
		{
			switch (rewardedVideoAdNetworkToUse) {
		#if ALLINONE_ADMOB
			case RewardedVideoAdNetworks.admob:
				return adMob.IsShowingRewardedVideo ();
				break;
		#endif
			#if ALLINONE_UNITYADS
			case RewardedVideoAdNetworks.unityAds:
				return unityAds.IsShowingRewardedVideo ();
				break;
				#endif

				#if ALLINONE_CHARTBOOST
			case RewardedVideoAdNetworks.chartboost:
				return chartboost.IsShowingRewardedVideo ();
				break;
				#endif
			default:
				return false;
				break;   
			}
		}


		public bool RewardedVideoLoaded ()
		{
			#if UNITY_EDITOR
			return true;
			#endif

			switch (rewardedVideoAdNetworkToUse) {
		#if ALLINONE_ADMOB
			case RewardedVideoAdNetworks.admob:
				return adMob.RewardedVideoLoaded ();
				break;
		#endif
				#if ALLINONE_UNITYADS
			case RewardedVideoAdNetworks.unityAds:
				return unityAds.RewardedVideoLoaded ();
				break;
				#endif

				#if ALLINONE_CHARTBOOST
			case RewardedVideoAdNetworks.chartboost:
				return chartboost.RewardedVideoLoaded ();
				break;
				#endif
			default:
				return false;
				break;   
			}
		}

		public void DisableAds ()
		{
			AdsEnabled = false;
			HideBanner ();
		}

	}
}