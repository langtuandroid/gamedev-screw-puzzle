using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WS.Script.Other;
using WS.Script.Weapon;
using Zenject;

namespace WS.Script.UI
{
    public class KnifeMenu : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private WeaponHandler _weaponManager;
        [SerializeField] private  Image knifeItemUI;
        [SerializeField] private Sprite imageRemain;
        [SerializeField] private Sprite imageEmpty;
        [SerializeField] private Transform knifeContainer;
        [SerializeField] private List<Image> knifeList;

        private void Awake()
        {
            knifeList = new List<Image>();

            for (int i = 0; i < 20; i++)
            {
                knifeList.Add(Instantiate(knifeItemUI, knifeContainer));
            }
            knifeList.Reverse();
        }
    
        public void Construct()
        {
            foreach(var obj in knifeList)
            {
                obj.gameObject.SetActive(false);
            }

            for (int i = 0; i < _weaponManager.MaxKnifes; i++)
            {
                knifeList[i].gameObject.SetActive(true);
            }
        }

        private void UpdateUI()
        {
            for (int i = 0; i < knifeList.Count; i++)
            {
                knifeList[i].sprite = (i <= _weaponManager.KnifesNum - 1) ? imageRemain : imageEmpty;
            }
        }


        private void Update()
        {
            if (_weaponManager.MaxKnifes > 0)
                UpdateUI();
        }
    }
}
