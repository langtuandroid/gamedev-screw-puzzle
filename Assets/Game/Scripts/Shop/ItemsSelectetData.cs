using System;
using UnityEngine;

namespace Game.Scripts.Shop
{
    public class ItemsSelectedData : MonoBehaviour
    {
        [SerializeField] private Texture2D[] _boardTextures;
        [SerializeField] private Sprite[] _bgImagesPhone;
        [SerializeField] private Sprite[] _bgImagesTabletSlim;
        [SerializeField] private Sprite[] _bgImagesTablet;

        public const string SELECTED_KEY = "Select";
        public event Action OnItemSelect;
        private int _selectedIndex;

        public Sprite[] TabletImages => _bgImagesTablet;
        public Sprite PhoneImage => _bgImagesPhone[_selectedIndex]; 
        public Sprite TabletSlimImage => _bgImagesTabletSlim[_selectedIndex]; 
        public Sprite TabletImage => _bgImagesTablet[_selectedIndex];
        public Texture2D BoardTexture => _boardTextures[_selectedIndex];
        private void Awake()
        {
            _selectedIndex = PlayerPrefs.GetInt(SELECTED_KEY, 0);
        }

        public void SelectItem(int selectIndex)
        {
            _selectedIndex = selectIndex;
            PlayerPrefs.SetInt(SELECTED_KEY, selectIndex);
            OnItemSelect?.Invoke();
        }
        
    }
}