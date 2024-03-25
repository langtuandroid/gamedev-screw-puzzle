using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
    private void Start()
    {
        //Vibration.Init();
        SceneManager.LoadScene(PlayerPrefs.GetInt("levelnumber", 1) > SceneManager.sceneCountInBuildSettings - 1
            ? Random.Range(1, SceneManager.sceneCountInBuildSettings - 1)
            : PlayerPrefs.GetInt("levelnumber", 1));
    }
}
