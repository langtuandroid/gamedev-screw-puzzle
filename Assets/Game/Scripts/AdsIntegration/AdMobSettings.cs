#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

    [CreateAssetMenu(fileName = "Settings", menuName = "AdMob/Settings", order = 2)]
    public class AdMobSettings : ScriptableObject
    {
#if UNITY_ANDROID
        [Header("Ads ID")]
        [SerializeField] private string _bannerId;
        [SerializeField] private string _interstitialId;
        [SerializeField] private string _rewardedId;
        
        [Header("Test Ads ID")]
        [SerializeField] private string _bannerTestId;
        [SerializeField] private string _interstitialTestId;
        [SerializeField] private string _rewardedTestId;

#elif UNITY_IOS
        [Header("Ads ID")]
        [SerializeField] private string _bannerId;
        [SerializeField] private string _interstitialId;
        [SerializeField] private string _rewardedId;

        [Header("Test Ads ID")]
        [SerializeField] private string _bannerTestId = "ca-app-pub-3940256099942544/2934735716";
        [SerializeField] private string _interstitialTestId = "ca-app-pub-3940256099942544/4411468910";
        [SerializeField] private string _rewardedTestId = "ca-app-pub-3940256099942544/1712485313";

#endif
        [SerializeField]
        private bool _isProduction;
        public bool IsProduction => _isProduction;
        
        public string BannerID => IsProduction ? _bannerId : _bannerTestId;
        public string InterstitialID => IsProduction ? _interstitialId : _interstitialTestId;
        public string RewardedID => IsProduction ? _rewardedId : _rewardedTestId;
        
        
        private const string MobileAdmobFile = "Settings";
        
        public static AdMobSettings LoadInstance()
        {
            var instance = Resources.Load<AdMobSettings>(MobileAdmobFile);
            return instance;
        }

    }

