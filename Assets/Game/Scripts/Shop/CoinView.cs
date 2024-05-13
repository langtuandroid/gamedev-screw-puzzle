using System;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Shop
{
    public class CoinView : MonoBehaviour
    {
        [Inject] private Bank _bank;
        [SerializeField] private TMP_Text _coinText;

        private void OnEnable()
        {
            _coinText.text = _bank.Coins.ToString();
            _bank.OnCoinsChange += ChangeCoins;
        }

        private void OnDisable()
        {
            _bank.OnCoinsChange -= ChangeCoins;
        }

        private void ChangeCoins(int coins)
        {
            _coinText.text = coins.ToString();
        }
    }
}