using UnityEditor.Rendering.LookDev;
using UnityEngine;
using Zenject;

namespace Common.Infrastructure
{
    public class CameraInstaller : MonoInstaller
    {
     
        public override void InstallBindings()
        {
            Container.Bind<ICameraRotation>().To<CameraRotation>().AsSingle();
            Container.Bind<ICameraPosition>().To<CameraPosition>().AsSingle();
        //    Container.Bind<ICameraShake>().To<�ameraShake>().AsSingle().WithArguments(cameraController.transform);
        } 
    }
}