
#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

    [CreateAssetMenu(fileName = "PurchaseID", menuName = "Subscription/PurchaseID", order = 1)]
    public class PurchaseIDHolder : ScriptableObject
    {
#if UNITY_ANDROID

    [Header("Production Purchase ID Android")]
    [SerializeField]
    private string _subscriptionMonthProductID;
    [SerializeField]
    private string _subscriptionYearProductID;
    [SerializeField]
    private string _subscriptionForeverProductID;
    
    [SerializeField]
    private string _buy100Id;
    [SerializeField]
    private string _buy300Id;
    [SerializeField]
    private string _buy1000Id;
    [SerializeField]
    private string _buy3000Id;
        
#elif UNITY_IOS
    
        [Header("Production Purchase ID IOS")]
        [SerializeField]
        private string _subscriptionMonthProductID;
        [SerializeField]
        private string _subscriptionYearProductID;
        [SerializeField]
        private string _subscriptionForeverProductID;
    
        [SerializeField]
        private string _buy100Id;
        [SerializeField]
        private string _buy300Id;
        [SerializeField]
        private string _buy1000Id;
        [SerializeField]
        private string _buy3000Id;

#endif
        
        [Header("Test ID")]
        [SerializeField]
        private string _subscriptionMonthID_Test = "sub.gamedev.test.one.month";
        [SerializeField]
        private string _subscriptionYearID_Test  = "sub.gamedev.test.one.year";
        [SerializeField]
        private string _subscriptionForeverID_Test  = "sub.gamedev.test.forever";
    
        [SerializeField]
        private string _buy100Id_Test  = "test.gamedev.buy100";
        [SerializeField]
        private string _buy300Id_Test  = "test.gamedev.buy300";
        [SerializeField]
        private string _buy1000Id_Test  = "test.gamedev.buy1000";
        [SerializeField]
        private string _buy3000Id_Test  = "test.gamedev.buy3000";
        [SerializeField]
        private bool _isProduction;
        public bool IsProduction => _isProduction;

        public string SubscriptionMonthID => IsProduction ? _subscriptionMonthProductID : _subscriptionMonthID_Test;
        public string SubscriptionYearID => IsProduction ? _subscriptionYearProductID : _subscriptionYearID_Test;
        public string SubscriptionForeverID => IsProduction ? _subscriptionForeverProductID : _subscriptionForeverID_Test;
        public string Buy100Id => IsProduction ? _buy100Id : _buy100Id_Test;
        public string Buy300Id => IsProduction ? _buy300Id : _buy300Id_Test;
        public string Buy1000Id => IsProduction ? _buy1000Id : _buy1000Id_Test;
        public string Buy3000Id => IsProduction ? _buy3000Id : _buy3000Id_Test;

        private const string MobilePurchaseIDSettingsFile = "PurchaseID";

        public static PurchaseIDHolder LoadInstance()
        {
            //Read from resources.
            var instance = Resources.Load<PurchaseIDHolder>(MobilePurchaseIDSettingsFile);
            Debug.Log("instance = " + instance.name);
            return instance;
        }
    
    }

