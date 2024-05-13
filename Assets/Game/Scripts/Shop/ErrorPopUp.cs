using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Scripts.Shop
{
    public class ErrorPopUp : MonoBehaviour
    {
        [SerializeField] private Button _closeButton;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(Close);
        }

        private void Close()
        {
            Destroy(gameObject);
        }
    }
}