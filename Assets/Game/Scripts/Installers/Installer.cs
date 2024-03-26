using UnityEngine;
using Zenject;

namespace Game.Scripts.Installers
{
    public class Installer : MonoInstaller
    {
        [SerializeField] private Board _board;
        [SerializeField] private Finish _finish;
        [SerializeField] private Wednesday _wednesday;
        [SerializeField] private CameraMove _cameraMove;
        [SerializeField] private GameManager _gameManager;
        [SerializeField] private TouchDrop _touchDrop;
        public override void InstallBindings()
        {
            Container.Bind<Board>().FromInstance(_board).AsSingle();
            Container.Bind<Finish>().FromInstance(_finish).AsSingle();
            Container.Bind<CameraMove>().FromInstance(_cameraMove).AsSingle();
            Container.Bind<Wednesday>().FromInstance(_wednesday).AsSingle();
            Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle();
            Container.Bind<TouchDrop>().FromInstance(_touchDrop).AsSingle();
        }
    }
}