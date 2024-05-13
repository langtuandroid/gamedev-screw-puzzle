using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WS.Script.GameManagers;
using WS.Script.Other;
using WS.Script.Target;
using Zenject;

namespace WS.Script.UI
{
    public class MenuManager : MonoBehaviour, IObserver
    {
        [Inject] private TargetHandler _targetManager;
        [Inject] private SoundManager _soundManager;
        [Inject] private GameController _gameManager;
   
        [SerializeField] private GameObject _startMenu;
        [SerializeField] private GameObject _gameGUI;
        [SerializeField] private GameObject _gameOverMenu;
        [SerializeField] private GameObject _loadingMenu;
        [SerializeField] private GameObject _pauseMenu;
        [SerializeField] private GameObject _askForLifeUI;
        [SerializeField] private GameObject _shopMenu;
        [SerializeField] private GameObject _settingsMenu;
        
        [Space]
        [SerializeField] private TMP_Text _stageText;
        [SerializeField] private TMP_Text _textBoss;
        [SerializeField] private TMP_Text _scoreText;
        [SerializeField] private Image[] levelDots;
        [SerializeField] private TMP_Text _soundStatus;
        
        [Header("LOADING PROGRESS")]
        [SerializeField] private Slider slider;
        [SerializeField] private TMP_Text _progressText;
        
        private void Start()
        {
            _startMenu.SetActive(true);
            _gameGUI.SetActive(false);
            _gameOverMenu.SetActive(false);
            _loadingMenu.SetActive(false);
            _pauseMenu.SetActive(false);
            _askForLifeUI.SetActive(false);
            _shopMenu.SetActive(false);
            _settingsMenu.SetActive(false);

            if (ValueStorage.IsAutoPlay)
            {
                ValueStorage.IsAutoPlay = false;
                Play();
            }
        }

        private void Update()
        {
            if (_targetManager._currentTarget)
            {
                _stageText.text = "Stage: " + (_targetManager._stage + 1);

                if (_targetManager._currentTarget._stage != STAGE_TYPE.BossFight)
                {
                    for(int i = 0; i < levelDots.Length; i++)
                    {
                        levelDots[i].gameObject.SetActive(true);
                        levelDots[i].color = i == (_targetManager._stageToBoss-1) ? Color.yellow : Color.white;
                        levelDots[i].transform.localScale = i == (_targetManager._stageToBoss-1) ? Vector3.one * 1.2f : Vector3.one;
                    }
                }
                else
                {
                    for (int i = 0; i < levelDots.Length; i++)
                    {
                        levelDots[i].color = i == (levelDots.Length-1 )? Color.yellow : Color.white;
                        levelDots[i].transform.localScale = i == (levelDots.Length-1) ? Vector3.one * 1.5f : Vector3.one;
                        levelDots[i].gameObject.SetActive(i == (levelDots.Length - 1));
                    }
                }
            }

            _soundStatus.text = ValueStorage.IsSound ? "Sound: ON" : "Sound: OFF";
            AudioListener.volume = ValueStorage.IsSound ? 1 : 0;
            _scoreText.text = _gameManager.GameScore.ToString();
        }

        public void SwitchSound()
        {
            _soundManager.Click();
            ValueStorage.IsSound = !ValueStorage.IsSound;
        
        }

        public void Play()
        {
            _soundManager.Click();
            _startMenu.SetActive(false);
            _gameGUI.SetActive(true);
            _gameManager.BeginGame();
        }

        public void Restart()
        {
            ValueStorage.IsAutoPlay = true;
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void IPlay()
        {
            _startMenu.SetActive(false);
            _gameGUI.SetActive(true);
        }

        public void ISuccess()
        {
        
        }

        public void IPause()
        {
            throw new System.NotImplementedException();
        }

        public void IUnPause()
        {
            throw new System.NotImplementedException();
        }

        public void IGameOver()
        {
            StartCoroutine(GameOverCo(0));
        }
        
        public void GoHome()
        {
            _soundManager.Click();
            Time.timeScale = 1;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }

        public void OpenShop(bool open)
        {
            _soundManager.Click();
            _shopMenu.SetActive(open);
        }

        public void OpenSettings(bool open)
        {
            _soundManager.Click();
            _settingsMenu.SetActive(open);
        }

        public void OpenLeaderboard()
        {
            var leaderboard = GameObject.Find("LEADERBOARD");
            if (leaderboard != null)
                leaderboard.SendMessageUpwards("OpenLeaderboard", true);
            else
                Debug.LogError("No leaderboard setup, please read the Tutorial file to know more");
        }


        private IEnumerator LoadAsynchronously(string name)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(name);
            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                if (slider != null)
                    slider.value = progress;
                if (_progressText != null)
                    _progressText.text = (int)progress * 100f + "%";
                yield return null;
            }
        }

        public void HomeScene()
        {
            _soundManager.Click();
            Time.timeScale = 1;
            _loadingMenu.SetActive(true);
            StartCoroutine(LoadAsynchronously("MainMenu"));

        }

        public void Pause()
        {
            _soundManager.Click();
            if (Time.timeScale == 0)
            {
                _pauseMenu.SetActive(false);
                Time.timeScale = 1;
                _soundManager.PauseMusic(false);
            }
            else
            {
                _pauseMenu.SetActive(true);
                Time.timeScale = 0;
                _soundManager.PauseMusic(true);
            }
        }


        private IEnumerator GameOverCo(float time)
        {
            _gameGUI.SetActive(false);

            yield return new WaitForSeconds(time);
            
            _soundManager.PlaySfx(_soundManager.soundGameover, 0.8f);
            _gameOverMenu.SetActive(true);
        }

        public void ShowAskForLife()
        {
            StartCoroutine(AskForLifeCo(0.5f));
        }

        public void ShowUI(bool open)
        {
            _gameGUI.SetActive(open);
        }

        private IEnumerator AskForLifeCo(float time)
        {
            if (_askForLifeUI.GetComponent<SaveMenu>().CheckSave())
            {
                yield return new WaitForSeconds(time);
                _askForLifeUI.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                yield return null;
                _gameManager.Fail();
            }
        }
    }
}
