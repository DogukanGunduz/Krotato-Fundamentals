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
        Debug.Log("Düþmana " + damage + " hasar verildi. Önceki Saðlýk: " + currentHealth);
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0; // Saðlýk deðerini sýfýra düþür
            Die();
        }

        Debug.Log("Kalan Saðlýk: " + currentHealth);
    }

    void Die()
    {
        Debug.Log("Düþman Öldü!");
        // Düþmanýn ölme iþlemleri buraya eklenebilir (örneðin, objeyi yok etmek veya özel bir animasyon oynatmak)
        Destroy(gameObject);
    }
}
