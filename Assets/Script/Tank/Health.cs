using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class Health : IHealth
{ 
    private int _currentHealth;
    private float _armour;
    private Slider _healthSlider;

    public Health(Slider healthSlider, int currentHealth, float armour)
    {
        _armour = armour;
        _currentHealth = currentHealth;
        _healthSlider = healthSlider;
    }

    public void TakeDamage(int damage)
    {
        float effectiveDamage = damage * (1 - _armour); 

        _currentHealth -= Mathf.RoundToInt(effectiveDamage);
        _currentHealth = Mathf.Max(0, _currentHealth);

        _healthSlider.value = _currentHealth;

        Debug.Log(_currentHealth);

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            SceneManager.LoadScene(0);
        }

    }

}

        

