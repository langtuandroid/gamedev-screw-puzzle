using System;
using Game.Scripts.Shop;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts.UI
{
    [RequireComponent(typeof(Image))]
    public class BackgroundScale : MonoBehaviour
    {
        [Inject] private ItemsSelectedData _itemsSelectedData;
        private Image _backGroundsr;

        private void Awake()
        {
            _backGroundsr = GetComponent<Image>();
        }

        private void Start()
        {
            CheckDeviceInches();
            _itemsSelectedData.OnItemSelect += CheckDeviceInches;
        }

        private void CheckDeviceInches()
        {
            float screenSizeInchessr =
                Mathf.Sqrt(Mathf.Pow(Screen.width / Screen.dpi, 2) + Mathf.Pow(Screen.height / Screen.dpi, 2));
            float aspectRatio = (float)Screen.width / Screen.height; // Вычисляем соотношение сторон
            
            Sprite backgroundSpritesr;
            if (screenSizeInchessr >= 7.0f)
            {
                backgroundSpritesr = Mathf.Approximately(aspectRatio, 3f / 5f) ? _itemsSelectedData.TabletSlimImage : _itemsSelectedData.TabletImage;
            }
            else
            {
                backgroundSpritesr = _itemsSelectedData.PhoneImage;
            }

            _backGroundsr.sprite = backgroundSpritesr;
        }

        public void OnDestroy()
        {
            _itemsSelectedData.OnItemSelect -= CheckDeviceInches;
        }
    }
}