using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Events;

[System.Serializable]
public class Purchasable: System.Object{

	public string generalProductID;
	public string appleProductID;
	public string googleProductID;
	public string localizedPrice;
	public string localizedPriceString;
	public string currency;
	public float valueToAdd;
	public ProductType productType;
	public PurchaseEvent onSuccesfulPurchase;

}
