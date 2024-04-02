using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        public int CurrentLevel { get; private set; }
        public int MaxLevel { get; private set; }
        private void Awake()
        {
            MaxLevel = PlayerPrefs.GetInt("MaxLevel", 100);
        }

        public void NextLevel()
        {
            MaxLevel++;
            PlayerPrefs.SetInt("MaxLevel", MaxLevel);
        }

        public void LoadLevel(int level)
        {
            if (level > SceneManager.sceneCountInBuildSettings - 1)
            {
                CurrentLevel = 0;
            }
            else
            {
                CurrentLevel = level;
            }
            SceneManager.LoadScene(CurrentLevel);
        }
    }
}