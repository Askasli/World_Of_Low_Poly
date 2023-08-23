using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyControlSpawn : IEnemyControlSpawn
{
    private Transform _targetPosition;
    private int _spawnedEnemyCount;
    private float _lastSpawnTime;
    private int MaxEnemyCount = 20;
    private float MinDelayBetweenSpawns = 2f;

    private List<EnemyManager.Factory> _enemyFactories;
    private List<EnemyManager> _existingEnemies = new List<EnemyManager>();
    private Player _player;



    [Inject]
    public EnemyControlSpawn(List<EnemyManager.Factory> enemyFactories, Player player)
    {
        _enemyFactories = enemyFactories;
        _player = player;
    }

    public void Spawner()
    {
        if (_spawnedEnemyCount < MaxEnemyCount && Time.realtimeSinceStartup - _lastSpawnTime > MinDelayBetweenSpawns)
        {
            SpawnRandomEnemy();
            _spawnedEnemyCount++;
        }


    }

    void SpawnRandomEnemy()
    {
        if (_spawnedEnemyCount >= MaxEnemyCount)
        {
            return; 
        }

        int randomIndex = Random.Range(0, _enemyFactories.Count);
        var chosenFactory = _enemyFactories[randomIndex];

        if (chosenFactory != null)
        {
            var enemyManager = chosenFactory.Create();
            enemyManager.OnEnemyDeath += HandleEnemyDeath;

            enemyManager.transform.position = ChooseRandomStartPosition(_existingEnemies);

            _existingEnemies.Add(enemyManager);

            _spawnedEnemyCount++;
            _lastSpawnTime = Time.realtimeSinceStartup;
        }
    }

    void HandleEnemyDeath(EnemyManager enemyManager)
    {
        enemyManager.OnEnemyDeath -= HandleEnemyDeath;
        _spawnedEnemyCount--;

        _existingEnemies.Remove(enemyManager);
        SpawnRandomEnemy();
    }

    Vector3 ChooseRandomStartPosition(List<EnemyManager> existingEnemies)
    {
        Vector3 playerPosition = _player.transform.position;

        float angle = Random.Range(0f, 360f);
        float distance = Random.Range(20f, 70f); 
        float minSpawnDistance = 15f; 

        Vector3 spawnOffset = Quaternion.Euler(0f, angle, 0f) * Vector3.forward * distance;
        Vector3 spawnPosition = playerPosition + spawnOffset + new Vector3(0, 1, 0);

        
        foreach (var enemy in existingEnemies)
        {
            if (Vector3.Distance(spawnPosition, enemy.transform.position) < minSpawnDistance)
            {
                return ChooseRandomStartPosition(existingEnemies);
            }
        }

        return spawnPosition;
    }

}
