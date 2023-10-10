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

        StartCoroutine(CoolDownDespawn());

    }

    IEnumerator CoolDownDespawn()
    {
        yield return new WaitForSeconds(_lifeTime);
        _pool.Despawn(this);
    }

    public class Factory : PlaceholderFactory<AfterShootEffectMachineGun>
    {
    }
}