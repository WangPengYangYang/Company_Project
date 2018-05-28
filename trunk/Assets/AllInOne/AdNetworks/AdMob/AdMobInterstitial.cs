#if ALLINONE_ADMOB
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using System;

namespace Oblius.Assets.AllInOneAdnetworks.Admob
{
	public class AdMobInterstitial
	{

		string adUnitID;
		InterstitialAd interstitial;

		Action onAdClosed;
		AllInOneMainThreadDispatcher mainThreadDispatcher;

		public bool isShowing;

		public AdMobInterstitial (string adUnitID, AllInOneMainThreadDispatcher mainThreadDispatcher)
		{
			this.adUnitID = adUnitID;
			this.mainThreadDispatcher = mainThreadDispatcher;
			RequestNewInterstitial ();
		}

		void RequestNewInterstitial ()
		{
			InterstitialAd interstitial = new InterstitialAd (adUnitID);
			AdRequest request = new AdRequest.Builder ().Build ();
			interstitial.LoadAd (request);
			InitializeEvents (interstitial);
			this.interstitial = interstitial;
		}

		public void InitializeEvents (InterstitialAd interstitial)
		{
			interstitial.OnAdClosed += HandleInterstitialClosed; 
		}

		public void Show (Action onAdClosed)
		{
			isShowing = true;
			interstitial.Show ();
			this.onAdClosed = onAdClosed;
			RequestNewInterstitial ();
		}

		public void HandleInterstitialClosed (object sender, EventArgs args)
		{
			mainThreadDispatcher.Enqueue (() => {
				onAdClosed ();
				isShowing = false;
			});
		}

		public bool IsLoaded ()
		{
			return interstitial.IsLoaded ();
		}

	}
}
#endif