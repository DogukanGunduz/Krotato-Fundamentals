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
            // Oyuncunun saðlýk bileþenini güncelle
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        // Oyuncunun canýný düþür
        currentHealth -= enemyDamage; // Düþmanýn vurduðu hasar miktarý (istediðin deðeri ayarla)

        healthBar.SetHealt(currentHealth);

        // Oyuncunun canýný kontrol et
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            Debug.Log("Oyuncunun Caný: " + currentHealth);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
