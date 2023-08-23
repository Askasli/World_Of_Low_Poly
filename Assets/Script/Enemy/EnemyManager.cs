using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnemyManager : MonoBehaviour, IPoolable<IMemoryPool>
{
    [SerializeField] private Slider healthValue;
    public event Action<EnemyManager> OnEnemyDeath;
    IMemoryPool _pool;

    [SerializeField] private int maxHealth;
    [SerializeField] private int armor;
    [SerializeField] private float speed;

    [SerializeField] private int currentHealth;
    private IMoveToward _moveToward;

    private HealthUIController _healthController;

    [Inject]
    private void Construct(IMoveToward moveToward, HealthUIController healthController)
    {
        _moveToward = moveToward;
        _healthController = healthController;
    }

    public void Initialize(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    private void Update()
    {
        healthValue.value = currentHealth;
        _moveToward.MoveToThePlayer(transform, speed);
    }

    public void OnDespawned()
    {
        _pool = null;
    }

    public void OnSpawned(IMemoryPool pool)
    {
        _pool = pool;
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Player"))
        {
            _healthController.TakeDamage(25);
            ReturnToPool();
        }


        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(50, armor);
        }

        if (collision.CompareTag("mBullet"))
        {
            TakeDamage(20, armor);
        }
    }

    public void TakeDamage(int damage, int attackerArmor)
    {
        int effectiveDamage = Mathf.Max(0, damage - Mathf.Max(0, attackerArmor - armor));
        currentHealth -= effectiveDamage;
        currentHealth = Mathf.Max(0, currentHealth);

        if (currentHealth <= 0)
        {
            ReturnToPool();
        }
    }

    public void ReturnToPool()
    {
        OnEnemyDeath?.Invoke(this);
        _pool?.Despawn(this);
    }

    public class Factory : PlaceholderFactory<EnemyManager>
    {
    }

}