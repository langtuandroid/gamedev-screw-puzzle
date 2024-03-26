using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

namespace Game.Scripts
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private List<LevelButton> _levelButtons;

        private void Start()
        {
            for (var i = 0; i < _levelButtons.Count; i++)
            {
                _levelButtons[i].Assign(i+1);
            }
        }

        private void LoadScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
            return;
            if (SceneManager.sceneCountInBuildSettings <= sceneIndex)
            {
                
            }
            else
            {
                Debug.LogWarning($"No scene with build index {sceneIndex}");
            }
            
        }

        private void Load()
        {
            int levelNumber = PlayerPrefs.GetInt("levelnumber", 1);
            if (levelNumber > SceneManager.sceneCountInBuildSettings - 1)
            {
                SceneManager.LoadScene(Random.Range(1, SceneManager.sceneCountInBuildSettings - 1));
            }
            else
            {
                SceneManager.LoadScene(levelNumber);
            }
        }
    }
}
