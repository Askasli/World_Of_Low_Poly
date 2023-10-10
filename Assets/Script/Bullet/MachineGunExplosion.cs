using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MachineGunExplosion : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private ParticleSystem[] particleSystem;
    [SerializeField] private float timeBetweenParticles = 0.1f;
    [SerializeField] float lifeTime;
  

    IMemoryPool _pool;

 
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

        _pool = pool;

        StartCoroutine(CoolDownDespawn());
    }


    IEnumerator CoolDownDespawn()
    {
        yield return new WaitForSeconds(lifeTime);
        _pool.Despawn(this);
    }


    IEnumerator PlayParticleSystem(int index)
    {
        yield return new WaitForSeconds(timeBetweenParticles * index);
        particleSystem[index].Clear();
        particleSystem[index].Play();
    }

    public class Factory : PlaceholderFactory<MachineGunExplosion>
    {
    }
}