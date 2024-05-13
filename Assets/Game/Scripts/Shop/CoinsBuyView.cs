using System;
using Integration;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.Shop
{
    public class CoinsBuyView : MonoBehaviour
    {
        [Inject] private IAPService _iapService;
        [SerializeField] private int _buyIndex;
        [SerializeField] private Button _button;
        private void Awake()
        {
            _button.onClick.AddListener(Buy);
        }
        
        public void Buy()
        {
            switch (_buyIndex)
            {
                case 1:
                    _iapService.BuyPack1();
                    break;
                case 2:
                    _iapService.BuyPack2();
                    break;
                case 3:
                    _iapService.BuyPack3();
                    break;
                case 4:
                    _iapService.BuyPack4();
                    break;
            }
        }
        
    }
}