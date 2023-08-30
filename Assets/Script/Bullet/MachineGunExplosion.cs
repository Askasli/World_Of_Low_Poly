using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MachineGunExplosion : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private ParticleSystem[] particleSystem;
    [SerializeField] private float timeBetweenParticles = 0.1f;
    [SerializeField] float lifeTime;
    float startTime;

    IMemoryPool _pool;

    public void Update()
    {
        if (Time.realtimeSinceStartup - startTime > lifeTime)
        {
            _pool.Despawn(this);
        }
    }

    public void OnDespawned()
    {
    }

    public void OnSpawned(IMemoryPool pool)
    {
        for (int i = 0; i < particleSystem.Length; i++)
        {
            StartCoroutine(PlayParticleSystem(i));
            lifeTime += timeBetweenParticles;
        }

        startTime = Time.realtimeSinceStartup;
        _pool = pool;
    }

    private IEnumerator PlayParticleSystem(int index)
    {
        yield return new WaitForSeconds(timeBetweenParticles * index);
        particleSystem[index].Clear();
        particleSystem[index].Play();
    }

    public class Factory : PlaceholderFactory<MachineGunExplosion>
    {
    }
}