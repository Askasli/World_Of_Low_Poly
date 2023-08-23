
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
    public class ControllerInstaller : MonoInstaller
    {

        public override void InstallBindings()
        {
            Container.Bind<InputHandler>().AsSingle();
            Container.Bind<IMouseInput>().To<MouseInput>().AsSingle();
            Container.Bind<WeaponActivationState>().AsSingle();
            Container.Bind<IWeaponActivationState>().To<WeaponActivationState>().FromResolve();
         
        }
    }
}