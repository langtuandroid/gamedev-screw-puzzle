using UnityEngine;
using UnityEngine.Serialization;
using WS.Script.GameManagers;
using WS.Script.Target;
using WS.Script.UI;
using Zenject;

namespace WS.Script.Weapon
{
    public class WeaponHandler : MonoBehaviour
    {
        [Inject] private DiContainer _diContainer;
        [Inject] private TargetHandler _targetManager;
        [Inject] private KnifeMenu _knifeManager;
        [Inject] private SoundManager _soundManager;
        [Inject] private GameController _gameManager;
        private float _throwSpeed = 50;
        private GameObject _lastWeapon;
        private AudioClip _throwSound;
        
        [FormerlySerializedAs("targetLayer")] [SerializeField] private LayerMask _layer;
        [FormerlySerializedAs("WeaponList")] [SerializeField] private GameObject[] _weapons;
        [FormerlySerializedAs("currentWeapon")] [SerializeField] private GameObject _selectedWeapon;
        [FormerlySerializedAs("pickedWeapon")] [SerializeField] private GameObject _pickedWeapon;
        public int MaxKnifes { get; private set; }
        public int KnifesNum { get; private set; }
        
        public void Configure()
        {
            _selectedWeapon = _weapons[0];
            if (ValueStorage.WeaponEquipped == 0)
                ValueStorage.WeaponEquipped = _weapons[0].GetInstanceID();
            else
            {
                foreach (var obj in _weapons)
                {
                    if (obj.GetInstanceID() == ValueStorage.WeaponEquipped)
                        _selectedWeapon = obj;
                }
            }

            MaxKnifes = _targetManager._currentTarget.Lives;
            KnifesNum = MaxKnifes;
            InstantiateWeapon();

            _knifeManager.Construct();
        }


        private void InstantiateWeapon()
        {
            _pickedWeapon = _diContainer.InstantiatePrefab(_selectedWeapon, transform.position, Quaternion.identity, null);
        }

        public void Throw()
        {
            if (_gameManager.gameState != GameController.GameState.Playing)
                return;

            if (_pickedWeapon == null)
                return;

            if(_lastWeapon != null)
            {
                _lastWeapon.GetComponent<Weapon>().Destroy();
            }

            _soundManager.PlaySfx(_throwSound);
            _pickedWeapon.GetComponent<Weapon>().Throw(_throwSpeed, _layer);
            _lastWeapon = _pickedWeapon;
            _pickedWeapon = null;
            KnifesNum--;
            if (KnifesNum > 0)
            {
                InstantiateWeapon();
            }
        }

        public void MoveOn()
        {
            KnifesNum++;
            if (_pickedWeapon != null)
                Destroy(_pickedWeapon.gameObject);
            _lastWeapon = null;
            InstantiateWeapon();
        }
    }
}
