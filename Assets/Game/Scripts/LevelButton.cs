using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts
{
    public class LevelButton : MonoBehaviour
    {
        [Inject] private LevelManager _levelManager;
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private GameObject _lockedImage;
        private int _sceneIndex;

        public void Assign(int sceneIndex)
        {
            _sceneIndex = sceneIndex;
            _lockedImage.SetActive(false);
            _levelText.gameObject.SetActive(true);
            _levelText.text = _sceneIndex.ToString();
            _button.onClick.AddListener(LoadScene);
        }
        
        private void LoadScene()
        {
            _levelManager.LoadLevel(_sceneIndex);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(LoadScene);
        }
    }
}