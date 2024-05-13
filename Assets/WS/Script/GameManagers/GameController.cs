using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WS.Script.Other;
using WS.Script.Target;
using WS.Script.UI;
using WS.Script.Weapon;
using Zenject;

namespace WS.Script.GameManagers
{
    public class GameController : MonoBehaviour
    {
        [Inject] private TargetHandler _targetManager;
        [Inject] private MenuManager _menuManager;
        [Inject] private SoundManager _soundManager;
        [Inject] private WeaponHandler _weaponManager;
        public enum GameState
        {
            Menu,
            Playing,
            GameOver,
            Success,
            Pause,
            WatingForSave,
            NextStage
        }
        
        [FormerlySerializedAs("backgroundRenderer")] [SerializeField] private SpriteRenderer _bgSpriteRenderer;
        [FormerlySerializedAs("randomBackgrounds")] [SerializeField] private Sprite[] _bgSprites;
        private List<IObserver> _listeners;
        private bool _isShowEndScreen;
        public GameState gameState { get; private set; }
        public int GameScore { get; set; }
        
        private void Awake()
        {
            gameState = GameState.Menu;
            _listeners = new List<IObserver>();
        }
        
    
        public void BeginGame()
        {
            gameState = GameState.Playing;

            _targetManager.Configure();

            var listener_ = FindObjectsOfType<MonoBehaviour>().OfType<IObserver>();
            foreach (var _listener in listener_)
            {
                _listeners.Add(_listener);
            }

            foreach (var item in _listeners)
            {
                item.IPlay();
            }
        }
        
        public void OpenNewStage()
        {
            gameState = GameState.NextStage;
            StartCoroutine(NewStageRoutine());
        
        }

        private IEnumerator NewStageRoutine()
        {
            _menuManager.ShowUI(false);
            yield return new WaitForSeconds(1f);

            gameState = GameState.Playing;
            _menuManager.ShowUI(true);
            _targetManager.StageComplete();
            _bgSpriteRenderer.sprite = _bgSprites[Random.Range(0, _bgSprites.Length)];
        }
        
        public void Fail()
        {
            if (gameState == GameState.NextStage)
                return;

            if (!_isShowEndScreen)
            {
                if (gameState == GameState.WatingForSave)
                    return;

                gameState = GameState.WatingForSave;
                _isShowEndScreen = true;
                _menuManager.ShowAskForLife();
                return;
            }

            if (gameState == GameState.GameOver)
                return;

            ValueStorage.BestResult = Mathf.Max(GameScore, ValueStorage.BestResult);
            
            _soundManager.PauseMusic(true);

            StartCoroutine(GameEndedRoutine());
        }

        private IEnumerator GameEndedRoutine()
        {
            gameState = GameState.GameOver;
            yield return new WaitForSeconds(0.3f);

            yield return new WaitForSeconds(0.2f);

            Time.timeScale = 1;

            foreach (var item in _listeners)
                item.IGameOver();
        }
    
        public void Next()
        {
            _weaponManager.MoveOn();
            gameState = GameState.Playing;
        }
    }
}
