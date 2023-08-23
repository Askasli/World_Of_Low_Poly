using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Zenject;


public class EnemySpawner : MonoBehaviour
{
    private IEnemyControlSpawn _enemyControlSpawn;

    [Inject]
    private void Construct(IEnemyControlSpawn enemyControlSpawn)
    {
        _enemyControlSpawn = enemyControlSpawn;
    }

    private void Update()
    {
        _enemyControlSpawn.Spawner();
    }

}