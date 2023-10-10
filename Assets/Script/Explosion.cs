using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Explosion : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private ParticleSystem[] particleSystem;
    [SerializeField] private float timeBetweenParticles = 0.1f;

    [SerializeField] float explosionRadius = 15f;
    [SerializeField] float explosionForce = 100f;

    float startTime;
    [SerializeField]
    float lifeTime;

    IMemoryPool _pool;

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
      
        _pool = pool;
        StartCoroutine(CoolDownDespawn());
    }


    IEnumerator CoolDownDespawn()
    {
        yield return new WaitForSeconds(lifeTime);
        _pool.Despawn(this);
    }


    private void ApplyExplosion()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(explosionForce, transform.position, explosionRadius);
            }
        }
    }




    private IEnumerator PlayParticleSystem(int index)
    {
        yield return new WaitForSeconds(timeBetweenParticles * index);
        particleSystem[index].Clear();
        particleSystem[index].Play();
        ApplyExplosion();
    }

    public class Factory : PlaceholderFactory<Explosion>
    {
    }
}