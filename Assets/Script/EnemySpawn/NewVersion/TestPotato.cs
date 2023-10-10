using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class TestPotato : MonoInstaller
{

    public override void InstallBindings()
    {
        Container.Bind<ItestTwo>().To<TestTwo>().AsSingle();

    }


 

}
