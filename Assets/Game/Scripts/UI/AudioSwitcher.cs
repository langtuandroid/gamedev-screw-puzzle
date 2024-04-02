using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI {
    public class AudioSwitcher : MonoBehaviour
    {
        private const string Key = "Audio";
        [SerializeField] private Sprite _audioInfoOn, _audioInfoOff;
        [SerializeField] private Image _image;
        private bool _isVolume;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }
        
        private void OnEnable()
        {
            _button.onClick.AddListener(Change);
            _isVolume = PlayerPrefs.GetInt(Key, 1) == 1;
            Sunc();
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Change);
        }

        private void Change()
        {
            _isVolume = !_isVolume;
            Sunc();
        }

        private void Sunc()
        {
            int volume = _isVolume ? 1 : 0;
            PlayerPrefs.SetInt(Key, volume);
            AudioListener.volume = volume;
            _image.sprite = _isVolume ? _audioInfoOn : _audioInfoOff;
        }
    }
}
