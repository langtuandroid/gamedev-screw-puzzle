using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class ProjectContext : MonoInstaller
    {
        [SerializeField] private AudioManager _audioManager;
        public override void InstallBindings()
        {
            Container.Bind<AudioManager>().FromInstance(_audioManager).AsSingle();
        }
    }
}