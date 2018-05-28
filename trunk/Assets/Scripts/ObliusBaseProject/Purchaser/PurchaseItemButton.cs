using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseItemButton : MonoBehaviour {

	public int productToPurchaseIndex;


	public void OnClick(){

		Purchasable productToPurchase = Purchaser.instance.purchaseItems [productToPurchaseIndex];

		if (productToPurchase.productType == UnityEngine.Purchasing.ProductType.NonConsumable) {
			Purchaser.instance.BuyNonConsumable (productToPurchase.generalProductID);
		}

		if (productToPurchase.productType == UnityEngine.Purchasing.ProductType.Consumable) {
			Purchaser.instance.BuyConsumable (productToPurchase.generalProductID);
		}			
	}

}
