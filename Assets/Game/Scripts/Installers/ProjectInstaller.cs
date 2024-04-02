using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private AudioManager _audioManager;
        [SerializeField] private LevelManager _levelManager;
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().FromInstance(_audioManager).AsSingle();
            Container.Bind<LevelManager>().FromInstance(_levelManager).AsSingle();
        }
    }
}