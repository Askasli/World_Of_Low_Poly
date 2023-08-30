using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class Bullet : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField]private TrailRenderer _trailRenderer;
    private IMemoryPool _pool;
    private float _startTime;
    private float _lifeTime = 2.5f;

    [SerializeField] private int _damageAmount = 10;


    private Explosion.Factory _explosionFactory;

    [Inject]
    public void Construct(Explosion.Factory explosionFactory)
    {
        _explosionFactory = explosionFactory;
    }


    public void Update()
    {
        if (Time.realtimeSinceStartup - _startTime > _lifeTime)
        {
            _pool.Despawn(this);
        }
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _startTime = Time.realtimeSinceStartup;
        _trailRenderer.gameObject.SetActive(true);
        _pool = pool;
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
            _trailRenderer.gameObject.SetActive(false);


            var explosion = _explosionFactory.Create();
            explosion.transform.position = transform.position;

            ReturnToPool();
        }
    
    }

    public int GetDamage()
    {
        return _damageAmount;
    }

    public class Factory : PlaceholderFactory<Bullet>
    {
    }
}