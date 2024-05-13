using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using WS.Script.GameManagers;
using WS.Script.Weapon;
using Zenject;

namespace WS.Script.UI
{
    public class WeaponBuy : MonoBehaviour
    {
        [Inject] private SoundManager _soundManager;
        
        [FormerlySerializedAs("weapon")] [SerializeField] private Weapon.Weapon _weapon;
        [FormerlySerializedAs("itemIcon")] [SerializeField] private Image _icon;
        [FormerlySerializedAs("coinHolder")] [SerializeField] private GameObject _coinHolder;
        

        public Weapon.Weapon Weapon => _weapon;
        private void OnEnable()
        {
            _icon.sprite = _weapon.weaponRenderer.sprite;
        }
    }
}
