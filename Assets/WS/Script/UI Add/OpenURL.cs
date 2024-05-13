using System;
using UnityEngine;
using UnityEngine.UI;

namespace NutBolts.Scripts.UI
{
    [RequireComponent(typeof(Button))]
    public class OpenURL : MonoBehaviour
    {
        private Button _button;
        [SerializeField] private string _urlToOpen = "https://www.google.com.ua/";

        private void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(Open);
        }

        private void Open()
        {
            Application.OpenURL(_urlToOpen);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(Open);
        }
    }
}