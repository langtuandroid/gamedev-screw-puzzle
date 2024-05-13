using GoogleMobileAds.Api;
using UnityEngine;
using Zenject;

namespace Integration  
{
	/// <summary>
	/// https://github.com/googleads/googleads-mobile-unity/tree/main/samples/HelloWorld/Assets/Scripts
	/// https://github.com/googleads/googleads-mobile-unity/releases
	/// https://developers.google.com/admob/unity/interstitial#ios
	/// </summary>

	public class AdMobController : MonoBehaviour
	{
		public string noAdsKey = "NoAds";
		[SerializeField] private AdMobSettings _settings;
		
		private bool _isPurchased;
		private BannerViewController _bannerViewController;
		private InterstitialAdController _interstitialAdController;
		private RewardedAdController _rewardedAdController;

		public bool IsPurchased => _isPurchased;

		[Inject]
		private void Construct(
			BannerViewController bannerViewController,
			InterstitialAdController interstitialAdController,
			RewardedAdController rewardedAdController)
		{
			_bannerViewController = bannerViewController;
			_interstitialAdController = interstitialAdController;
			_rewardedAdController = rewardedAdController;
		}
		private void Awake()
		{
			MobileAds.Initialize(initStatus =>
			{
				Debug.Log("InitAds = " + initStatus);
			});
		}

		private void Start()
		{
			_bannerViewController.BannerId = _settings.BannerID;
			_interstitialAdController.InterstitialId = _settings.InterstitialID;
			_rewardedAdController.RewardedId = _settings.RewardedID;
			LoadAllAds();
		}

		private void LoadAllAds()
		{
			_isPurchased = PlayerPrefs.GetInt(noAdsKey, 0) == 1;
			Debug.Log("_noAds=" +IsPurchased);
			if (!IsPurchased)
			{
				RequestBanner();
				_interstitialAdController.LoadAd();
			}

			_rewardedAdController.LoadAd();
		}

		public void RemoveAds()
		{
			PlayerPrefs.SetInt(noAdsKey, 1);
			PlayerPrefs.Save();
			_bannerViewController.HideAd();
		}
		
// Banner	
		public void RequestBanner()
		{
			_bannerViewController.CreateBannerView();
			_bannerViewController.LoadAd();
			_bannerViewController.HideAd();
		}

		public void ShowBanner(bool show)
		{
			_isPurchased = PlayerPrefs.GetInt(noAdsKey, 0) == 1;
			if (!IsPurchased)
			{
				if (show)
				{
					_bannerViewController.ShowAd();
				}
				else
				{
					_bannerViewController.HideAd();
				}
			}
		}

// Interstitial		
		public void ShowInterstitialAd()
		{
			_isPurchased = PlayerPrefs.GetInt(noAdsKey, 0) == 1;
			if (!IsPurchased)
			{
				_interstitialAdController.ShowAd();
			}
		}

// Rewarded			
		public void ShowRewardedAd()
		{
			_rewardedAdController.ShowAd();
		}
	}
}


