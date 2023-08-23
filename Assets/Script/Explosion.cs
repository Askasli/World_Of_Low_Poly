using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Explosion : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField]
    ParticleSystem[] _particleSystem;

    float _startTime;
    [SerializeField]
    float _lifeTime;

    IMemoryPool _pool;

    public void Update()
    {
        if (Time.realtimeSinceStartup - _startTime > _lifeTime)
        {
            _pool.Despawn(this);
        }
    }

    public void OnDespawned()
    {
    }

    public void OnSpawned(IMemoryPool pool)
    {
        for (int i = 0; i < _particleSystem.Length; i++)
        {
            _particleSystem[i].Clear();
            _particleSystem[i].Play();
        }
     
        _startTime = Time.realtimeSinceStartup;
        _pool = pool;
    }

    public class Factory : PlaceholderFactory<Explosion>
    {
    }
}