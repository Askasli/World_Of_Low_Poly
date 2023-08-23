using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyPool : MonoInstaller
{
    [SerializeField] private Transform _enemyTypeAPrefab;
    [SerializeField] private Transform _enemyTypeBPrefab;

    public override void InstallBindings()
    {
        Container.Bind<IEnemyControlSpawn>().To<EnemyControlSpawn>().AsSingle();

        Container.BindFactory<EnemyManager, EnemyManager.Factory>().FromPoolableMemoryPool<EnemyManager, EnemySpawnPool>(
           poolBinder => poolBinder.WithInitialSize(7).FromComponentInNewPrefab(_enemyTypeAPrefab).UnderTransformGroup("Enemies"));

        Container.BindFactory<EnemyManager, EnemyManager.Factory>().FromPoolableMemoryPool<EnemyManager, EnemySpawnPool>(
            poolBinder => poolBinder.WithInitialSize(7).FromComponentInNewPrefab(_enemyTypeBPrefab).UnderTransformGroup("Enemies"));
    }

    class EnemySpawnPool : MonoPoolableMemoryPool<IMemoryPool, EnemyManager>
    {
        
    }

}
