using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObliusNativeRatePopup
{

	MNRateUsPopup popUp;

	public ObliusNativeRatePopup (string title, string message, string rateUsButtonText, string declineButtonText, string laterButtonText)
	{
		popUp = new MNRateUsPopup (title, message, rateUsButtonText, declineButtonText, laterButtonText);
		popUp.Show ();
	}

	public void SetAppleId (string appleID)
	{
		popUp.SetAppleId (appleID);
	}

	public void SetAndroidAppUrl (string androidAppUrl)
	{
		popUp.SetAndroidAppUrl (androidAppUrl);
	}

	public void AddDeclineListener (Action callback)
	{
		popUp.AddDeclineListener (() => callback ());
	}

	public void AddRemindListener (Action callback)
	{
		popUp.AddRemindListener (() => callback ());
	}

	public void AddRateUsListener (Action callback)
	{
		popUp.AddRateUsListener (() => callback ());
	}

	public void AddDismissListener (Action callback)
	{
		popUp.AddDismissListener (() => callback ());
	}

	public void Show ()
	{
		popUp.Show ();
	}


}
