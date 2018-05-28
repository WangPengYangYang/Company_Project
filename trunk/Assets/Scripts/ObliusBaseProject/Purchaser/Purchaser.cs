using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;



// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
public class Purchaser : MonoBehaviour, IStoreListener
{


	public static Purchaser instance;

	private static IStoreController m_StoreController;
	// Reference to the Purchasing system.
	private static IExtensionProvider m_StoreExtensionProvider;
	// Reference to store-specific Purchasing subsystems.

	// Product identifiers for all products capable of being purchased: "convenience" general identifiers for use with Purchasing, and their store-specific identifier counterparts
	// for use with and outside of Unity Purchasing. Define store-specific identifiers also on each platform's publisher dashboard (iTunes Connect, Google Play Developer Console, etc.)

	public Purchasable[] purchaseItems;
	public GameObject waitingResponseOverlay;

                                                                                                    
	void Awake ()
	{
		if (instance != null) {
			Destroy (gameObject);
			return;
		}

		instance = this;
		DontDestroyOnLoad (gameObject);
	}

	void Start ()
	{
		// If we haven't set up the Unity Purchasing reference
		if (m_StoreController == null) {
			// Begin to configure our connection to Purchasing
			InitializePurchasing ();
		}
	}

	public void InitializePurchasing ()
	{
		// If we have already connected to Purchasing ...
		if (IsInitialized ()) { 
			// ... we are done here.
			return;
		}

		// Create a builder, first passing in a suite of Unity provided stores.
		var builder = ConfigurationBuilder.Instance (StandardPurchasingModule.Instance ());

		foreach (Purchasable purchaseItem in purchaseItems) {
			builder.AddProduct(purchaseItem.generalProductID,purchaseItem.productType, new IDs () { {
					purchaseItem.appleProductID,
					AppleAppStore.Name
				}, {
					purchaseItem.googleProductID,
					GooglePlay.Name
				},
			});
		}
		// And finish adding the subscription product.
		//builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs() { { kProductNameAppleSubscription, AppleAppStore.Name }, { kProductNameGooglePlaySubscription, GooglePlay.Name }, });// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
		UnityPurchasing.Initialize (this, builder);
	}


	private bool IsInitialized ()
	{
		// Only say we are initialized if both the Purchasing references are set.
		return m_StoreController != null && m_StoreExtensionProvider != null;
	}


	public void BuyConsumable (string productID)
	{
		// Buy the consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID (productID);
	}


	public void BuyNonConsumable (string productID)
	{
		// Buy the non-consumable product using its general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
		BuyProductID (productID);
	}

	/*
        public void BuySubscription()
        {
            // Buy the subscription product using its the general identifier. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
            BuyProductID(kProductIDSubscription);
        }
        */

	void BuyProductID (string productId)
	{
		// If the stores throw an unexpected exception, use try..catch to protect my logic here.
		try {
			// If Purchasing has been initialized ...
			if (IsInitialized ()) {
				// ... look up the Product reference with the general product identifier and the Purchasing system's products collection.
				Product product = m_StoreController.products.WithID (productId);

				// If the look up found a product for this device's store and that product is ready to be sold ... 
				if (product != null && product.availableToPurchase) {					
					ShowWaitingResponsePanel(true);
					Debug.Log (string.Format ("Purchasing product asychronously: '{0}'", product.definition.id));// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed asynchronously.
					m_StoreController.InitiatePurchase (product);
				}
                // Otherwise ...
                else {
					// ... report the product look-up failure situation  
					ShowWaitingResponsePanel(false);
					Util.ShowPopUp ("Error", "Please check your internet connection and retry later");
					Debug.Log ("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
				}
			}
            // Otherwise ...
            else {
				// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or retrying initiailization.
				ShowWaitingResponsePanel(false);
				Util.ShowPopUp ("Error", "Please check your internet connection and retry later");
				Debug.Log ("BuyProductID FAIL. Not initialized.");
			}
		}
        // Complete the unexpected exception handling ...
        catch (Exception e) {
			// ... by reporting any unexpected exception for later diagnosis.
			ShowWaitingResponsePanel(false);
			Util.ShowPopUp ("Error", "An unexpected error happened, we're working to fix it");
			Debug.Log ("BuyProductID: FAIL. Exception during purchase. " + e);
		}
	}


	// Restore purchases previously made by this customer. Some platforms automatically restore purchases. Apple currently requires explicit purchase restoration for IAP.
	public void RestorePurchases ()
	{
		// If Purchasing has not yet been set up ...
		if (!IsInitialized ()) {
			// ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
			Debug.Log ("RestorePurchases FAIL. Not initialized.");
			return;
		}

		// If we are running on an Apple device ... 
		if (Application.platform == RuntimePlatform.IPhonePlayer ||
		    Application.platform == RuntimePlatform.OSXPlayer) {
			// ... begin restoring purchases
			Debug.Log ("RestorePurchases started ...");

			// Fetch the Apple store-specific subsystem.
			var apple = m_StoreExtensionProvider.GetExtension<IAppleExtensions> ();
			// Begin the asynchronous process of restoring purchases. Expect a confirmation response in the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
			apple.RestoreTransactions ((result) => {
				// The first phase of restoration. If no more responses are received on ProcessPurchase then no purchases are available to be restored.
				Debug.Log ("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
			});
		}
        // Otherwise ...
        else {
			// We are not running on an Apple device. No work is necessary to restore purchases.
			Debug.Log ("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
		}
	}


	//
	// --- IStoreListener
	//

	public void OnInitialized (IStoreController controller, IExtensionProvider extensions)
	{
		// Purchasing has succeeded initializing. Collect our Purchasing references.
		Debug.Log ("OnInitialized: PASS");

		// Overall Purchasing system, configured with products for this application.
		m_StoreController = controller;
		// Store specific subsystem, for accessing device-specific store features.
		m_StoreExtensionProvider = extensions;


		//setting correct prices
		foreach (Purchasable purcahseItem in purchaseItems) {
			purcahseItem.localizedPrice = m_StoreController.products.WithID(purcahseItem.generalProductID).metadata.localizedPrice.ToString();
			purcahseItem.localizedPriceString = m_StoreController.products.WithID(purcahseItem.generalProductID).metadata.localizedPriceString;
			purcahseItem.currency = m_StoreController.products.WithID(purcahseItem.generalProductID).metadata.isoCurrencyCode;
		}

	}


	public void OnInitializeFailed (InitializationFailureReason error)
	{
		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
		Debug.Log ("OnInitializeFailed InitializationFailureReason:" + error);
	}


	public PurchaseProcessingResult ProcessPurchase (PurchaseEventArgs args)
	{
		bool productRecognized = false;

		foreach (Purchasable purchaseItem in purchaseItems) {
			
			if (String.Equals (args.purchasedProduct.definition.id, purchaseItem.generalProductID, StringComparison.Ordinal)) {
				purchaseItem.onSuccesfulPurchase.Invoke(purchaseItem);
				Util.ShowPopUp ("Success!", "Product successfully purchased.");
				Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
				productRecognized = true;
			} 

		}

		if (!productRecognized) {
			Util.ShowPopUp ("Error", "Please check your internet connection and retry later");
			Debug.Log (string.Format ("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
		}

		ShowWaitingResponsePanel(false);
		return PurchaseProcessingResult.Complete;
	}


	public void OnPurchaseFailed (Product product, PurchaseFailureReason failureReason)
	{
		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing this reason with the user.
		ShowWaitingResponsePanel(false);
		Util.ShowPopUp ("Error", "Please check your internet connection and retry");
		Debug.Log (string.Format ("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
	}


	 
	public void ShowWaitingResponsePanel(bool value){

		if (waitingResponseOverlay != null) {
			if (value == true) {
				waitingResponseOverlay.SetActive (true);
			} else {
				waitingResponseOverlay.SetActive (false);
			}
		}

	}

}
