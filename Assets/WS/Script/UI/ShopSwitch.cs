using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WS.Script.GameManagers;
using Zenject;

namespace WS.Script.UI
{
    public class ShopSwitch : MonoBehaviour
    {
        [Inject] private SoundManager _soundManager;
        [SerializeField] private WeaponBuy[] _weaponBuy;
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private Button _buyButton;
        [SerializeField] private Button _selectButton;
        [SerializeField] private Button _selectedButton;
        [SerializeField] private TMP_Text _textPrice;
         [FormerlySerializedAs("_namePrice")] [SerializeField] private TMP_Text _nameText;
        private int _currentWeapon;

        private void Start()
        {
            Switch(false);
            _leftButton.onClick.AddListener((() => Switch(false)));
            _rightButton.onClick.AddListener((() => Switch(true)));
            _buyButton.onClick.AddListener(Buy);
            _selectButton.onClick.AddListener(Equip);
        }

        private void Switch(bool isRight)
        {
            _weaponBuy[_currentWeapon].gameObject.SetActive(false);
            _currentWeapon = Math.Clamp(_currentWeapon + (isRight ? 1 : -1), 0, _weaponBuy.Length - 1);
            _weaponBuy[_currentWeapon].gameObject.SetActive(true);
            CheckButtons();
            
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(false);
            _selectedButton.gameObject.SetActive(false);
            _nameText.text = _weaponBuy[_currentWeapon].Weapon.Name;
            
            if (_weaponBuy[_currentWeapon].Weapon.IsUnlocked)
            {
                _buyButton.gameObject.SetActive(false);
                _selectButton.gameObject.SetActive(true);
                if (ValueStorage.WeaponEquipped == _weaponBuy[_currentWeapon].Weapon.gameObject.GetInstanceID())
                {
                    _selectButton.gameObject.SetActive(false);
                    _selectedButton.gameObject.SetActive(true);
                }
            }
            else
            {
                _buyButton.gameObject.SetActive(true);
                _selectButton.gameObject.SetActive(false);
                _textPrice.text = _weaponBuy[_currentWeapon].Weapon.Price.ToString();
            }
        }

        private void CheckButtons()
        {
            _leftButton.interactable = _currentWeapon != 0;
            _rightButton.interactable = _currentWeapon != _weaponBuy.Length - 1;
        }
        
        private void Buy()
        {
            if (ValueStorage.CoinsData <= _weaponBuy[_currentWeapon].Weapon.Price) return;
            ValueStorage.CoinsData -= _weaponBuy[_currentWeapon].Weapon.Price;
            
            _soundManager.PlaySfx(_soundManager.soundPurchasedItem);
            _weaponBuy[_currentWeapon].Weapon.IsUnlocked = true;
            _buyButton.gameObject.SetActive(false);
            _selectButton.gameObject.SetActive(true);
        }


        private void Equip()
        {
            ValueStorage.WeaponEquipped = _weaponBuy[_currentWeapon].Weapon.gameObject.GetInstanceID();
            _soundManager.PlaySfx(_soundManager.soundPickItem);
            _selectButton.gameObject.SetActive(false);
            _selectedButton.gameObject.SetActive(true);
        }
        
    }
}