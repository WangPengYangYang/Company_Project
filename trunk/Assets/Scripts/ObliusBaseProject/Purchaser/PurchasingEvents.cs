using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oblius.Assets.AllInOneAdnetworks;
public class PurchasingEvents : MonoBehaviour {	

	public void RemoveAds(){
		AdNetworksManager.instance.DisableAds ();
		Debug.Log ("BUYING REMOVE ADS");
	}

}
