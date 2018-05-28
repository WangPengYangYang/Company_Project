using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Oblius.Assets.AllInOneAdnetworks;


#if ALLINONE_ADMOB
using GoogleMobileAds.Api;
#endif

namespace Oblius.Assets.AllInOneAdnetworks.Admob
{
	public class AdMob : AdNetwork
	{
		public override string defineSymbolString {
			get {
				return "ALLINONE_ADMOB";
			}
			set {
			}
		}

		public string androidBannerID;
		public string androidInterstitialID;
		public string androidRewardVideoID;

		public string IOSBannerID;
		public string IOSInterstitialID;
		public string IOSRewardVideoID;

		#if ALLINONE_ADMOB
		public AdPosition bannerPosition;

		AdMobBanner banner;
		AdMobInterstitial interstitial;
		AdMobRewardedVideo rewardedVideo;

		AllInOneMainThreadDispatcher mainThreadDispatcher;

		void Awake ()
		{		

			mainThreadDispatcher = GetComponent<AllInOneMainThreadDispatcher> ();

			#if UNITY_ANDROID
			banner = new AdMobBanner (androidBannerID, bannerPosition);
			interstitial = new AdMobInterstitial (androidInterstitialID, mainThreadDispatcher);
			rewardedVideo = new AdMobRewardedVideo (androidRewardVideoID, mainThreadDispatcher);
			#endif

			#if UNITY_IOS
			banner = new AdMobBanner (IOSBannerID, bannerPosition);
			interstitial = new AdMobInterstitial (IOSInterstitialID,mainThreadDispatcher);
			rewardedVideo = new AdMobRewardedVideo (IOSRewardVideoID,mainThreadDispatcher);
			#endif

		}

		public bool RewardedVideoLoaded ()
		{
			return rewardedVideo.videoLoaded;
		}

		public bool InterstitialLoaded ()
		{
			return interstitial.IsLoaded ();
		}

		public void ShowBanner ()
		{
			banner.Show ();
		}

		public void HideBanner ()
		{
			banner.Hide ();
		}

		public void ShowInterstitial (Action onAdClosed)
		{
			interstitial.Show (onAdClosed);
		}

		public bool IsShowingRewardedVideo ()
		{
			return rewardedVideo.isShowing;
		}

		public bool IsShowingInterstitial ()
		{
			return interstitial.isShowing;
		}

		public void ShowRewardedVideo (Action<bool> onRewardVideoWatched)
		{
			rewardedVideo.Show (onRewardVideoWatched);
		}
		
		#endif

	

	}
}
