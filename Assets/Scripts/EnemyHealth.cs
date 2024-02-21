using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("D��mana " + damage + " hasar verildi. �nceki Sa�l�k: " + currentHealth);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0; // Sa�l�k de�erini s�f�ra d���r
            Die();
        }

        Debug.Log("Kalan Sa�l�k: " + currentHealth);
    }

    void Die()
    {
        Debug.Log("D��man �ld�!");
        // D��man�n �lme i�lemleri buraya eklenebilir (�rne�in, objeyi yok etmek veya �zel bir animasyon oynatmak)
        Destroy(gameObject);
    }
}
