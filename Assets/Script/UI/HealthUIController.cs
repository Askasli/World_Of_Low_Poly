using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    private int currentHealth = 100;

  
    private void Update()
    {
        UpdateHealthUI();
    }


    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            SceneManager.LoadScene(0);
        }
    }

    private void UpdateHealthUI()
    {
        healthSlider.value = currentHealth;
    }
}
