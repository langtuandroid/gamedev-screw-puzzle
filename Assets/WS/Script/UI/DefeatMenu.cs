using TMPro;
using UnityEngine;
using WS.Script.GameManagers;
using Zenject;

namespace WS.Script.UI
{
    public class DefeatMenu : MonoBehaviour
    {
        [Inject] private GameController _gameManager;
        [SerializeField] private TMP_Text _bestText;
        [SerializeField] private TMP_Text _scoreText;

        private void Start()
        {
            _bestText.text = ValueStorage.BestResult.ToString();
            _scoreText.text = _gameManager.GameScore.ToString();
        }
    }
}
