using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Scripts
{
    public class LevelButton : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TMP_Text _levelText;
        private int _sceneIndex;

        public void Assign(int sceneIndex)
        {
            _sceneIndex = sceneIndex;
            _levelText.text = _sceneIndex.ToString();
            _button.onClick.AddListener(LoadScene);
        }

        private void LoadScene()
        {
            SceneManager.LoadScene(_sceneIndex);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(LoadScene);
        }
    }
}