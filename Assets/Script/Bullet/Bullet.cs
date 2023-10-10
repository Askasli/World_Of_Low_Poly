using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private float explosionRadius = 15f;
    [SerializeField] private float explosionForce = 100f;
    [SerializeField] private TrailRenderer _trailRenderer;

    private IExplosionForce _explosionForce;
    private IMemoryPool _pool;

    [SerializeField] private int _damageAmount = 10;
    private float lifeTime = 2.5f;

  


    private Explosion.Factory _explosionFactory;

    [Inject]
    public void Construct(Explosion.Factory explosionFactory, IExplosionForce explosionForce)
    {
        _explosionFactory = explosionFactory;
        _explosionForce = explosionForce;
    }


    public void OnSpawned(IMemoryPool pool)
    {
        _trailRenderer.enabled = false;
        _pool = pool;
        StartCoroutine(CoolDownDespawn());
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void ReturnToPool()
    {
        _pool?.Despawn(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Enemy"))
        {
          
            _trailRenderer.enabled = false;

            var explosion = _explosionFactory.Create();
            explosion.transform.position = transform.position;

            ReturnToPool();
        }
    
    }

    IEnumerator CoolDownDespawn()
    {
        yield return new WaitForSeconds(lifeTime);
        _pool.Despawn(this);
    }

    public int GetDamage()
    {
        return _damageAmount;
    }

    public class Factory : PlaceholderFactory<Bullet>
    {
    }
}