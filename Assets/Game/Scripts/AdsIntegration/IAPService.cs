using System.Collections.Generic;
using Game.Scripts.Shop;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;
using Zenject;


namespace Integration
{
    public class IAPService : MonoBehaviour, IDetailedStoreListener
    {
        [Inject] private Bank _bank;
        private static IStoreController _storeController;
        private static IExtensionProvider _extensionsProvider;
        
        [SerializeField]
        private PurchaseIDHolder _purchaseIDHolder;
        [SerializeField]
        public Toggle _toggleMonth;
        [SerializeField]
        public Toggle _toggleYear;
        [SerializeField]
        public Toggle _toggleForever;
        [SerializeField]
        public Button _buySubscriptionButton;
        [SerializeField]
        public Button _closeSubpanel;
        [SerializeField]
        private GameObject _subscriptionCanvas;
        
        private string _subscriptionMonthProductID;
        private string _subscriptionYearProductID;
        private string _subscriptionForeverProductID;
        
        private string _buy100Id;
        private string _buy300Id;
        private string _buy1000Id;
        private string _buy3000Id;
        
        private AdMobController _adMobController;

        [Inject]
        private void Construct (AdMobController adMobController)
        {
            _adMobController = adMobController;
        }

        private void Awake()
        {
            LoadID();
            if (_storeController == null)
            {
                InitializePurchasing();
            }
            else
            {
                string nameOfError = "error _storeController = null";
                Debug.Log(nameOfError);
            }
            DontDestroyOnLoad(gameObject);
        }
        
        private void LoadID()
        {
            _subscriptionMonthProductID = _purchaseIDHolder.SubscriptionMonthID;
            _subscriptionYearProductID = _purchaseIDHolder.SubscriptionYearID;
            _subscriptionForeverProductID = _purchaseIDHolder.SubscriptionForeverID;
            
            _buy100Id = _purchaseIDHolder.Buy100Id;
            _buy300Id = _purchaseIDHolder.Buy300Id;
            _buy1000Id = _purchaseIDHolder.Buy1000Id;
            _buy3000Id = _purchaseIDHolder.Buy3000Id;
        }

        private void OnEnable()
        {
            _buySubscriptionButton.onClick.AddListener(BuySubscription);
            _closeSubpanel.onClick.AddListener(HideSubscriptionPanel);
        }

        private void OnDisable()
        {
            _buySubscriptionButton.onClick.RemoveListener(BuySubscription);
            _closeSubpanel.onClick.RemoveListener(HideSubscriptionPanel);
        }

        public void ShowSubscriptionPanel()
        {
            if (_adMobController.IsPurchased)
            {
                return;
            }
            _subscriptionCanvas.SetActive(true);
            _adMobController.ShowBanner(false);
        }
        
        public void HideSubscriptionPanel()
        {
            _subscriptionCanvas.SetActive(false);
            _adMobController.ShowBanner(true);
        }

        private void CheckSubscriptionStatus()
        {
            if (IsInitialized())
            {
                string[] productIds = { _subscriptionMonthProductID, _subscriptionYearProductID, _subscriptionForeverProductID };

                bool subscriptionActive = false;

                foreach (string productId in productIds)
                {
                    var subscriptionProduct = _storeController.products.WithID(productId);

                    try
                    {
                        var isSubscribed = IsSubscribedTo(subscriptionProduct);
                        string isSubscribedText = isSubscribed ? "You are subscribed" : "You are not subscribed";
                        Debug.Log("isSubscribedText = " + isSubscribedText);
                        subscriptionActive = isSubscribed;
                        if (subscriptionActive)
                        {
                            break;
                        }
                    }
                    catch (StoreSubscriptionInfoNotSupportedException)
                    {
                        var receipt = (Dictionary<string, object>)MiniJson.JsonDecode(subscriptionProduct.receipt);
                        var store = receipt["Store"];
                        string isSubscribedText =
                            "Couldn't retrieve subscription information because your current store is not supported.\n" +
                            $"Your store: \"{store}\"\n\n" +
                            "You must use the App Store, Google Play Store or Amazon Store to be able to retrieve subscription information.\n\n" +
                            "For more information, see README.md";
                        Debug.Log("isSubscribedText = " + isSubscribedText);
                    }
                }
                PlayerPrefs.SetInt(_adMobController.noAdsKey, subscriptionActive ? 1 : 0);
                PlayerPrefs.Save();
                if (subscriptionActive)
                {
                    HideSubscriptionPanel();
                }
                else
                {
                    ShowSubscriptionPanel();
                }
            }
        }
        
        bool IsSubscribedTo(Product subscription)
        {
            if (subscription.receipt == null)
            {
                return false;
            }
            var subscriptionManager = new SubscriptionManager(subscription, null);
            var info = subscriptionManager.getSubscriptionInfo();
            return info.isSubscribed() == Result.True;
        }

        public bool IsInitialized()
        {
            return _storeController != null && _extensionsProvider != null;
        }

        private void InitializePurchasing()
        {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule
#if UNITY_IOS
             .Instance(AppStore.AppleAppStore));
#elif UNITY_ANDROID
            .Instance(AppStore.GooglePlay));
#else
            .Instance(AppStore.NotSpecified));
#endif
            builder.AddProduct(_subscriptionMonthProductID, ProductType.Subscription);
            builder.AddProduct(_subscriptionYearProductID, ProductType.Subscription);
            builder.AddProduct(_subscriptionForeverProductID, ProductType.NonConsumable);
            
            builder.AddProduct(_buy100Id, ProductType.Consumable);
            builder.AddProduct(_buy300Id, ProductType.Consumable);
            builder.AddProduct(_buy1000Id, ProductType.Consumable);
            builder.AddProduct(_buy3000Id, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }
        

        public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
        {
            Debug.Log("OnInitialized: SUCSESS");
            _storeController = controller;
            _extensionsProvider = extensions;
            
            CheckSubscriptionStatus();
        }

        private void BuySubscription()
        {
            if (_toggleMonth.isOn)
            {
                BuyProductID(_subscriptionMonthProductID);
            }
            else if (_toggleYear.isOn)
            {
                BuyProductID(_subscriptionYearProductID);
            }
            else if (_toggleForever.isOn)
            {
                BuyProductID(_subscriptionForeverProductID);
            }
        }
        
        public void BuyPack1()
        {
            BuyProductID(_buy100Id);
        }

        public void BuyPack2()
        {
            BuyProductID(_buy300Id);
        }

        public void BuyPack3()
        {
            BuyProductID(_buy1000Id);
        }
        
        public void BuyPack4()
        {
            BuyProductID(_buy3000Id);
        }

        
        public void BuyProductID(string productId)
        {
            _storeController.InitiatePurchase(productId);
        }
        

        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
        {
            var product = args.purchasedProduct;
            if (product.definition.id == _subscriptionMonthProductID)
            {
                Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
                _adMobController.RemoveAds();
                HideSubscriptionPanel();
            }
            if (product.definition.id == _subscriptionYearProductID)
            {
                Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
                _adMobController.RemoveAds();
                HideSubscriptionPanel();
            }
            if (product.definition.id == _subscriptionForeverProductID)
            {
                Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
                _adMobController.RemoveAds();
                HideSubscriptionPanel();
            }
            if (product.definition.id == _buy100Id)
            {
                _bank.ChangeCoins(100);
                Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
            }
            if (product.definition.id == _buy300Id)
            {
                _bank.ChangeCoins(300);
                Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
            }
            if (product.definition.id == _buy1000Id)
            {
                _bank.ChangeCoins(1000);
                Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
            }
            if (product.definition.id == _buy3000Id)
            {
                _bank.ChangeCoins(3000);
                Debug.Log($"ProcessPurchase: PASS. Product: '{product.definition.id}'");
            }
            
            return PurchaseProcessingResult.Complete;
        }
        
        public void RestorePurchases()
        {
            if (IsInitialized())
            {
                Debug.Log("Restoring purchases...");
                _extensionsProvider.GetExtension<IAppleExtensions>()?.RestoreTransactions(OnRestore);
            }
            else
            {
                Debug.Log("[STORE NOT INITIALIZED]");
            }
        }

        private void OnRestore(bool success, string error)
        {
            var restoreMessage = "";
            if (success)
            {
                restoreMessage = "Restore Successful";
            }
            else
            {
                restoreMessage = $"Restore Failed with error: {error}";
            }
            Debug.Log(restoreMessage);
        }

        public void OnInitializeFailed(InitializationFailureReason error)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }

        public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
        {
            Debug.Log($"OnPurchaseFailed: FAIL. Products: '{product.definition.storeSpecificId}', PurchaseFailureReason: {failureReason}");
        }

        public void OnInitializeFailed(InitializationFailureReason error, string? message)
        {
            Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
        }  
        
        public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
        {
            Debug.Log($"OnPurchaseFailed: {product}. {failureDescription}");
        }
    }
}
