#if ALLINONE_UNITYADS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;


namespace Oblius.Assets.AllInOneAdnetworks.UnityADS
{
	public class RewardedVideo
	{

		public string id;
		Action<bool> onRewardVideoWatched;

		public bool isShowing;

		public RewardedVideo (string id)
		{
			this.id = id;
		}

		public void Show (Action<bool> onRewardVideoWatched)
		{
			this.onRewardVideoWatched = onRewardVideoWatched;
			ShowOptions options = new ShowOptions ();
			options.resultCallback = HandleShowResult;
			Advertisement.Show (id,options);
			isShowing = true;
		}

		void HandleShowResult (ShowResult result)
		{
			isShowing = false;

			if (result == ShowResult.Finished) {
				onRewardVideoWatched (true);
			} else if (result == ShowResult.Skipped) {
				onRewardVideoWatched (false);
			} else if (result == ShowResult.Failed) {
				onRewardVideoWatched (false);
			}

		}

		public bool IsLoaded(){
			return Advertisement.IsReady(id);
		}


	}
}
#endif