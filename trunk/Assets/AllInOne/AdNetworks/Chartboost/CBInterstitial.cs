#if ALLINONE_CHARTBOOST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;
using System;



namespace Oblius.Assets.AllInOneAdnetworks.ChartboostAds
{
	public class CBInterstitial
	{

		CBLocation location;
		Action onInterstitialClosed;

		public bool isShowing;

		public CBInterstitial ()
		{
			location = CBLocation.Default;
			Chartboost.didCloseInterstitial += HandleInterstitialClosed;
			Chartboost.didFailToLoadInterstitial += (CBLocation arg1, CBImpressionError arg2) => CacheInterstitial();
			CacheInterstitial ();
		}

		void CacheInterstitial ()
		{
			Chartboost.cacheInterstitial (location);
		}

		public void Show (Action onInterstitialClosed)
		{
			this.onInterstitialClosed = onInterstitialClosed;
			Chartboost.showInterstitial (location);
			isShowing = true;
			CacheInterstitial ();
		}


		void HandleInterstitialClosed (CBLocation location)
		{
			isShowing = false;
			onInterstitialClosed ();
		}

		public bool Loaded(){
			return Chartboost.hasInterstitial(location);
		}


	}
}
#endif
