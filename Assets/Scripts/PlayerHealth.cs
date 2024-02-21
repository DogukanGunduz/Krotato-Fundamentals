using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100;
    private float currentHealth;
    public float enemyDamage = 23f;

    public HealthBar healthBar;

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Oyuncunun sa�l�k bile�enini g�ncelle
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        // Oyuncunun can�n� d���r
        currentHealth -= enemyDamage; // D��man�n vurdu�u hasar miktar� (istedi�in de�eri ayarla)

        healthBar.SetHealt(currentHealth);

        // Oyuncunun can�n� kontrol et
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Oyuncunun Can�: " + currentHealth);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
