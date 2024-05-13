using UnityEngine;
using UnityEngine.UI;

    public class BackGroundManager : MonoBehaviour
    {
        [SerializeField]
        private Image _backGround;

        [SerializeField]
        private Sprite _bgSmartphone;
        [SerializeField]
        private Sprite _bgMidleTablet;
        [SerializeField]
        private Sprite _bgTablet;

        private void Start()
        {
            CheckDeviceInches();
        }

        private void CheckDeviceInches()
        {
            float screenSizeInches = Mathf.Sqrt(Mathf.Pow(Screen.width / Screen.dpi, 2) + Mathf.Pow(Screen.height / Screen.dpi, 2));
            float aspectRatio = (float)Screen.width / Screen.height;
            Sprite backgroundSprite;

            if (screenSizeInches >= 7.0f)
            {
                backgroundSprite = Mathf.Approximately(aspectRatio, 3f / 5f) ? _bgMidleTablet : _bgTablet;
            }
            else
            {
                backgroundSprite = _bgSmartphone;
            }

            _backGround.sprite = backgroundSprite;
        }
    }

