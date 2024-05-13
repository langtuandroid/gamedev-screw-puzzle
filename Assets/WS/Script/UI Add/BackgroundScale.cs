using UnityEngine;
using UnityEngine.UI;

namespace NutBolts.Scripts.UI
{
    public class BackgroundScale : MonoBehaviour
    {
        [SerializeField]
        private Image _backGroundsr;
        [SerializeField]
        private Sprite _bgSmartphonesr;
        [SerializeField]
        private Sprite _bgMidleTabletsr;
        [SerializeField]
        private Sprite _bgTabletsr;
        private void Start()
        {
            CheckDeviceInches();
        }
        private void CheckDeviceInches()
        {
            float screenSizeInchessr = Mathf.Sqrt(Mathf.Pow(Screen.width / Screen.dpi, 2) + Mathf.Pow(Screen.height / Screen.dpi, 2));
            float aspectRatio = (float)Screen.width / Screen.height; // Вычисляем соотношение сторон
            Sprite backgroundSpritesr;
            if (screenSizeInchessr >= 7.0f)
            {
                backgroundSpritesr = Mathf.Approximately(aspectRatio, 3f / 5f) ? _bgMidleTabletsr : _bgTabletsr;
            }
            else
            {
                backgroundSpritesr = _bgSmartphonesr;
            }
            _backGroundsr.sprite = backgroundSpritesr;
        }
        public class BackGroundManager 
        {
            
        }
    }
}