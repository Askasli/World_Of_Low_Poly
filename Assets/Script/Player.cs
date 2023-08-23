using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    private IHealth _health;

    [Inject]
    private void Construct(IHealth health)
    {
        _health = health;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }

}
