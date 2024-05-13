using UnityEngine;
using WS.Script.GameManagers;
using WS.Script.Target;
using WS.Script.UI;
using WS.Script.Weapon;
using Zenject;

namespace WS.Script.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameController _gameManager;
        [SerializeField] private MenuManager _menuManager;
        [SerializeField] private SoundManager _soundManager;
        [SerializeField] private WeaponHandler _weaponManager;
        [SerializeField] private KnifeMenu _knifeManager;
        [SerializeField] private TargetHandler _targetManager;
        [SerializeField] private ObjectPool _objectPool;
        public override void InstallBindings()
        {
            Container.Bind<GameController>().FromInstance(_gameManager).AsSingle();
            Container.Bind<MenuManager>().FromInstance(_menuManager).AsSingle();
            Container.Bind<SoundManager>().FromInstance(_soundManager).AsSingle();
            Container.Bind<WeaponHandler>().FromInstance(_weaponManager).AsSingle();
            Container.Bind<KnifeMenu>().FromInstance(_knifeManager).AsSingle();
            Container.Bind<TargetHandler>().FromInstance(_targetManager).AsSingle();
            Container.Bind<ObjectPool>().FromInstance(_objectPool).AsSingle();
        }
    }
}