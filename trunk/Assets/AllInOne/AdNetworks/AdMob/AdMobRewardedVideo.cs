#if ALLINONE_ADMOB
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace Oblius.Assets.AllInOneAdnetworks.Admob
{
	public class AdMobRewardedVideo
	{

		RewardBasedVideoAd rewardedVideo;
		string adUnitId;

		public bool videoLoaded;
		public bool isShowing;
		public bool entireVideoWatched;

		public Action<bool> onRewardVideoWatched;

		AllInOneMainThreadDispatcher mainThreadDispatcher;

		public AdMobRewardedVideo (string adUnitId, AllInOneMainThreadDispatcher mainThreadDispatcher)
		{
			rewardedVideo = RewardBasedVideoAd.Instance;
			this.adUnitId = adUnitId;
			this.mainThreadDispatcher = mainThreadDispatcher;
			InitializeEvents ();
			RequestRewardedVideo ();
		}

		void InitializeEvents ()
		{
			// Called when an ad request has successfully loaded.
			rewardedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
			// Called when an ad request failed to load.
			rewardedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
			// Called when an ad is shown.
			rewardedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
			// Called when the ad starts to play.
			rewardedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
			// Called when the user should be rewarded for watching a video.
			rewardedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
			// Called when the ad is closed.
			rewardedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
			// Called when the ad click caused the user to leave the application.
			rewardedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;
		}

		private void RequestRewardedVideo ()
		{
			videoLoaded = false;
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder ().Build ();
			// Load the rewarded video ad with the request.
			rewardedVideo.LoadAd (request, adUnitId);
		}

		public void Show (Action<bool> onRewardVideoWatched)
		{   
			this.onRewardVideoWatched = onRewardVideoWatched;
			isShowing = true;
			rewardedVideo.Show ();
		}

		public void HandleRewardBasedVideoLoaded (object sender, EventArgs args)
		{
			videoLoaded = true;
			MonoBehaviour.print ("HandleRewardBasedVideoLoaded event received");
		}

		public void HandleRewardBasedVideoFailedToLoad (object sender, AdFailedToLoadEventArgs args)
		{
			MonoBehaviour.print (
				"HandleRewardBasedVideoFailedToLoad event received with message: "
				+ args.Message);

			RequestRewardedVideo ();
		}

		public void HandleRewardBasedVideoOpened (object sender, EventArgs args)
		{
			entireVideoWatched = false;
			Debug.Log ("SHOWING REWARDED VIDEO");
			MonoBehaviour.print ("HandleRewardBasedVideoOpened event received");
		}

		public void HandleRewardBasedVideoStarted (object sender, EventArgs args)
		{
			entireVideoWatched = false;
			Debug.Log ("SHOWING REWARDED VIDEO");
			MonoBehaviour.print ("HandleRewardBasedVideoStarted event received");
		}

		public void HandleRewardBasedVideoClosed (object sender, EventArgs args)
		{

			mainThreadDispatcher.Enqueue (() => {
				if (entireVideoWatched) {
					onRewardVideoWatched (true);
				} else {
					onRewardVideoWatched (false);
				}
				isShowing = false;
			});

			RequestRewardedVideo ();

			Debug.Log ("STOP SHOWING REWARDED VIDEO");
			MonoBehaviour.print ("HandleRewardBasedVideoClosed event received");
		}

		public void HandleRewardBasedVideoRewarded (object sender, Reward args)
		{
			entireVideoWatched = true;
			Debug.Log ("STOP SHOWING REWARDED VIDEO");

			string type = args.Type;
			double amount = args.Amount;
			MonoBehaviour.print (
				"HandleRewardBasedVideoRewarded event received for "
				+ amount.ToString () + " " + type);
		}

		public void HandleRewardBasedVideoLeftApplication (object sender, EventArgs args)
		{
			MonoBehaviour.print ("HandleRewardBasedVideoLeftApplication event received");
		}

	}
}
#endif