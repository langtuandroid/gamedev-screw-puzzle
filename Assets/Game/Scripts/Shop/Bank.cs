using System;
using UnityEngine;

namespace Game.Scripts.Shop
{
    public class Bank
    {
        public event Action<int> OnCoinsChange; 
        private const string KEY = "Coins";
        private const int START_COINS = 0;
        private int _coins;

        public int Coins => _coins;

        public Bank()
        {
            _coins = PlayerPrefs.GetInt(KEY, START_COINS);
        }

        public void ChangeCoins(int change)
        {
            _coins += change;
            _coins = Math.Max(_coins, 0);
            PlayerPrefs.SetInt(KEY, _coins);
            OnCoinsChange?.Invoke(_coins);
        }
    }
}