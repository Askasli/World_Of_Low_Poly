using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int tankArmour;
    [SerializeField] private int currentHealth;
    private int maxHealth = 100;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    public void TakeDamage(int damageAmount)
    {
        int effectiveDamage = Mathf.Max(0, damageAmount - Mathf.Max(0, tankArmour));

        currentHealth -= effectiveDamage;
        currentHealth = Mathf.Max(0, currentHealth);

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
