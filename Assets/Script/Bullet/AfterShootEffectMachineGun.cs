using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class AfterShootEffectMachineGun : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] ParticleSystem[] _particleSystem;
    [SerializeField] float _lifeTime;
    float _startTime;

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

    public class Factory : PlaceholderFactory<AfterShootEffectMachineGun>
    {
    }
}