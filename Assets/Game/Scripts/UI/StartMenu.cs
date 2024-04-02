using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.UI
{
    public class StartMenu : MonoBehaviour
    {
        [Header("Buttons")]
        [SerializeField] private Button _openLevelsButton;
        [SerializeField] private Button _openOptionsButton;
        [SerializeField] private Button _closeLevelButton;
        [SerializeField] private Button _closeOptionsButton;
        
        [Header("Menu")]
        [SerializeField] private GameObject _startMenu;
        [SerializeField] private GameObject _opetionsMenu;
        [SerializeField] private GameObject _levelsMenu;

        private void Start()
        {
            _openLevelsButton.onClick.AddListener((() => OpenLevels(true)));
            _closeLevelButton.onClick.AddListener((() => OpenLevels(false)));
            _openOptionsButton.onClick.AddListener((() => OpenOptions(true)));
            _closeOptionsButton.onClick.AddListener((() => OpenOptions(false)));
        }

        private void OpenLevels(bool isOpen)
        {
            _startMenu.SetActive(!isOpen);
            _levelsMenu.SetActive(isOpen);
        }
        
        private void OpenOptions(bool isOpen)
        {
            _startMenu.SetActive(!isOpen);
            _opetionsMenu.SetActive(isOpen);
        }
    }
}