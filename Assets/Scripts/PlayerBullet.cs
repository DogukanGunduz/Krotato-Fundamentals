using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float lifetime = 1f; // Mermi �mr� (saniye cinsinden)
    public int damageAmount = 10; // Mermi hasar miktar�
    public float knockbackForce = 5f; // Geri itme kuvveti

    void Start()
    {
        // Mermiyi belirli bir s�re sonra yok et
        Destroy(gameObject, lifetime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Mermi d��mana �arparsa
        if (other.CompareTag("Enemy"))
        {
            // D��man�n �zerindeki EnemyHealth bile�enini al
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // E�er d��man�n sa�l�k bile�eni varsa hasar ver
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
            }

            // D��man� geri itme etkisi uygula
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            if (enemyRb != null)
            {
                Vector2 knockbackDirection = (enemyRb.transform.position - transform.position).normalized;

                // Knockback etkisini d��man� uzakla�t�racak �ekilde uygula
                enemyRb.AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
            }

            // Mermiyi �arp�lan d��manla birlikte yok et
            Destroy(gameObject);
        }
    }
}
