using Game.Scripts.UI;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private StartMenu _startMenu;
        
        public override void InstallBindings()
        {
            Container.Bind<StartMenu>().FromInstance(_startMenu).AsSingle();
        }
    }
}