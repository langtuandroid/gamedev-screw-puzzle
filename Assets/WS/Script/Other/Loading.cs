using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace WS.Script.Other
{
	public class Loading : MonoBehaviour 
	{
		private string _sceneName = "Playing";
		private float _time = 2;

		private void Start () 
		{
			StartCoroutine (LoadingRoutine ());
		}
		private IEnumerator LoadingRoutine()
		{
			yield return new WaitForSeconds (_time);
			SceneManager.LoadSceneAsync (_sceneName);
		}
	}
}
