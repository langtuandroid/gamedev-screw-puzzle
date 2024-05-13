using System;
using Game.Scripts.UI;
using Integration;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Shop
{
    
    public class BuySkinView : MonoBehaviour
    {
        private const string BOUGHT_KEY = "BuySkin";
        [Inject] private ItemsSelectedData _itemsSelectedData;
        [Inject] private StartMenu _startMenu;
        [Inject] private Bank _bank;
        [Inject] private AdMobController _adMobController;
        [Inject] private RewardedAdController _rewardedAdController;
        [SerializeField] private Image _image;
        [SerializeField] private int _itemIndex;
        [SerializeField] private int _itemCost = 100;
        [SerializeField] private bool _isWideo;
        [SerializeField] private TMPro.TMP_Text _coinText;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _videoButton;
        [SerializeField] private GameObject _itemSelected;
        private static Action<int> _onSelect;
        private string _isBoughtKey => BOUGHT_KEY + _itemIndex;
        private string _isSelectedKey => ItemsSelectedData.SELECTED_KEY;
        private bool _isDefault, _isBought, _isSelected;

        private void Start()
        {
            _image.sprite = _itemsSelectedData.TabletImages[_itemIndex];
            _coinText.text = _itemCost.ToString();

            _isDefault = _itemIndex == 0;
            _isBought = PlayerPrefs.GetInt(_isBoughtKey, _isDefault ? 1 : 0) == 1;
            _isSelected = PlayerPrefs.GetInt(_isSelectedKey, 0) == _itemIndex;
            
            _buyButton.gameObject.SetActive(!_isWideo);
            _videoButton.gameObject.SetActive(_isWideo);
            
            if (_isBought)
            {
                ChangeViewToBought();
                if (_isSelected)
                {
                    ChangeViewToSelect();
                }
            }
        }
        
        private void OnEnable()
        {
            _onSelect += OnItemSelected;
            _buyButton.onClick.AddListener(BuyWithCoins);
            _videoButton.onClick.AddListener(BuyWithVideo);
            _selectButton.onClick.AddListener(Select);
        }

        private void OnDisable()
        {
            _onSelect -= OnItemSelected;
            _buyButton.onClick.RemoveAllListeners();
            _videoButton.onClick.RemoveAllListeners();
            _selectButton.onClick.RemoveAllListeners();
        }
        
        private void BuyWithCoins()
        {
            if (_bank.Coins >= _itemCost)
            {
                _bank.ChangeCoins(-_itemCost);
                ItemUnlocked();
            }
            else
            {
                _startMenu.OpenCoinsBuy(true);
            }
        }

        private void BuyWithVideo()
        {
            _rewardedAdController.ShowAd();
            _rewardedAdController.GetRewarded += ItemUnlocked;
            _rewardedAdController.OnVideoClosed += ClosedVideo;
        }

        private void ClosedVideo()
        {
            _rewardedAdController.OnVideoClosed -= ClosedVideo;
            _rewardedAdController.GetRewarded -= ItemUnlocked;
        }

        private void ItemUnlocked()
        {
            _isBought = true;
            PlayerPrefs.SetInt(_isBoughtKey, 1);
            ChangeViewToBought();
        }

        private void Select()
        {
            _onSelect?.Invoke(_itemIndex);
        }
        
        private void OnItemSelected(int itemIndex)
        {
            if (!_isBought) return;
            
            bool isThisItemSelect = itemIndex == _itemIndex;
            _isSelected = isThisItemSelect;
            ChangeViewToSelect();
            if (isThisItemSelect)
            {
                _itemsSelectedData.SelectItem(_itemIndex);
                PlayerPrefs.SetInt(_isSelectedKey, _itemIndex);
            }
        }
        
        private void ChangeViewToBought()
        {
            _videoButton.gameObject.SetActive(false);
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
        }

        private void ChangeViewToSelect()
        {
            _itemSelected.SetActive(_isSelected);
            _selectButton.gameObject.SetActive(!_isSelected);
        }
    }
}
