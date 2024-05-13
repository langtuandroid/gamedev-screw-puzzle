using Integration;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI
{
    public class StartMenu : MonoBehaviour
    {
        [Inject] private AdMobController _adMobController;
        [Inject] private IAPService _iapService;
        [Header("Buttons")]
        [SerializeField] private Button _openLevelsButton;
        [SerializeField] private Button _openOptionsButton;
        [SerializeField] private Button _closeLevelButton;
        [SerializeField] private Button _closeOptionsButton;
        [SerializeField] private Button _openShopButton;
        [SerializeField] private Button _closeShopButton;
            
        [Header("Menu")]
        [SerializeField] private GameObject _startMenu;
        [SerializeField] private GameObject _opetionsMenu;
        [SerializeField] private GameObject _levelsMenu;
        [SerializeField] private GameObject _coinsMenu;
        [SerializeField] private GameObject _shopMenu;

        private static bool _isFirstLoad = true;

        private void Start()
        {
            _openLevelsButton.onClick.AddListener((() => OpenLevels(true)));
            _closeLevelButton.onClick.AddListener((() => OpenLevels(false)));
            _openOptionsButton.onClick.AddListener((() => OpenOptions(true)));
            _closeOptionsButton.onClick.AddListener((() => OpenOptions(false)));
            _openShopButton.onClick.AddListener((() => OpenShop(true)));
            _closeShopButton.onClick.AddListener((() => OpenShop(false)));
            _adMobController.ShowBanner(true);
            if (_isFirstLoad)
            {
                _isFirstLoad = false;
                _iapService.ShowSubscriptionPanel();
            }
           
        }

        private void OpenLevels(bool isOpen)
        {
            _startMenu.SetActive(!isOpen);
            _levelsMenu.SetActive(isOpen);
        }
        
        private void OpenOptions(bool isOpen)
        {
            _startMenu.SetActive(!isOpen);
            _opetionsMenu.SetActive(isOpen);
        }
        
        private void OpenShop(bool isOpen)
        {
            _startMenu.SetActive(!isOpen);
            _shopMenu.SetActive(isOpen);
        }

        public void OpenCoinsBuy(bool isOpen)
        {
            _shopMenu.SetActive(!isOpen);
            _coinsMenu.SetActive(isOpen);
        }
    }
}