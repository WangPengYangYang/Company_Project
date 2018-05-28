#if ALLINONE_ADMOB
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

namespace Oblius.Assets.AllInOneAdnetworks.Admob
{
	public class AdMobBanner
	{

		BannerView banner;
		AdPosition position;
		string adUnitID;

		public AdMobBanner (string adUnitID, AdPosition position)
		{

			if (banner != null) {
				banner.Destroy ();
			}

			this.adUnitID = adUnitID;
			this.position = position;

			banner = RequestBanner ();
			banner.Hide ();
		}

		BannerView RequestBanner ()
		{
			// Create a 320x50 banner at the top of the screen.
			BannerView bannerView = new BannerView (adUnitID, AdSize.SmartBanner, position);
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder ().Build ();
			// Load the banner with the request.
			bannerView.LoadAd (request);
			return bannerView;
		}

		public void Show ()
		{
			banner.Show ();
		}

		public void Hide ()
		{
			banner.Hide ();
		}

	}
}
#endif
