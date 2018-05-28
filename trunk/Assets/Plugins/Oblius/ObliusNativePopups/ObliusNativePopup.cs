using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObliusNativePopup
{

	MNPopup popUp;

	public ObliusNativePopup (string title, string dialogMessage)
	{
		popUp = new MNPopup (title, dialogMessage);
	}

	public void AddAction (string title, Action callback)
	{
		popUp.AddAction (title, (() => callback ()));
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
