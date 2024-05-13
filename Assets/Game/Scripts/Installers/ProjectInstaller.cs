using Game.Scripts.Shop;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private LevelManager _levelManager;
        [SerializeField] private ItemsSelectedData _itemsSelectedData;
        public override void InstallBindings()
        {
            Container.Bind<Bank>().AsSingle().NonLazy();
            Container.Bind<AudioManager>().FromInstance(_audioManager).AsSingle();
            Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle();
            Container.Bind<ItemsSelectedData>().FromInstance(_itemsSelectedData).AsSingle();
        }
    }
}