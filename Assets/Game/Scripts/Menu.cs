using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts
{
    public class Menu : MonoBehaviour
    {
        private void Start()
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
