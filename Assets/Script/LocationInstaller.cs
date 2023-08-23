using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private Transform startPoint;
        [SerializeField] private GameObject playerPrefab;

        public override void InstallBindings()
        {
            BindInputController();
            BindPlayer();
        }

        private void BindPlayer()
        {
            Container.Bind<Player>().FromComponentInNewPrefab(playerPrefab).WithGameObjectName("Player").AsSingle();
        }

        private void BindInputController()
        {
            Container.Bind<IInputManager>().To<InputManager>().AsCached();
        }

        private void Awake()
        {
            Player player = Container.Resolve<Player>();
            player.transform.position = startPoint.position;
        }


    }
}