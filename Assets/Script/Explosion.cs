using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Explosion : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private ParticleSystem[] particleSystem;
    [SerializeField] private float timeBetweenParticles = 0.1f;

    float startTime;
    [SerializeField]
    float lifeTime;

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
        lifeTime = 0f;

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

    public class Factory : PlaceholderFactory<Explosion>
    {
    }
}