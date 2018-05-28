#if ALLINONE_CHARTBOOST
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ChartboostSDK;
using System;



public class CBRewardedVideo
{

	public CBLocation location;
	Action<bool> onRewardVideoWatched;

	public bool isShowing;

	public CBRewardedVideo ()
	{
		location = CBLocation.Default;

		Chartboost.didCompleteRewardedVideo += ((CBLocation arg1, int arg2) => {
			onRewardVideoWatched (true);
			isShowing = false;
		});


		Chartboost.didDismissRewardedVideo += ((CBLocation arg1) => {
			onRewardVideoWatched (false);
			isShowing = false;
		});

		Chartboost.didCloseRewardedVideo += (CBLocation obj) => isShowing = false;
		Chartboost.didFailToLoadRewardedVideo += (CBLocation arg1, CBImpressionError arg2) => CacheVideo ();

		CacheVideo ();
	}

	public void CacheVideo ()
	{
		Chartboost.cacheRewardedVideo (location);
	}

	public void Show (Action<bool> onRewardVideoWatched)
	{
		this.onRewardVideoWatched = onRewardVideoWatched;
		isShowing = true;
		Chartboost.showRewardedVideo (location);
		CacheVideo ();
	}

	public bool Loaded ()
	{
		return Chartboost.hasRewardedVideo (location);
	}

}


#endif
