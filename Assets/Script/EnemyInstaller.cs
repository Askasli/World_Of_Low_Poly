using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyInstaller : MonoInstaller
{
    [SerializeField] private Transform targetTransform;

    public override void InstallBindings()
    {
         Container.Bind<IMoveToward>().To<MoveToward>().AsSingle();
    }
}
