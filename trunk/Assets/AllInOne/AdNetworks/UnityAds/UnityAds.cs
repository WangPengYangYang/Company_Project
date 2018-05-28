using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if ALLINONE_UNITYADS
using Oblius.Assets.AllInOneAdnetworks.UnityADS;
using UnityEngine.Advertisements;
#endif


namespace Oblius.Assets.AllInOneAdnetworks.UnityADS
{
	public class UnityAds : AdNetwork
	{


		public override string defineSymbolString {
			get {
				return "ALLINONE_UNITYADS";
			}
			set {
			}
		}


		public string rewardedVideoID;

		#if ALLINONE_UNITYADS

		RewardedVideo rewardedVideo;

		void Awake(){
			rewardedVideo = new RewardedVideo (rewardedVideoID);
		}

		public void ShowRewardedVideo(Action<bool> onRewardVideoWatched){
			rewardedVideo.Show (onRewardVideoWatched);
		}
			

		public bool RewardedVideoLoaded(){
			return rewardedVideo.IsLoaded ();
		}

		public bool IsShowingRewardedVideo(){
			return rewardedVideo.isShowing;
		}

		#endif

	
	}
}
