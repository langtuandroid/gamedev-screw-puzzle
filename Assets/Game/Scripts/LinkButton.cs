using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts
{
    [RequireComponent(typeof(Button))]
    public class LinkButton : MonoBehaviour
    {
        [SerializeField] private string URL = "https://www.youtube.com/";
        private Button _button;
        public void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(OpenLink);
        }
        private void OnDisable()
        {
            _button.onClick.RemoveListener(OpenLink);
        }
        private void OpenLink()
        {
            Application.OpenURL(URL);
        }
    }
}