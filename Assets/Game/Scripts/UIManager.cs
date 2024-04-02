using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace Game.Scripts
{
    public class UIManager : MonoBehaviour
    {
        [Inject] private LevelManager _levelManager;
        private static int _currentLvl;
        private static int _lvlNum;
        private static int _levelAttempts;
        
        [SerializeField] private GameObject _winPanel;
        [SerializeField] private List<int> _helpLevel;
        [Header("LevelNumber Bar")] 
        [SerializeField] private Image _lvlBgFill;
        [SerializeField] private GameObject[] _lvlNumImages;
        [SerializeField] private Sprite _filling;
        [SerializeField] private List<Sprite> _specialImages;
        [Header("Help Text")] 
        [SerializeField] private GameObject _pinObject;
        [SerializeField] private GameObject _fillObject;
        [Header("Key Help")] 
        [SerializeField] private GameObject _keyText;
        
        [Inject] private AudioManager _audioManager;
        private bool _help;
        public bool Pin { get; set; }
        public bool Fill { get; set; }
        public bool Win { get; set; }
        
        

        private void Awake()
        {
            _lvlNum = _levelManager.CurrentLevel;
            _lvlNumImages[^1].GetComponent<Image>().sprite = _specialImages[(_lvlNum - 1) / 5];
            LevelNumberHandler();
            
        }

        void Start()
        {
            if (_helpLevel.Contains(_lvlNum))
            {
                if (_pinObject != null && _fillObject != null)
                {
                    _help = true;
                }

                if (_pinObject != null && _fillObject != null)
                {
                    Pin = true;
                    Fill = false;
                    if (Pin)
                    {
                        _pinObject.SetActive(true);
                    }
                }
            }

            if (_keyText != null)
            {
                _keyText.SetActive(true);
            }
        }


        void Update()
        {
            if (!_help) return;
            switch (Fill)
            {
                case true when !Pin:
                    _pinObject.SetActive(false);
                    _fillObject.SetActive(true);
                    break;
                case false when !Pin:
                    _pinObject.SetActive(false);
                    _fillObject.SetActive(false);
                    break;
            }
        }

        private void LevelNumberHandler()
        {
            var lvlNumber = HandlingUnlockBasedOnLevel();
            print(lvlNumber + ":;" + _lvlNum);
            _lvlBgFill.DOFillAmount((lvlNumber - 1) / 5f, 0.25f).SetEase(Ease.Flash);
            for (var i = 0; i < _lvlNumImages.Length - 1; i++)
            {
                if (i < lvlNumber - 1)
                {
                    _lvlNumImages[i].GetComponent<Image>().color = new Color(1f, 0.71f, 0);
                }
                else if (i == lvlNumber - 1) _lvlNumImages[i].GetComponent<Image>().sprite = _filling;
            }

            switch (lvlNumber)
            {
                case 1:
                    _lvlNumImages[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _lvlNum.ToString();
                    _lvlNumImages[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum + 1).ToString();
                    _lvlNumImages[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum + 2).ToString();
                    _lvlNumImages[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum + 3).ToString();
                    //BossLevelImg(true);
                    break;
                case 2:
                    _lvlNumImages[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 1).ToString();
                    _lvlNumImages[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _lvlNum.ToString();
                    _lvlNumImages[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum + 1).ToString();
                    _lvlNumImages[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum + 2).ToString();
                    //BossLevelImg(false);
                    break;
                case 3:
                    _lvlNumImages[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 2).ToString();
                    _lvlNumImages[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 1).ToString();
                    _lvlNumImages[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _lvlNum.ToString();
                    _lvlNumImages[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum + 1).ToString();
                    //BossLevelImg(false);
                    break;
                case 4:
                    _lvlNumImages[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 3).ToString();
                    _lvlNumImages[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 2).ToString();
                    _lvlNumImages[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 1).ToString();
                    _lvlNumImages[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = _lvlNum.ToString();
                    //BossLevelImg(false);
                    break;
                case 5:
                    _lvlNumImages[0].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 4).ToString();
                    _lvlNumImages[1].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 3).ToString();
                    _lvlNumImages[2].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 2).ToString();
                    _lvlNumImages[3].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = (_lvlNum - 1).ToString();
                    //BossLevelImg(false);
                    break;
            }
        }

        private void BossLevelImg(bool call)
        {
            var lvlNo = PlayerPrefs.GetInt("SpecialImg", 0);
            if (_lvlNum > 5 && call && _currentLvl != _lvlNum)
            {
                lvlNo = lvlNo >= _specialImages.Count - 1 ? _specialImages.Count - 1 : ++lvlNo;
                _currentLvl = _lvlNum;
            }

            _lvlNumImages[^1].GetComponent<Image>().sprite = _specialImages[lvlNo];
            PlayerPrefs.SetInt("SpecialImg", lvlNo);
            print(lvlNo + "::");
        }


        private int HandlingUnlockBasedOnLevel()
        {
            var currentlvlnum = 0;

            var temp = _lvlNum;

            if (temp.ToString().Length > 1)
            {
                if (temp.ToString().EndsWith("1") || temp.ToString().EndsWith("6"))
                {
                    currentlvlnum = 1;
                }
                else if (temp.ToString().EndsWith("2") || temp.ToString().EndsWith("7"))
                {
                    currentlvlnum = 2;
                }
                else if (temp.ToString().EndsWith("3") || temp.ToString().EndsWith("8"))
                {
                    currentlvlnum = 3;
                }
                else if (temp.ToString().EndsWith("4") || temp.ToString().EndsWith("9"))
                {
                    currentlvlnum = 4;
                }
                else if (temp.ToString().EndsWith("5") || temp.ToString().EndsWith("0"))
                {
                    currentlvlnum = 5;
                }
            }
            else
            {
                if (temp.ToString().StartsWith("1") || temp.ToString().StartsWith("6"))
                {
                    currentlvlnum = 1;
                }
                else if (temp.ToString().StartsWith("2") || temp.ToString().StartsWith("7"))
                {
                    currentlvlnum = 2;
                }
                else if (temp.ToString().StartsWith("3") || temp.ToString().StartsWith("8"))
                {
                    currentlvlnum = 3;
                }
                else if (temp.ToString().StartsWith("4") || temp.ToString().StartsWith("9"))
                {
                    currentlvlnum = 4;
                }
                else if (temp.ToString().StartsWith("5") || temp.ToString().StartsWith("0"))
                {
                    currentlvlnum = 5;
                }
            }

            return currentlvlnum;
        }

        public void NextlevelButton()
        {
            if (_audioManager)
            {
                _audioManager.Play("Fill");
                GameManager.Vibrate();
            }

            _levelManager.LoadLevel(_levelManager.CurrentLevel + 1);
        }

        public void RetryButton()
        {
            if (_audioManager)
            {
                _audioManager.Play("Fill");
                GameManager.Vibrate();
            }
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            _levelAttempts++;
            Debug.Log($"Level Attempts::{_levelAttempts}");
        }

        public void HomeButton()
        {
            SceneManager.LoadScene(0);
        }

        public void WinPanel()
        {
            StartCoroutine(WinActive());
        }

        private IEnumerator WinActive()
        {
            yield return new WaitForSeconds(0.5f);
            _winPanel.SetActive(true);
        }
    }
}